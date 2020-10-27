using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Boundary;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    class RfidReaderTest
    {
        private RfidReader _uut;
        private int events;
        private int id;

        [SetUp]
        public void Setup()
        {
            _uut = new RfidReader();
            id = 0;
            events = 0;

            _uut.RfidEvent += (o, a) =>
            {
                events++;
                id = a.Id;
            };
        }

        [Test]
        public void RfidReader_EventTriggered()
        {
            _uut.RfidDetected(1234);
            Assert.That(events, Is.EqualTo(1));
        }

        [TestCase(1234)]
        [TestCase(123)]
        [TestCase(2)]
        public void RfidReader_IdSet(int id)
        {
            _uut.RfidDetected(id);
            Assert.That(id, Is.EqualTo(id));
        }
    }
}
