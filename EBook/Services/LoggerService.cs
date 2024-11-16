using Services.Contracts;

namespace Services
{
    public class LoggerService : ILoggerService
    {       
        public void LogWarning(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
        public void LogError(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }

}
