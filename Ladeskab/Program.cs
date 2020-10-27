using System;
using Ladeskab;
using Ladeskab.UsbSimulator;

namespace Ladeskab
{
    class Program
    {
        static void Main(string[] args)
        {
            Door door = new Door();
            Display display = new Display();
            RfidReader rfidReader = new RfidReader();
            UsbChargerSimulator usbCharger = new UsbChargerSimulator();
            ChargeControl charger = new ChargeControl(usbCharger, display);
            StationControl station = new StationControl(door, display, rfidReader, charger);
            

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.openDoor();
                        break;

                    case 'C':
                        door.closeDoor();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.RfidDetected(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
