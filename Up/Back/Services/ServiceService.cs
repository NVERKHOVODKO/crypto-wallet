namespace UP.Services;

/// <summary>
/// Сервис для работы с услугами
/// </summary>
[AutoInterface]
public class ServiceService(IDbRepository repository) : IServiceService
{
    /// <summary>
    /// Получение 
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Service>> GetServices()
    {
        return Task.FromResult<IEnumerable<Service>>(repository.GetAll<Service>());
    }
}