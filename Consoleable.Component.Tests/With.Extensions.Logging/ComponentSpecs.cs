using Consoleable.Component.Properties;
using NUnit.Framework;
using TestBase;

namespace Consoleable.Component.Specs.With.Extensions.Logging
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
            var component=new AComponent(LoggerFactoryFactory.LoggerFor<AComponent>(), new Settings());

            component.AnAction("something").ShouldNotBeNull();
        }
    }
}