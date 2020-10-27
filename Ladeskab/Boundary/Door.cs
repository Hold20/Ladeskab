using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Ladeskab.Interface;

namespace Ladeskab.Boundary
{
    public class Door: IDoor
    {

        public bool DoorLocked{ get; set; }
        

        public void openDoor()
        {
            this.DoorLocked = true;
            Console.WriteLine("Døren er nu åben");
            DoorOpened(new doorOpenEventArgs());
        }

        public void closeDoor()
        {
            this.DoorLocked = false;
            Console.WriteLine("Døren er lukket");
            DoorClosed(new doorCloseEventArgs());
        }

        public void LockedDoor()
        {   
            DoorLocked = true;
        }

        public void UnlockedDoor()
        {
            DoorLocked = false;
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
