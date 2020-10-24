using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public interface Door: IDoor
    {
        void lockedDoor();

        void unlockedDoor();

        bool DoorLocked { get; }
        
        //dede

    }
}
