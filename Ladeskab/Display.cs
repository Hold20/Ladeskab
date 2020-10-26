using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Ladeskab
{
    public class Display: IDisplay
    {

        public void ChargingMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
