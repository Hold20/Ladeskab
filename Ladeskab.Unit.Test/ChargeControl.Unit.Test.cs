using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Controller;
using Ladeskab.Interface;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Unit.Test
{
    [TestFixture]
    class ChargeControlUnitTest
    {

        private ChargeControl _uut;
        private IUsbCharger _subUsbCharger;
        private IDisplay _subDisplay;

        [SetUp]
        public void setup()
        {
            _subUsbCharger = Substitute.For<IUsbCharger>();
            _subDisplay = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_subUsbCharger, _subDisplay);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsConnected_returns_CorrectValue(bool connect)
        {
            _subUsbCharger.Connected.Returns(connect);

            Assert.That(_uut.IsConnected(), Is.EqualTo(connect));
        }

        [Test]
        public void StartCharge_Called_CallsStartCharge()
        {
            _subUsbCharger.Connected.Returns(true);

            _uut.StartCharge();

            _subUsbCharger.Received().StartCharge();
        }

        public void StartCharge_NotCalled_NoStart()
        {
            _subUsbCharger.Connected.Returns(false);

            _uut.StartCharge();

            _subUsbCharger.DidNotReceive().StartCharge();
        }

        public void StopCharge_Called_CallsStopCharge()
        {
            _subUsbCharger.Connected.Returns(true);

            _uut.StopCharge();

            _subUsbCharger.Received().StopCharge();
        }

        public void StopCharge_NotCalled_NoStop()
        {
            _subUsbCharger.Connected.Returns(false);

            _uut.StopCharge();

            _subUsbCharger.DidNotReceive().StopCharge();
        }

        [TestCase(double.MaxValue, "Ladning stoppet")]
        [TestCase(510, "Ladning stoppet")]
        [TestCase(499, "Ladestrømmen er")]
        [TestCase(300, "Ladestrømmen er")]
        [TestCase(6, "Ladestrømmen er")]
        [TestCase(5, "Fuldt opladet telefon og ladning stoppet")]
        [TestCase(0.1, "Fuldt opladet telefon og ladning stoppet")]
        public void ReadCurrentValue_CallsDisplayCorrectly(double newCurrent, string message)
        {
            _subUsbCharger.Connected.Returns(true);

            _subUsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs()
            {
                Current = newCurrent
            });

            _subDisplay.Received().DisplayMessage(Arg.Any<string>());

        }

        [TestCase(double.MaxValue, "Ladning stoppet")]
        [TestCase(510, "Ladning stoppet")]
        [TestCase(499, "Ladestrømmen er")]
        [TestCase(300, "Ladestrømmen er")]
        [TestCase(6, "Ladestrømmen er")]
        [TestCase(5, "Fuldt opladet telefon og ladning stoppet")]
        [TestCase(0.1, "Fuldt opladet telefon og ladning stoppet")]
        public void ReadCurrentValue__Disconnected_CallsDisplayCorrectly(double newCurrent, string message)
        {
            _subUsbCharger.Connected.Returns(false);

            _subUsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs()
            {
                Current = newCurrent
            });

            _subDisplay.DidNotReceive().DisplayMessage(Arg.Any<string>());

        }


        [TestCase(double.MaxValue)]
        [TestCase(510)]
        [TestCase(5)]
        [TestCase(0.1)]
        public void ReadCurrentValue_StopChargeCalled(double newCurrent)
        {
            _subUsbCharger.Connected.Returns(true);

            _subUsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs()
            {
                Current = newCurrent
            });

            _subUsbCharger.Received(1).StopCharge();
        }


        [TestCase(500)]
        [TestCase(300)]
        [TestCase(200)]
        public void ReadCurrentValue_StopChargeNotCalled(double newCurrent)
        {
            _subUsbCharger.Connected.Returns(true);

            _subUsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs()
            {
                Current = newCurrent
            });

            _subUsbCharger.DidNotReceive().StopCharge();
        }

        [TestCase(500)]
        [TestCase(300)]
        [TestCase(200)]
        public void ReadCurrentValue__Disconnected_StopChargeNotCalled(double newCurrent)
        {
            _subUsbCharger.Connected.Returns(false);

            _subUsbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs()
            {
                Current = newCurrent
            });

            _subUsbCharger.DidNotReceive().StopCharge();
        }

    }
}