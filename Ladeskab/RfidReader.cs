using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public class RfidReader: IRfidReader
    {
        public void RfidDetected(int id)
        {
            this.Rfid = true;
        }

        public event EventHandler<RfidEventArgs> RfidEvent;

        protected virtual void RfidDetectedEvent(RfidEventArgs e)
        {
            RfidEvent?.Invoke(this, e);
        }

        public bool Rfid { get; set; }
    }
}
