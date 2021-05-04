using nanoFramework.Hardware.Esp32.Rmt;
using System;
using System.Threading;

namespace NFApp1
{
    public class HCSR04
    {
        ReceiverChannel _rxChannel;
        TransmitterChannel _txChannel;
        RmtCommand _txPulse;

        const float _speedOfSound = 340.29F;

        public HCSR04(int TxPin, int RxPin)
        {
            _txChannel = new TransmitterChannel(TxPin);

            _txPulse = new RmtCommand(10, true, 10, false);
            _txChannel.AddCommand(_txPulse);
            _txChannel.AddCommand(new RmtCommand(20, true, 15, false));

            _txChannel.ClockDivider = 80;
            _txChannel.CarrierEnabled = false;
            _txChannel.IdleLevel = false;

            // The received echo pulse width represents the distance to obstacle
            // 150us to 38ms
            _rxChannel = new ReceiverChannel(RxPin);

            _rxChannel.ClockDivider = 80; // 1us clock ( 80Mhz / 80 ) = 1Mhz
            _rxChannel.EnableFilter(true, 100); // filter out 100Us / noise 
            _rxChannel.SetIdleThresold(40000);  // 40ms based on 1us clock
            _rxChannel.ReceiveTimeout = new TimeSpan(0, 0, 0, 0, 60);
        }

        public float ObterDistancia()
        {
            RmtCommand[] response = null;

            _rxChannel.Start(true);

            // Send 10us pulse
            _txChannel.Send(false);

            // Try 5 times to get valid response
            for (int count = 0; count < 5; count++)
            {
                response = _rxChannel.GetAllItems();
                if (response != null)
                    break;

                // Retry every 60 ms
                Thread.Sleep(60);
            }

            _rxChannel.Stop();

            if (response == null)
                return -1;

            // Echo pulse width in micro seconds
            int duracao = response[0].Duration0;

            var distancia = duracao / 58;

            return distancia;

            // Calculate distance in meters
            // Distance calculated as  (speed of sound) * duration(meters) / 2 
            //return _speedOfSound * duration / (1000000 * 2);
        }
    }
}
