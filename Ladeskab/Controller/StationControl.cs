using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ladeskab.Interface;
using Ladeskab.Boundary;


namespace Ladeskab.Controller
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        public LadeskabState _state;
        private IChargeControl _charger;
        public int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private IRfidReader _rfidReader;



        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

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
                _display.DisplayMessage("Forbind telefon");

                _state = LadeskabState.DoorOpen;
            }
            
        }

        private void DoorClosed(object sender, doorCloseEventArgs e)
        {
            if(_state == LadeskabState.DoorOpen)
            {
                _display.DisplayMessage("Tryk R for at indtaste kode og dermed låse");

                _state = LadeskabState.Available;
            }
        }

    

       
        private void RfidDetected(object rfidReader, RfidEventArgs e)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockedDoor();
                        _charger.StartCharge();
                        _oldId = e.Id;
                        _display.DisplayMessage("Telefonen er forbundet og du er grim");

                        
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.DisplayMessage("Der er ingen forbindelse til din telefon");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (e.Id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockedDoor();
                        _display.DisplayMessage("Fjern nu bare din telefon for helvede");

                        
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayMessage("Ja, det virker så ikke. Prøv igen, taber");
                    }

                    break;
            }
        }

        
    }
}
