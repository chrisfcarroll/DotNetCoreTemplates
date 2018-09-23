using System;
using System.Linq;
using LibLog;
using Microsoft.Extensions.Configuration;

namespace Consoleable.Component.Properties
{
    class LibLogFactory
    {
        public static ILog LoggerFor<T>(){return LogProvider.For<T>();}
        public static ILog LoggerFor(string name){return LogProvider.GetLogger(name);}
        public static LibLogFactory Instance;
        
        public static ILog FromConfiguration(IConfiguration configuration)
        {

            configuration
                .GetSection(ReadFromSectionName)
                .Bind(Instance= new LibLogFactory());

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