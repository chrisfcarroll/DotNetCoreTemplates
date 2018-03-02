using LibLog;
using Consoleable.Component.Properties;

namespace Consoleable.Component
{
    public class AComponent
    {
        internal readonly ILog Logger;
        internal readonly Settings Settings;

        public AComponent(ILog logger, Settings settings)
        {
            Logger = logger;
            Settings = settings;
            Logger.Debug("A console-runnable component with SomeSetting={@SomeSetting}", Settings.SomeSetting);
        }

        public bool AVerb(params string[] args)
        {
            Logger.Debug( $"{nameof(AVerb)} called with {nameof(args)}"+"={@args}", args);
            return true;
        }
    }
}
