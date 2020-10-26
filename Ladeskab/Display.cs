using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public class Display: IDisplay
    {

        public void showConnectPhone()
        {
            Console.WriteLine("Tilslut telefon");
        }

        public void showReadRfid()
        {
            Console.WriteLine("Indlaes RFID");
        }

        public void showConnectionToPhoneFailed()
        {
            Console.WriteLine("Tilslutningsfejl");
        }

        public void showChargerCabinetIsOccupied()
        {
            Console.WriteLine("Ladeskab optaget");
        }

        public void showRfidMistake()
        {
            Console.WriteLine("RFID fejl");
        }

        public void showRemovePhone()
        {
            Console.WriteLine("Fjern telefon");
        }

        public void showPhoneConnected()
        {
            Console.WriteLine("Telefon er forbundet og lader");
        }


    }
}
