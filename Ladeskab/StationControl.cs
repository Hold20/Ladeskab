using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private IRfidReader _rfidReader;



        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl(IDoor door, IDisplay display, IRfidReader rfidReader, IChargeControl charger)
        {
            _display = display;
            _door = door;
            _rfidReader = rfidReader;
            _charger = charger;


            _rfidReader.RfidEvent += RfidDetected;
            _door.doorCloseEvent += DoorClosed;
            _door.doorOpenEvent += DoorOpen;

        }

        private void DoorOpen(object sender, doorOpenEventArgs e)
        {
            if(_state == LadeskabState.Available)
            {
                _display.showConnectPhone();

                _state = LadeskabState.DoorOpen;
            }
            
        }

        private void DoorClosed(object sender, doorCloseEventArgs e)
        {
            if(_state == LadeskabState.DoorOpen)
            {
                _display.showReadRfid();

                _state = LadeskabState.Available;
            }
        }

    

       
        private void RfidDetected(object rfidReader, RfidEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected)
                    {
                        _door.lockedDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        _display.showPhoneConnected();

                        
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.showConnectionToPhoneFailed
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.unlockedDoor();
                        _display.showRemovePhone();

                        
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.showRfidMistake();
                    }

                    break;
            }
        }

        
    }
}
