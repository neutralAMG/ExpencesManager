using Microsoft.Extensions.Logging;

namespace Expences.Infraestrocture.Logger.Loggers
{
    public sealed class AplicationLogger<T> : ILoggerAdapter where T : class
    {
        private readonly ILogger<T> logger;

        //public AplicationLogger(ILogger<T> logger)
        //{
        //    this.logger = logger;
        //}

        public void LogError(string message)
        {
            logger.LogError(message);
        }

        public void LogInfo(string message)
        {
            logger.LogInformation(message);
        }

        public void LogCritical(string message)
        {
            logger.LogCritical(message);
        }

        public void LogWarning(string message)
        {
            logger.LogError(message);
        }
    }
}
