using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Ladeskab
{

    public class doorOpenEventArgs : EventArgs
    {
        public bool door { get; }
    }

    public class doorCloseEventArgs : EventArgs
    {
        public bool door { get; }
    }


    public interface IDoor
    {
        public void closeDoor();

        public void openDoor();

        public bool DoorLocked { get; }

        public void LockedDoor();

        public void UnlockedDoor();

        public event EventHandler<doorCloseEventArgs> doorCloseEvent;

        public event EventHandler<doorOpenEventArgs> doorOpenEvent;

    }
}
