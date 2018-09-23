using Consoleable.Component.With.LibLog.Properties;

namespace Consoleable.Component.With.LibLog
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

        public bool AnAction(params string[] args)
        {
            Logger.Debug( $"{nameof(AnAction)} called with {nameof(args)}"+"={@args}", args);
            return true;
        }
    }
}
