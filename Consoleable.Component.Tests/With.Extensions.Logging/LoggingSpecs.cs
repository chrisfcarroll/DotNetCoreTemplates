using System;
using System.Collections.Generic;
using Consoleable.Component.Properties;
using Extensions.Logging.ListOfString;
using NUnit.Framework;
using TestBase;
using Microsoft.Extensions.Logging;

namespace Consoleable.Component.Specs.With.Extensions.Logging
{
    [TestFixture]
    public class LoggingSpecs
    {
        AComponent sut;
        List<string> logLines;

        [Test]
        public void Logs__WhenAVerbIsCalled()
        {
            sut.AnAction("boo");

            logLines.ForEach(Console.WriteLine);

            logLines
                .ShouldNotBeEmpty()
                .ShouldContain(s => s.Contains("AnAction"));
        }
    
        [SetUp]
        public void Setup()
        {
            var logger= new LoggerFactory()
                                .AddStringListLogger(logLines=new List<string>() )
                                .CreateLogger("Test");
            sut = new AComponent(logger, new Settings());
        }
    }
}