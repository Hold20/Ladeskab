using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public interface IDisplay
    {
        public void showConnectPhone();

        public void showReadRfid();

        public void showConnectionToPhoneFailed();

        public void showChargerCabinetIsOccupied();

        public void showRfidMistake();

        public void showRemovePhone();

    }
}
