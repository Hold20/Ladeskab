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
        void lockedDoor();

        void unlockedDoor();

        bool DoorLocked { get; }

        event EventHandler<doorCloseEventArgs> doorCloseEvent;

        event EventHandler<doorOpenEventArgs> doorOpenEvent;

        protected virtual void DoorOpened(doorOpenEventArgs);

        protected virtual void DoorClosed(doorCloseEventArgs);
    }
}
