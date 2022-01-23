using Backend.Kernel.Logging;
using ILogger = Backend.Kernel.Logging.ILogger;

namespace Frontend.Logging
{
    public class UnityLoggerProvider: ILoggerProvider
    {
        public ILogger GetLogger(string name)
        {
            return new UnityLogger(name);
        }
    }
}