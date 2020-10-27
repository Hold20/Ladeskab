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

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            
        }
    }
}
