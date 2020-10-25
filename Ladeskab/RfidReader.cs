using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public class RfidReader: IRfidReader
    {
        public void RfidDetected(int id)
        {

            string idString = System.Console.ReadLine();
            _ = Convert.ToInt32(idString);  

        }

        public event EventHandler<RfidEventArgs> RfidEvent;

        protected virtual void RfidDetectedEvent(RfidEventArgs e)
        {
            RfidEvent?.Invoke(this, e);
        }

        public bool Rfid { get; set; }
    }
}
