
namespace Expences.Infraestrocture.Logger
{
    public interface ILoggerAdapter
    {
        void LogError(string message);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogCritical(string message);
    }
}
