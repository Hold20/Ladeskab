using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Interface;
using Ladeskab.Boundary;

namespace Ladeskab.Controller
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

        public void StartCharge()
        {
            if (IsConnected())
            {
                _usbCharger.StartCharge();
            }
        }

        public void StopCharge()
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
                _display.DisplayMessage("Ladestrømmen er " + e.Current.ToString("0.00") + "mA\r");
                return;
            }

            if (e.Current > 0 && e.Current <= 5)
            {
                StopCharge();
                _display.DisplayMessage("Fuldt opladet telefon og ladning stoppet\r");
                return;
            }

            if (e.Current > 500)
            {
                StopCharge();
                _display.DisplayMessage("Ladning stoppet\r");
            }

        }

    }
}
