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

        public event EventHandler<CurrentEventArgs> CurrentValueEvent;

        public double CurrentValue { get; private set; }

        public bool Connected { get; private set; }

        private bool _overload;
        private bool _charging;
        private System.Timers.Timer _timer;
        private int _ticksSinceStart;

        public ChargeControl()
        {
            CurrentValue = 0.0;
            Connected = true;
            _overload = false;

            _timer = new System.Timers.Timer();
            _timer.Enabled = false;
            _timer.Interval = CurrentTickInterval;
            _timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            // Only execute if charging
            if (_charging)
            {
                _ticksSinceStart++;
                if (Connected && !_overload)
                {
                    double newValue = MaxCurrent - 
                                      _ticksSinceStart * (MaxCurrent - FullyChargedCurrent) / (ChargeTimeMinutes * 60 * 1000 / CurrentTickInterval);
                    CurrentValue = Math.Max(newValue, FullyChargedCurrent);
                }
                else if (Connected && _overload)
                {
                    CurrentValue = OverloadCurrent;
                }
                else if (!Connected)
                {
                    CurrentValue = 0.0;
                }

                OnNewCurrent();
            }
        }

        public void SimulateConnected(bool connected)
        {
            Connected = connected;
        }

        public void SimulateOverload(bool overload)
        {
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
