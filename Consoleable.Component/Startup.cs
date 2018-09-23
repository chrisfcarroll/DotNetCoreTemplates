using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Consoleable.Component.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

[assembly: ComVisible(false)]
[assembly: Guid("d1c4ab83-c553-4e3b-8e75-c9e76498206b")]
[assembly: InternalsVisibleTo("Consoleable.Component.Specs")]

namespace Consoleable.Component
{
    class Startup
    {
        public static IConfiguration Configuration;
        public static ILogger Logger;
        public static Settings Settings;
        public static ILogger CreateLogger<T>() { return LoggerFactoryFactory.LoggerFor<T>(); }

        public static int Main(params string[] args)
        {
            HelpAndExitIfNot( args.Length>0 );
            
            Configure<AComponent>();

            var component = new AComponent(
                Startup.CreateLogger<AComponent>(),
                Startup.Settings
            );

            return component.AnAction(args);
        }

        public static void Configure<T>()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(typeof(Startup).Assembly.Location))
                .AddJsonFile("appsettings.json", false)
                .Build();
            Configuration.GetSection(typeof(T).Name).Bind(Settings = new Settings());
            Logger = CreateLogger<T>();

            CreateLogger<Startup>().LogDebug(
                $"Console appsettings requested Setting {Settings.SomeSetting}, " +
                $"and requested logging at level {LoggerFactoryFactory.LogLevel}");
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
