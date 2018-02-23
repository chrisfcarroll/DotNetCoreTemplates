using System;
using System.IO;
using Component.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Component
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            HelpAndExitIfNot( args.Length>0 );
            
            Startup.Configure(nameof(AComponent));

            var component = new AComponent(
                                           Startup.LoggerFactory.CreateLogger<AComponent>(),
                                           Startup.Settings
                                          );

            component.AVerb(args);
        }


        static void HelpAndExitIfNot(bool ok)
        {
            if (ok) return;
            Console.WriteLine(Help);
            Environment.Exit(0);
        }
        
        static readonly string Help =
            @"Help string for command line invocation here.

    Usage: Command [ArgumentA] [OptionB]

    Settings can re read from the app-settings.json section {nameof(AComponent)}

    Comments.";

    }

    static class Startup
    {
        public static IConfiguration Configuration;
        public static ILoggerFactory LoggerFactory;
        public static Settings Settings;


        public static void Configure(string settingsName)
        {
            Configuration = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", false)
                           .Build();
            Configuration.GetSection(settingsName).Bind(Settings = new Settings());

            LoggerFactory =  new LoggerFactory().FromConfiguration(Configuration);
        }

    }
}
