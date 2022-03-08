namespace OctoLink.Services
{
    public interface IOctoService
    {
        Task<T?> GetReadings<T>();
    }
}
