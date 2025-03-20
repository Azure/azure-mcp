namespace AzureMCP.Services.Interfaces;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, TimeSpan? expiration = null);
    Task SetAsync<T>(string key, T data, TimeSpan? expiration = null);
    Task DeleteAsync(string key);
} 