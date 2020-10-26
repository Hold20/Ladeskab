using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Ladeskab
{
    public class Door: IDoor
    {

        public bool DoorLocked{ get; set; }
        private IDisplay _DisplayDoor;

        public void lockedDoor()
        {
            this.DoorLocked = true;
            _DisplayDoor.showDoorLocked();
        }

        public void unlockedDoor()
        {
            this.DoorLocked = false;
            _DisplayDoor.showDoorUnlocked();

        }


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
