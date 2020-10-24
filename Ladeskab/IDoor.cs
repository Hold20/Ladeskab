using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public interface IDoor
    {
        void lockedDoor();

        void unlockedDoor();

        bool DoorLocked { get; }
    }
}
