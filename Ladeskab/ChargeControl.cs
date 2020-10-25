using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public class ChargeControl: IChargeControl
    {
        public bool IsConnected { get; set; }

        public void startCharge()
        {
            Console.WriteLine("Der lades nu");
        }

        public void stopCharge()
        {
            Console.WriteLine("Der lades ikke mere")
        }
    }
}
