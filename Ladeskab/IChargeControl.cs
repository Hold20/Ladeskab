using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
   public interface IChargeControl
    {
        public bool IsConnected { get; set; }

        public void startCharge();

        public void stopCharge();
    }
}
