using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Ladeskab
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
            Console.WriteLine("Indlæs RFID klammo");
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

        public void showDoorLocked()
        {
            Console.WriteLine("Døren er låst. Så kan du jo prøve at se om du kan åbne den");
        }

        public void showDoorUnlocked()
        {
            Console.WriteLine("Nu er døren så låst op, prøv liiiiige at åbne den igen");
        }
    }
}
