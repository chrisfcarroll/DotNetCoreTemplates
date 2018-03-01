using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Consoleable.Component.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

[assembly: ComVisible(false)]
[assembly: Guid("d1c4ab83-c553-4e3b-8e75-c9e76498206b")]
[assembly: InternalsVisibleTo("Consoleable.Component.Tests")]

namespace Consoleable.Component
{
    class Startup
    {
        public static IConfiguration Configuration;
        public static ILoggerFactory LoggerFactory;
        public static Settings Settings;
        public static ILogger CreateLogger<T>() { return LoggerFactory.CreateLogger<T>(); }
        public static ILogger CreateLogger(Type type) { return LoggerFactory.CreateLogger(type); }
        public static ILogger CreateLogger(string name) { return LoggerFactory.CreateLogger(name); }

        public static Instance<T> Configure<T>()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(typeof(Startup).Assembly.Location))
                .AddJsonFile("appsettings.json", false)
                .Build();
            Configuration.GetSection(typeof(T).Name).Bind(Settings = new Settings());

            LoggerFactory = new LoggerFactory().FromConfiguration(Configuration);
            LoggerFactory.CreateLogger("StartUp").LogDebug($"Logging configured at level {LoggingConfig.ConfiguredLoggingLevel}");
            LoggerFactory.CreateLogger("StartUp").LogDebug("Settings: {@Settings}", Settings);
            return new Instance<T>();
        }

        public class Instance<T> : Startup
        {
            // ReSharper disable MemberHidesStaticFromOuterClass
            public new IConfiguration Configuration => Startup.Configuration;
            public new ILoggerFactory LoggerFactory => Startup.LoggerFactory;
            public new Settings Settings => Startup.Settings;
            public     ILogger CreateLogger() => Startup.CreateLogger<T>();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            HelpAndExitIfNot( args.Length>0 );
            
            Startup.Configure<AComponent>();

            var component = new AComponent(
                                           Startup.LoggerFactory.CreateLogger<AComponent>(),
                                           Startup.Settings
                                          );

            component.AVerb(args);
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
