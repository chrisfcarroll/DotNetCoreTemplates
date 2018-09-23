using System;
using System.Collections.Generic;
using Consoleable.Component.Properties;
using NUnit.Framework;
using Serilog.Extensions.Logging;
using Serilog;
using Serilog.Sinks.ListOfString;
using TestBase;

namespace Consoleable.Component.Specs.With.Extensions.Logging
{
    [TestFixture]
    public class WhenUsingSerilog
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
            var serilogger = new LoggerConfiguration()
                                .MinimumLevel.Verbose()
                                .WriteTo
                                .StringList(logLines = new List<string>())
                                .CreateLogger();
            var logger= new SerilogLoggerProvider(serilogger).CreateLogger("Test");

            sut = new AComponent(logger, new Settings());
        }
    }
}