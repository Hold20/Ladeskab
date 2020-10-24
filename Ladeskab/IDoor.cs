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

        event EventHandler<doorCloseEventArgs> doorCloseEvent;

        event EventHandler<doorOpenEventArgs> doorOpenEvent;

        protected virtual void DoorOpened(doorOpenEventArgs);

        protected virtual void DoorClosed(doorCloseEventsArgs);
    }
}
