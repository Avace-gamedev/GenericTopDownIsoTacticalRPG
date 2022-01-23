namespace Backend.Kernel.Logging
{
    public interface ILoggerProvider
    {
        ILogger GetLogger(string name);
    }
}