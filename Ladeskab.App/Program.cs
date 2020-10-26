using System;
using Ladeskab;

namespace Ladeskab
{
    class Program
    {
        static void Main(string[] args)
        {
            Door door = new Door();
            Display display = new Display();
            RfidReader rfidReader = new RfidReader();
            UsbChargerSimulator charger = new UsbChargerSimulator();
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
                        door.OnDoorOpen();
                        break;

                    case 'C':
                        door.OnDoorClose();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.OnRfidRead(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
