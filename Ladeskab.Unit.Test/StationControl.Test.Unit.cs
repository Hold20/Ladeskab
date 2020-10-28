using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NSubstitute;
using Ladeskab.Controller;
using Ladeskab.Boundary;
using Ladeskab.Interface;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    class StationControlTest
    {
        private IChargeControl _charger;
        private IDoor _door;
        private IDisplay _display;
        private IRfidReader _rfidReader;
        StringWriter _result;

        private StationControl _uut;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRfidReader>();
            _charger = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _result = new StringWriter();

            _uut = new StationControl(_door, _display, _rfidReader, _charger);
        }

        [Test]
        public void doorOpenOnStartupTest()
        {
            _door.doorOpenEvent += Raise.EventWith(new doorOpenEventArgs());

            _display.Received(1).DisplayMessage("Forbind telefon");
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        }

        [Test]
        public void doorOpenAndThenCloseTest()
        {
            _door.doorOpenEvent += Raise.EventWith(new doorOpenEventArgs());
            _door.doorCloseEvent += Raise.EventWith(new doorCloseEventArgs());

            _display.Received(1).DisplayMessage("Tryk R for at indtaste kode og dermed låse");
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void RfidDetected_LadestateAvalible_chargerIsConnected()
        {
            _door.doorOpenEvent += Raise.EventWith(new doorOpenEventArgs());
            _door.doorCloseEvent += Raise.EventWith(new doorCloseEventArgs());

            _charger.IsConnected().Returns(true);

            int testId = 1234;
            _rfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs(testId));

            _display.Received(1).DisplayMessage("Telefonen er forbundet og du er grim");
            _charger.Received(1).StartCharge();
            _door.Received(1).LockedDoor();
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
            Assert.That(_uut._oldId, Is.EqualTo(testId));
        }

        [Test]
        public void RfidDetected_LadestateAvalible_chargerIsNotConnected()
        {
            _door.doorOpenEvent += Raise.EventWith(new doorOpenEventArgs());
            _door.doorCloseEvent += Raise.EventWith(new doorCloseEventArgs());

            _charger.IsConnected().Returns(false);

            _rfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs(1234));

            _display.Received(1).DisplayMessage("Der er ingen forbindelse til din telefon");
        }

        [Test]
        public void RfidDetected_LadestateLocked_idIsOldId()
        {
            _uut._state = StationControl.LadeskabState.Locked;
            int testId = 1234;
            _uut._oldId = testId;

            _rfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs(testId));

            _display.Received(1).DisplayMessage("Fjern nu bare din telefon for helvede");
            _charger.Received(1).StopCharge();
            _door.Received(1).UnlockedDoor();
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void RfidDetected_LadestateLocked_idIsNotOldId()
        {
            _uut._state = StationControl.LadeskabState.Locked;
            int testId = 1234;
            _uut._oldId = testId;

            _rfidReader.RfidEvent += Raise.EventWith(new RfidEventArgs(5678));

            _display.Received(1).DisplayMessage("Ja, det virker så ikke. Prøv igen, taber");
        }
    }
}
