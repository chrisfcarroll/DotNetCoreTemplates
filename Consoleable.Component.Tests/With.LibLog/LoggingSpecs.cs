using System;
using Consoleable.Component.With.LibLog.Properties;
using LibLog;
using NUnit.Framework;
using TestBase;
using StringListLogger=LibLog.StringListLogger;

namespace Consoleable.Component.Specs.With.LibLog
{
    [TestFixture]
    public class LoggingSpecs
    {
        Consoleable.Component.With.LibLog.AComponent sut;
        ILog logger;

        [Test]
        public void Logs__WhenAVerbIsCalled()
        {
            sut.AnAction("boo");

            StringListLogger.Loggers["Test"].LoggedLines.ForEach(Console.WriteLine);

            StringListLogger.Loggers["Test"].LoggedLines
                .ShouldNotBeEmpty()
                .ShouldContain(s => s.Contains("AnAction"));
        }
    
        [SetUp]
        public void Setup()
        {
            logger= LogProvider.Use<StringListLogProvider>()("Test");
            sut = new Consoleable.Component.With.LibLog.AComponent(logger, new Settings());
        }
    }
}