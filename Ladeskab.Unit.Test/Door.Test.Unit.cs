using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Boundary;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    class DoorTest
    {
        private Door _uut;
        private int openCount;
        private int closeCount;

        [SetUp]
        public void Setup()
        {
            _uut = new Door();

            openCount = 0;
            closeCount = 0;

            _uut.doorOpenEvent += (o, a) => openCount++;
            _uut.doorCloseEvent += (o, a) => closeCount++;
        }

        [Test]
        public void openDoor_eventTriggered()
        {
            _uut.openDoor();
            Assert.That(openCount, Is.EqualTo(1));
        }

        [Test]
        public void openDoor_eventNotTriggered()
        {
            _uut.openDoor();
            Assert.That(closeCount, Is.EqualTo(0));
        }

        [Test]
        public void closeDoor_eventTriggered()
        {
            _uut.closeDoor();
            Assert.That(closeCount, Is.EqualTo(1));
        }

        [Test]
        public void closeDoor_eventNotTriggered()
        {
            _uut.closeDoor();
            Assert.That(openCount, Is.EqualTo(0));
        }

        [Test]
        public void DoorIsClosed()
        {
            _uut.closeDoor();
            Assert.That(_uut.DoorLocked, Is.False);
        }

        [Test]
        public void DoorIsOpen()
        {
            _uut.openDoor();
            Assert.That(_uut.DoorLocked, Is.True);
        }

        [Test]
        public void DoorClosedAtStartUp()
        {
            Assert.That(_uut.DoorLocked, Is.False);
        }
    }
}
