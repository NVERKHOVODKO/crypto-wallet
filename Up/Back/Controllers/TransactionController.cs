namespace UP.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController(
    IDbRepository dbRepository,
    ILogger<TransactionController> logger,
    IUserRepository userRepository,
    ICurrencyRepository currencyRepository,
    ITransactionsRepository transactionsRepository,
    IEmailService emailService)
    : ControllerBase
{
    /// <summary>
    /// Gets the equivalent coin quantity for a given amount in USD.
    /// </summary>
    /// <param name="coinName">Name of the coin.</param>
    /// <param name="quantityUSD">Amount in USD.</param>
    /// <returns>The equivalent quantity of the specified coin.</returns>
    [HttpGet]
    [Route("getCoinQuantity/{coinName}/{quantityUSD}")]
    public async Task<ActionResult> GetCoinQuantity(string coinName, double quantityUSD)
    {
        return Ok(await currencyRepository.GetCoinQuantity(quantityUSD, coinName));
    }

    /// <summary>
    /// Retrieves the user's conversation history based on the user's ID.
    /// </summary>
    /// <param name="id">User's unique ID.</param>
    /// <returns>A list of the user's transaction conversations.</returns>
    [HttpGet]
    [Route("getUserConversationsHistory/{id}")]
    public Task<ActionResult> GetUserList(Guid id)
    {
        return Task.FromResult<ActionResult>(Ok(transactionsRepository.GetUserConversionsHistory(id)));
    }
    
    /// <summary>
    /// Retrieves the user's deposit history.
    /// </summary>
    /// <param name="id">User's unique ID.</param>
    /// <returns>A list of the user's deposit history.</returns>
    [HttpGet]
    [Route("getUserDepositHistory/{id}")]
    public Task<ActionResult> GetUserDepositHistory(Guid id)
    {
        return Task.FromResult<ActionResult>(Ok(transactionsRepository.GetUserDepositHistory(id)));
    }

    /// <summary>
    /// Converts a specific amount of one coin into another.
    /// </summary>
    /// <param name="request">The conversion request data.</param>
    /// <returns>Success or failure message based on the conversion process.</returns>
    [HttpPost]
    [Route("convert")]
    public async Task<ActionResult> Convert([FromBody] ConvertRequest request)
    {
        switch (request.Quantity)
        {
            case 0:
                logger.LogInformation("Error. Quantity must be above than zero");
                return BadRequest("Количество должно быть больше нуля");
            case < 0:
                logger.LogInformation("Error. Quantity must be above than zero");
                return BadRequest("Количество должно быть больше нуля");
        }

        var apiKey = "4da2c4791b9c285b22c1bf08bc36f304ab2ca80bc901504742b9a42a814c4614";
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", apiKey);
        var url = "https://min-api.cryptocompare.com/data/price?fsym=" + request.ShortNameStart + "&tsyms=" +
                  request.ShortNameFinal;
        var response = await httpClient.GetAsync(url);
        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(responseContent);
        var priceRatio = (double)json[request.ShortNameFinal.ToUpper()];
        var finalQuantity = priceRatio * request.Quantity;
        var startCoinQuantityInUserWallet =
            userRepository.GetCoinQuantityInUserWallet(request.UserId, request.ShortNameStart);
        if (startCoinQuantityInUserWallet < request.Quantity)
        {
            logger.LogInformation("The user doesn't have enough coins to complete the conversion");
            return BadRequest("Недостаточно монет для совершения конвертации");
        }

        currencyRepository.SubtractCoinFromUser(request.UserId, request.ShortNameStart, request.Quantity);
        currencyRepository.AddCryptoToUserWallet(request.UserId, request.ShortNameFinal, finalQuantity);

        var conversionData = new Conversion
        {
            Id = Guid.Empty,
            Commission = 0,
            BeginCoinQuantity = request.Quantity,
            EndCoinQuantity = finalQuantity,
            QuantityUSD = await currencyRepository.GetCoinPrice(request.Quantity, request.ShortNameStart),
            BeginCoinShortname = request.ShortNameStart,
            EndCoinShortname = request.ShortNameFinal,
            UserId = request.UserId
        };

        await dbRepository.Add(conversionData);
        await dbRepository.SaveChangesAsync();

        logger.LogInformation("Converted successfully");
        return Ok("Конвертация совершена испешно");
    }

    /// <summary>
    /// Allows a user to buy cryptocurrency.
    /// </summary>
    /// <param name="request">The purchase request data.</param>
    /// <returns>Success or failure message based on the purchase process.</returns>
    [HttpPost]
    [Route("buyCrypto")]
    public async Task<ActionResult> BuyCrypto([FromBody] BuyCryptoRequest request)
    {
        if (request.Quantity == 0)
        {
            logger.LogInformation("Quantity must be above than zero");
            return UnprocessableEntity("Количество должно быть больше нуля");
        }

        var quantityUsdtInUserWallet = userRepository.GetCoinQuantityInUserWallet(request.UserId, "usdt");
        if (quantityUsdtInUserWallet < request.Quantity)
        {
            logger.LogInformation("Not enough balance");
            return UnprocessableEntity("Недостаточно монет");
        }

        currencyRepository.SubtractCoinFromUser(request.UserId, "usdt", request.Quantity);
        var coinQuantity = await currencyRepository.GetCoinQuantity(request.Quantity, request.CoinName);
        currencyRepository.AddCryptoToUserWallet(request.UserId, request.CoinName, coinQuantity);
        logger.LogInformation("UserId(" + request.UserId + ") bought " + coinQuantity + " " + request.CoinName);
        return Ok("Транзакция совершена успешно");
    }

    /// <summary>
    /// Allows a user to sell cryptocurrency.
    /// </summary>
    /// <param name="request">The sell request data.</param>
    /// <returns>Success or failure message based on the sell process.</returns>
    [HttpPut]
    [Route("sellCrypto")]
    public async Task<ActionResult> SellCrypto([FromBody] SellCryptoRequest request)
    {
        switch (request.QuantityForSell)
        {
            case 0:
                logger.LogInformation("Quantity must be above than zero");
                return UnprocessableEntity("Количество должно быть больше нуля");
            case < 0:
                logger.LogInformation("Quantity must be above than zero");
                return BadRequest("Количество должно быть больше нуля");
        }

        var quantityInUserWallet = userRepository.GetCoinQuantityInUserWallet(request.UserId, request.CoinName);
        if (quantityInUserWallet < request.QuantityForSell)
        {
            logger.LogInformation("Not enough coins");
            return UnprocessableEntity("Недостаточно монет");
        }

        currencyRepository.SubtractCoinFromUser(request.UserId, request.CoinName, request.QuantityForSell);
        currencyRepository.AddCryptoToUserWallet(request.UserId, "usdt",
            await currencyRepository.GetCoinPrice(request.QuantityForSell, request.CoinName));
        return Ok("Транзакция совершена успешно");
    }

    /// <summary>
    /// Transfers cryptocurrency from one user to another.
    /// </summary>
    /// <param name="request">The transfer request data.</param>
    /// <returns>Success or failure message based on the transfer process.</returns>
    [HttpPost]
    [Route("sendCrypto")]
    public Task<ActionResult> SendCrypto([FromBody] SendCryptoRequest request)
    {
        if (request.ReceiverId == request.SenderId)
        {
            logger.LogInformation("You can't send cryptocurrency to yourself");
            return Task.FromResult<ActionResult>(UnprocessableEntity("Невозможно отправить криптовалюту себе же"));
        }

        switch (request.QuantityForSend)
        {
            case 0:
                logger.LogInformation("Quantity must be above than zero");
                return Task.FromResult<ActionResult>(UnprocessableEntity("Количество должно быть больше нуля"));
            case < 0:
                logger.LogInformation("Quantity must be above than zero");
                return Task.FromResult<ActionResult>(UnprocessableEntity("Количество должно быть больше нуля"));
        }

        var quantityInUserWallet = userRepository.GetCoinQuantityInUserWallet(request.SenderId, request.CoinName);
        if (quantityInUserWallet < request.QuantityForSend)
        {
            logger.LogInformation("Not enough coins");
            return Task.FromResult<ActionResult>(UnprocessableEntity("Недостаточно монет"));
        }

        try
        {
            currencyRepository.AddCryptoToUserWallet(request.ReceiverId, request.CoinName, request.QuantityForSend);
        }
        catch (Exception e)
        {
            //ignored
        }
        currencyRepository.SubtractCoinFromUser(request.SenderId, request.CoinName, request.QuantityForSend);
        currencyRepository.WriteTransactionToDatabase(request.CoinName, request.QuantityForSend, request.SenderId,
            request.ReceiverId);

        emailService.SendEmailMessageTransactionsAsync(request.SenderId, request.CoinName, request.QuantityForSend,
            false, request.ReceiverId);
        emailService.SendEmailMessageTransactionsAsync(request.ReceiverId, request.CoinName, request.QuantityForSend,
            true, request.SenderId);

        logger.LogInformation("Transfer completed successfully");
        return Task.FromResult<ActionResult>(Ok("Перевод выполнен успешно"));
    }

    /// <summary>
    /// Пополнение баланса пользователя
    /// </summary>
    /// <param name="request">Запрос для пополнения баланса</param>
    /// <returns>Статус операции пополнения баланса</returns>
    [HttpPost]
    [Route("replenishTheBalance")]
    public Task<ActionResult> ReplenishTheBalance([FromBody] ReplenishTheBalanceRequest request)
    {
        logger.LogInformation("Replenishment from user(" + request.UserId + "): " + request.QuantityUsd + "$");
        switch (request.QuantityUsd)
        {
            case 0:
                return Task.FromResult<ActionResult>(BadRequest("Количество должно быть больше нуля"));
            case < 0:
                return Task.FromResult<ActionResult>(BadRequest("Количество должно быть больше нуля"));
        }

        transactionsRepository.ReplenishTheBalance(request.UserId, request.QuantityUsd);
        logger.LogInformation("Balance replenished successfully");
        return Task.FromResult<ActionResult>(Ok("Баланс пополнен успешно"));
    }

    /// <summary>
    /// Снятие USDT с баланса пользователя
    /// </summary>
    /// <param name="request">Запрос для снятия средств</param>
    /// <returns>Статус операции снятия средств</returns>
    [HttpPut]
    [Route("withdrawUSDT")]
    public Task<ActionResult> WithdrawUSDT([FromBody] WithdrawRequest request)
    {
        logger.LogInformation("Withdraw from user(" + request.UserId + "): " + request.QuantityForWithdraw + "$");
        switch (request.QuantityForWithdraw)
        {
            case 0:
                return Task.FromResult<ActionResult>(UnprocessableEntity("Количество должно быть больше нуля"));
            case < 0:
                logger.LogInformation("Quantity must be above than zero");
                return Task.FromResult<ActionResult>(UnprocessableEntity("Количество должно быть больше нуля"));
        }

        var quantityInUserWallet = userRepository.GetCoinQuantityInUserWallet(request.UserId, "usdt");
        if (quantityInUserWallet < request.QuantityForWithdraw)
        {
            logger.LogInformation("Not enough balance");
            return Task.FromResult<ActionResult>(UnprocessableEntity("Недостаточно монет"));
        }

        currencyRepository.SubtractCoinFromUser(request.UserId, "usdt", request.QuantityForWithdraw);
        currencyRepository.WriteWithdrawToDatabase(request.QuantityForWithdraw, request.QuantityForWithdraw * 0.02,
            request.UserId);
        return Task.FromResult<ActionResult>(Ok("Транзакция выполнена успешно"));
    }

    /// <summary>
    /// Получение истории снятий средств пользователем
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>История снятий средств пользователя</returns>
    [HttpGet]
    [Route("getUserWithdrawalsHistory/{userId}")]
    public Task<IActionResult> GetUserWithdrawalsHistory(Guid userId)
    {
        return Task.FromResult<IActionResult>(Ok(transactionsRepository.GetUserWithdrawalsHistory(userId)));
    }

    /// <summary>
    /// Получение истории транзакций пользователя
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>История транзакций пользователя</returns>
    [HttpGet]
    [Route("getUserTransactionsHistory/{userId}")]
    public Task<IActionResult> GetUserTransactionsHistory(Guid userId)
    {
        return Task.FromResult<IActionResult>(Ok(transactionsRepository.GetUserTransactionsHistory(userId)));
    }
}