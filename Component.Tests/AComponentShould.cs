using System;
using System.Collections.Generic;
using Component.Properties;
using NUnit.Framework;

namespace Component.Tests
{
    public class AComponentShould
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
        public void Log__WhenAVerbIsCalled()
        {
            sut.AVerb(null);
            Assert.IsNotEmpty(log);
        }
    }
}
