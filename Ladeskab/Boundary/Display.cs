using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Ladeskab.Interface;

namespace Ladeskab.Boundary
{
    public class Display: IDisplay
    {
        public void showPhoneConnected()
        {
            Console.WriteLine("Telefonen er forbundet og du er grim");
        }

        public void showConnectPhone()
        {
            Console.WriteLine("Forbind telefon din amøbe");
        }

        public void showReadRfid()
        {
            Console.WriteLine("Tryk R for at indtaste kode og dermed låse");
        }

        public void showConnectionToPhoneFailed()
        {
            Console.WriteLine("Der er ingen forbindelse til din telefon, tag dig lige sammen");
        }

        public void showRemovePhone()
        {
            Console.WriteLine("Fjern nu bare din telefon for helvede");
        }

        public void showRfidMistake()
        {
            Console.WriteLine("Ja, det virker så ikke. Prøv igen, taber");
        }

        public void ChargeMessage(string message)
        {
            Console.WriteLine(message);
            
        }
    }
}
