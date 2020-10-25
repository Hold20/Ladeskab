using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Ladeskab
{
    public class Door: IDoor
    {
        public void lockedDoor()
        {
            this.DoorLocked = true;
        }

        public void unlockedDoor()
        {
            this.DoorLocked = false;
        }

        public bool DoorLocked 
        { get; set; }

        public event EventHandler<doorCloseEventArgs> doorCloseEvent;

        public event EventHandler<doorOpenEventArgs> doorOpenEvent;

        protected virtual void DoorOpened(doorOpenEventArgs e)
        {
            doorOpenEvent?.Invoke(sender:this, e);
        }

        protected virtual void DoorClosed(doorCloseEventArgs e)
        {
            doorCloseEvent?.Invoke(sender:this, e);
        }

    }
}
