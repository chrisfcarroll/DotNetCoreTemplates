using Microsoft.Extensions.Logging;

namespace Consoleable.Component.Properties
{
    class LoggerFactoryFactory
    {
        public static ILogger LoggerFor<T>(){return Instance.CreateLogger<T>();}
        public static ILogger LoggerFor(string name){return Instance.CreateLogger(name);}
        public static ILoggerFactory Instance = new LoggerFactory();
        
        public static ILoggerFactory UsingProvider(ILoggerProvider provider = null)
        {
            if(provider!=null)Instance.AddProvider(provider);
            return Instance;
        }

        public static LogLevel LogLevel { get; set; }
    }
}