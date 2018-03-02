using System;
using Consoleable.Component.Properties;
using LibLog;
using NUnit.Framework;
using TestBase;
using StringListLogger=LibLog.StringListLogger;

namespace Consoleable.Component.Tests
{
    [TestFixture]
    public class ComponentSpecs
    {
        AComponent sut;
        ILog logger;

        [SetUp]
        public void Setup()
        {
            logger= LogProvider.Use<StringListLogProvider>()("Test");
            sut = new AComponent(logger, new Settings());
        }

        [Test]
        public void Logs__WhenAVerbIsCalled()
        {
            sut.AVerb("boo");

            StringListLogger.Loggers["Test"].LoggedLines.ForEach(Console.WriteLine);

            StringListLogger.Loggers["Test"].LoggedLines
                .ShouldNotBeEmpty()
                .ShouldContain(s => s.Contains("AVerb"));
        }
    }
}