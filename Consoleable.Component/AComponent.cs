using Consoleable.Component.Properties;
using Microsoft.Extensions.Logging;

namespace Consoleable.Component
{
    public class AComponent
    {
        internal readonly ILogger Logger;
        internal readonly Settings Settings;

        public AComponent(ILogger logger, Settings settings)
        {
            Logger = logger;
            Settings = settings;
            Logger.LogDebug("A console-runnable component with SomeSetting={@SomeSetting}", Settings.SomeSetting);
        }

        public int AnAction(params string[] args)
        {
            Logger.LogDebug( $"{nameof(AnAction)} called with {nameof(args)}"+"={@args}", args);
            return args?.Length ?? 0;
        }
    }
}
