using System;
using System.Linq;
using LibLog;
using Microsoft.Extensions.Configuration;

namespace Consoleable.Component.With.LibLog.Properties
{
    class LibLogFactory
    {
        public LogLevel LogLevel { get; set; }
        public string Provider { get; set; }

        public static ILog LoggerFor<T>(){return LogProvider.For<T>();}
        public static ILog LoggerFor(string name){return LogProvider.GetLogger(name);}
        public static LibLogFactory Instance;
        public static ILog FromConfiguration(IConfiguration configuration)
        {

            configuration
                .GetSection(Logging)
                .Bind(Instance= new LibLogFactory());

            LogProvider
                .SetCurrentLogProvider(
                    Activator.CreateInstance(
                        LogProvider
                            .Providers
                            .FirstOrDefault(p=>p.Name.StartsWith(Instance.Provider??"Console"))) as ILogProvider);

            return LoggerFor<AComponent>();
        }
        const string Logging = "Logging";
    }
}