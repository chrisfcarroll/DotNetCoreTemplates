using Consoleable.Component.With.LibLog.Properties;
using NUnit.Framework;
using TestBase;

namespace Consoleable.Component.Specs.With.LibLog
{
    [TestFixture]
    public class ComponentSpecs
    {
        [Test]
        public void CanBeCalledFromTheCommandLine()
        {
            Startup.Main("a","b","c").ShouldBe(3);
        }

        [Test]
        public void CanBeCreatedWithoutCommandLine()
        {
            var component=
                new Component.With.LibLog
                    .AComponent(
                        LibLogFactory.LoggerFor<AComponent>(), 
                        new Settings());

            component.AnAction("something").ShouldNotBeNull();
        }
    }
}