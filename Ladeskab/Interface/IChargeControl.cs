using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Boundary;

namespace Ladeskab.Interface
{
   public interface IChargeControl
   {
       public bool IsConnected();

        public void StartCharge();

        public void StopCharge();
    }
}
