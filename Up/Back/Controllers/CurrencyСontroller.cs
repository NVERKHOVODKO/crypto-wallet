namespace UP.Controllers;

/// <summary>
/// Контроллер для управления валютами, балансами пользователей и получения информации о криптовалютах.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ILogger<CurrencyController> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IDbRepository _dbRepository;

    public CurrencyController(ILogger<CurrencyController> logger, IUserRepository userRepository,
        ICurrencyRepository currencyRepository, IDbRepository dbRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _dbRepository = dbRepository;
    }

    /// <summary>
    /// Возвращает список монет пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Список монет пользователя</returns>
    [HttpGet]
    [Route("getUserCoins")]
    public Task<ActionResult> GetUserCoins(Guid userId)
    {
        _logger.LogInformation($"Return user coinList. Id: {userId}");
        return Task.FromResult<ActionResult>(Ok(_userRepository.GetUserCoins(userId)));
    }

    /// <summary>
    /// Возвращает полную информацию о монетах пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Полная информация о монетах пользователя</returns>
    [HttpGet]
    [Route("getUserCoinsFull")]
    public async Task<IActionResult> GetUserCoinsFull(Guid userId)
    {
        return Ok(await _userRepository.GetUserCoinsFull(userId));
    }
    
    /// <summary>
    /// Конвертирует количество одной валюты в другую.
    /// </summary>
    /// <param name="shortNameStart">Краткое имя исходной валюты</param>
    /// <param name="shortNameFinal">Краткое имя целевой валюты</param>
    /// <param name="quantity">Количество исходной валюты</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Конвертированное количество целевой валюты</returns>
    [HttpGet]
    [Route("getQuantityAfterConversion")]
    public async Task<IActionResult> GetQuantityAfterConversion(string shortNameStart, string shortNameFinal,
        double quantity, Guid userId)
    {
        try
        {
            _logger.LogInformation("User:" + userId + "Converted " + quantity + " " + shortNameStart + " to " +
                                   shortNameFinal);
            var apiKey = "4da2c4791b9c285b22c1bf08bc36f304ab2ca80bc901504742b9a42a814c4614";
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", apiKey);
            var url = "https://min-api.cryptocompare.com/data/price?fsym=" + shortNameStart + "&tsyms=" +
                      shortNameFinal;
            var response = await httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(responseContent);
            var priceRatio = (double)json[shortNameFinal.ToUpper()];
            var finalQuantity = priceRatio * quantity;
            return Ok(finalQuantity);
        }
        catch (Exception e)
        {
            _logger.LogInformation("Error. Currencies have not been converted");
            return BadRequest("Произошла неизвестная ошибка");
        }
    }

    /// <summary>
    /// Возвращает баланс пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <returns>Баланс пользователя</returns>
    [HttpGet]
    [Route("getUserBalance")]
    public async Task<ActionResult> GetUserBalance(Guid userId)
    {
        try
        {
            var balance = await _currencyRepository.GetUserBalance(userId);
            return Ok(balance);
        }
        catch (Exception e)
        {
            return BadRequest("Не удалось вернуть баланс пользователя");
        }
    }
    
    /// <summary>
    /// Возвращает количество монет определенного типа в кошельке пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="coinName">Название монеты</param>
    /// <returns>Количество монет</returns>
    [HttpGet]
    [Route("getCoinQuantityInUserWallet")]
    public Task<ActionResult> GetCoinQuantityInUserWallet(Guid userId, string coinName)
    {
        try
        {
            var quantity = _userRepository.GetCoinQuantityInUserWallet(userId, coinName);
            return Task.FromResult<ActionResult>(Ok(quantity));
        }
        catch (Exception e)
        {
            return Task.FromResult<ActionResult>(BadRequest("Не удалось вернуть количество монет"));
        }
    }
    
    /// <summary>
    /// Возвращает текущую цену монеты.
    /// </summary>
    /// <param name="quantity">Количество монет</param>
    /// <param name="coinName">Название монеты</param>
    /// <returns>Текущая цена монеты</returns>
    [HttpGet]
    [Route("getCoinPrice")]
    public async Task<ActionResult> GetCoinPrice(double quantity, string coinName)
    {
        try
        {
            var price = await _currencyRepository.GetCoinPrice(quantity, coinName);
            return Ok(price);
        }
        catch (Exception e)
        {
            return BadRequest("Не удалось вернуть цену");
        }
    }
    
    /// <summary>
    /// Возвращает историю цен монеты по краткому названию.
    /// </summary>
    /// <param name="shortName">Краткое имя монеты</param>
    /// <returns>Анализ изменений цен</returns>
    [HttpGet]
    [Route("get-coin-price-history/{shortName}")]
    public async Task<ActionResult> GetCoinRatio(string shortName)
    {
        var coinSnapShots = await _dbRepository.Get<CryptoCurrencyPrices>()
            .Where(c => c.CoinShortName == shortName)
            .Select(s => s.Price)
            .ToListAsync();
    
        if (coinSnapShots == null || coinSnapShots.Count == 0)
            throw new EntityNotFoundException($"Coin with short name {shortName} not found.");
    
        var result = AnalyzePrices(coinSnapShots);

        return Ok(result);
    }

    private static List<double> AnalyzePrices(IReadOnlyList<double> prices)
    {
        List<double> result = [];

        switch (prices.Count)
        {
            case 0:
                return result;
            case 1:
                result.Add(prices[0]);
                return result;
        }

        var changes = new List<double>();
        for (var i = 1; i < prices.Count; i++)
        {
            changes.Add(prices[i] - prices[i - 1]);
        }

        var interval = Math.Max(1, changes.Count / 10);
        for (var i = 0; i < changes.Count; i += interval)
        {
            var value = prices[i];
            if (changes[i] > 0)
                result.Add(value);
            else
            {
                result.Add(i > 0 ? prices[i - 1] : value);
            }
        }

        return result;
    }

    /// <summary>
    /// Возвращает активный список криптовалют.
    /// </summary>
    /// <returns>Список активных криптовалют с их информацией</returns>
    [HttpGet]
    [Route("getCurrenciesList")]
    public async Task<ActionResult> GetCoinsList()
    {
        var coinsList = await _dbRepository.Get<CoinListInfo>()
            .Where(x => x.IsActive == true)
            .ToListAsync();

        var cryptoDictionary = coinsList.ToDictionary(
            coin => coin.ShortName.ToLower(),
            coin => coin.FullName.ToLower()
        );
        var coins = new List<CoinsInformation>();
        foreach (var key in cryptoDictionary.Keys)
        {
            var temp = await _currencyRepository.GetFullCoinInformation(key);
            coins.Add(new CoinsInformation
            {
                Id = temp.Id,
                ShortName = temp.ShortName,
                FullName = temp.FullName,
                IconPath = temp.IconPath,
                DailyVolume = temp.DailyVolume,
                DailyImpact = temp.DailyImpact,
                Price = temp.Price,
                PercentagePriceChangePerDay = temp.PercentagePriceChangePerDay,
                Quantity = temp.Quantity
            });
            
            var coin = await _dbRepository.Get<CoinListInfo>()
                .FirstOrDefaultAsync(c => c.ShortName.ToLower() == temp.FullName.ToLower());
            if (coin == null)
                throw new EntityNotFoundException($"Coin with short name {temp.FullName} not found.");

            var currentTime = DateTime.UtcNow;
            var tenMinutesAgo = currentTime.AddMinutes(-1);

            var existingPrice = _dbRepository.Get<CryptoCurrencyPrices>()
                .FirstOrDefault(p => p.CoinId == coin.Id && p.Timestamp >= tenMinutesAgo);

            if (existingPrice != null) continue;
            var newPrice = new CryptoCurrencyPrices
            {
                Id = Guid.NewGuid(),
                CoinShortName = temp.FullName,
                Price = temp.Price,
                Timestamp = currentTime,
                CoinId = coin.Id
            };
            await _dbRepository.Add(newPrice);
        }
        await _dbRepository.SaveChangesAsync();
        return Ok(coins);
    }
}