using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab
{
    public interface IDisplay
    {

        public void showPhoneConnected();

        public void showConnectPhone();

        public void showReadRfid();

        public void showConnectionToPhoneFailed();

        public void showRemovePhone();

        public void showRfidMistake();

        void ChargeMessage(string Message);

    }
}
