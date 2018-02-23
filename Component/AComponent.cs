using Component.Properties;
using Microsoft.Extensions.Logging;

namespace Component
{
    public class AComponent
    {
        internal readonly ILogger Logger;
        internal readonly Settings Settings;

        public AComponent(ILogger logger, Settings settings)
        {
            Logger = logger;
            Settings = settings;
        }

        public bool AVerb(string[] args)
        {
            Logger.LogInformation($"This is a console runnable component for {Settings.SomeSetting}");

            return true;
        }
    }
}
