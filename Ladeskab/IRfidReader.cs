using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{

    public interface IRfidReader
    {
        void RfidDetected(int id);

        event EventHandler<RfidEventArgs> RfidEvent;

    }
}
