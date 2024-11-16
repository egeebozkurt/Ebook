namespace Services.Contracts
{
    public interface ILoggerService
    {
        void LogWarning(string message);
        void LogError(string message);

    }
}
