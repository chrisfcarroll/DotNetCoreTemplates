using System;
using System.Collections.Generic;
using Consoleable.Component.Properties;
using NUnit.Framework;
using TestBase;

namespace Consoleable.Component.Tests
{
    [TestFixture]
    public class ComponentSpecs
    {
        List<String> log;
        AComponent sut;

        [SetUp]
        public void Setup()
        {
            var logger = new StringListLogger(log = new List<string>());
            sut = new AComponent(logger, new Settings());
        }

        [Test]
        public void Logs__WhenAVerbIsCalled()
        {
            sut.AVerb(null);

            log
                .ShouldNotBeEmpty()
                .ShouldContain(s=>s.Contains("AVerb"));

            log.ForEach(Console.WriteLine);
        }
    }
}
