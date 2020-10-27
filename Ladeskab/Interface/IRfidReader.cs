using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Boundary;

namespace Ladeskab.Interface
{

    public interface IRfidReader
    {
        void RfidDetected(int id);

        event EventHandler<RfidEventArgs> RfidEvent;

    }
}
