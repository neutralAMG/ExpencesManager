namespace Expences.Infraestrocture.Utils.Logger
{
    public class LoggerAdapter
    {
        private readonly ILoggerAdapter _logger;

        //TODO: fix the problem with the object neding a constructor
        public LoggerAdapter(ILoggerAdapter logger)
        {
            _logger = logger;
        }

        public void LogCritical(string message)
        {
            _logger.LogCritical(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogInfo(string message)
        {
            _logger.LogInfo(message);

        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
