using System;
using System.Linq;
using LibLog;
using Microsoft.Extensions.Configuration;

namespace Consoleable.Component.Properties
{
    class LoggingConfig
    {
        public static ILog LoggerFor<T>(){return LogProvider.For<T>();}
        public static ILog LoggerFor(string name){return LogProvider.GetLogger(name);}
        public static LoggingConfig Instance;
        
        public static ILog FromConfiguration(IConfiguration configuration)
        {

            configuration
                .GetSection(ReadFromSectionName)
                .Bind(Instance= new LoggingConfig());

            LogProvider
                .SetCurrentLogProvider(
                    Activator.CreateInstance(
                        LogProvider
                            .Providers
                            .FirstOrDefault(p=>p.Name.StartsWith(Instance.Provider??"Console"))) as ILogProvider);

            return LoggerFor<AComponent>();
        }
        const string ReadFromSectionName = "Logging";

        public LogLevel LogLevel { get; set; }
        public string Provider { get; set; }
    }
}