using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Consoleable.Component.With.LibLog.Properties;
using Microsoft.Extensions.Configuration;

[assembly: ComVisible(false)]
[assembly: Guid("d1c4ab83-c553-4e3b-8e75-c9e76498206b")]
[assembly: InternalsVisibleTo("Consoleable.Component.Tests")]

namespace Consoleable.Component.With.LibLog
{
    class Startup
    {
        public static IConfiguration Configuration;
        public static ILog Logger;
        public static Settings Settings;
        public static ILog CreateLogger<T>() { return LoggingConfig.LoggerFor<T>(); }

        public static Instance<T> Configure<T>()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(typeof(Startup).Assembly.Location))
                .AddJsonFile("appsettings.json", false)
                .Build();
            Configuration.GetSection(typeof(T).Name).Bind(Settings = new Settings());
            Logger = LoggingConfig.FromConfiguration(Configuration);
            LoggingConfig.LoggerFor("Startup").Debug($"Console appsettings requested Setting {Settings.SomeSetting}, and requested logging with provider {LoggingConfig.Instance.Provider} at level {LoggingConfig.Instance.LogLevel}");
            return new Instance<T>();
        }

        public class Instance<T> : Startup
        {
            // ReSharper disable MemberHidesStaticFromOuterClass
            public new IConfiguration Configuration => Startup.Configuration;
            public new ILog Logger => Startup.Logger;
            public new Settings Settings => Startup.Settings;
            public     ILog CreateLogger() => Startup.CreateLogger<T>();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            HelpAndExitIfNot( args.Length>0 );
            
            Startup.Configure<AComponent>();

            var component = new AComponent(
                                           Startup.CreateLogger<AComponent>(),
                                           Startup.Settings
                                          );

            component.AnAction(args);
        }

        static void HelpAndExitIfNot(bool argsOk)
        {
            if (argsOk) return;
            Console.WriteLine( Help );
            Environment.Exit(0);
        }
        
        static readonly string Help =
            @"Help string for command line invocation here.

    Usage: dotnet Consoleable.Component.dll [ArgumentA] [OptionB]

    Settings can be read from the app-settings.json section {nameof(AComponent)}

    Example

    <Insert useful and illuminating example usages here>

    Comments.
";
    }
}
