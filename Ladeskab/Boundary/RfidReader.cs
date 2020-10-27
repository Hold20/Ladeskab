using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public class RfidReader: IRfidReader
    {

        public RfidReader()
        { 
        }

        public void RfidDetected(int id)
        {
            RfidDetectedEvent(new RfidEventArgs(id));
        }

        public event EventHandler<RfidEventArgs> RfidEvent;

        protected virtual void RfidDetectedEvent(RfidEventArgs e)
        {
            RfidEvent?.Invoke(this, e);
        }
    }

    public class RfidEventArgs : EventArgs
    {
        public int Id { get; }

        public RfidEventArgs(int id)
        {
            Id = id;
        }

    }

}
