using Ladeskab.Boundary;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Text;
using System.IO;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    public class DisplayUnitTest
    {
        private Display _uut;
        StringWriter _result;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
            _result = new StringWriter();
            Console.SetOut(_result);
        }

        [Test]
        public void DisplayMessage_Test()
        {
            _uut.DisplayMessage("Tester");
            Assert.That(_result.ToString(), Is.EqualTo("Tester\r\n"));
        }
    }
}