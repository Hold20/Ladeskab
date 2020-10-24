using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{

    public class RfidEventArgs : EventArgs
    {
        public bool Rfid { get; }
    }



    public interface IRfidReader
    {
        void RfidDetected(id);

        event EventHandler<RfidEventsArgs> RfidEvent;

        protected virtual void RfidDetectedEvent(doorOpenEventArgs);
    }
}
