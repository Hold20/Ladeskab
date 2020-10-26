using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.UsbSimulator;

namespace Ladeskab
{
    public class ChargeControl: IChargeControl
    {
        private IUsbCharger _usbCharger;
        private IDisplay _display;

        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _usbCharger = usbCharger;
            _usbCharger.CurrentValueEvent += ReadCurrentValue;
            _display = display;
        }

        public void startCharge()
        {
            if (IsConnected())
            {
                _usbCharger.StartCharge();
            }
        }

        public void stopCharge()
        {
            if (IsConnected())
            {
                _usbCharger.StopCharge();
            }
        }

        private void ReadCurrentValue(object sender, CurrentEventArgs e)
        {
            if (!IsConnected())
            {
                return;
            }

            if (e.Current > 5 && e.Current <= 500)
            {
                _display.ChargingMessage("Ladestrømmen er " + e.Current.ToString("0.00") + "mA\r");
                return;
            }

            if (e.Current > 0 && e.Current <= 5)
            {
                _display.ChargingMessage("Fuldt opladet telefon og ladning stoppet\r");
                return;
            }

            if (e.Current > 500)
            {
                stopCharge();
                _display.ChargingMessage("Ladning stoppet\r");
            }

        }

    }
}
