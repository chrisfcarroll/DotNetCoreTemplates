using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Consoleable.Component.With.LibLog.Properties;
using LibLog;
using Microsoft.Extensions.Configuration;

[assembly: ComVisible(false)]
[assembly: Guid("d1c4ab83-c553-4e3b-8e75-c9e76498206b")]
[assembly: InternalsVisibleTo("Consoleable.Component.Specs")]

namespace Consoleable.Component.With.LibLog
{
    class Startup
    {
        public static IConfiguration Configuration;
        public static ILog Logger;
        public static Settings Settings;
        public static ILog CreateLogger<T>() { return LibLogFactory.LoggerFor<T>(); }

        public static void Main(string[] args)
        {
            HelpAndExitIfNot( args.Length>0 );
            
            Configure<AComponent>();

            var component = new AComponent(
                Startup.CreateLogger<AComponent>(),
                Startup.Settings
            );
            component.AnAction(args);
        }

        public static void Configure<T>()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(typeof(Startup).Assembly.Location))
                .AddJsonFile("appsettings.json", false)
                .Build();
            Configuration.GetSection(typeof(T).Name).Bind(Settings = new Settings());
            Logger = LibLogFactory.FromConfiguration(Configuration);
            LibLogFactory.LoggerFor("Startup").Debug($"Console appsettings requested Setting {Settings.SomeSetting}, and requested logging with provider {LibLogFactory.Instance.Provider} at level {LibLogFactory.Instance.LogLevel}");
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
