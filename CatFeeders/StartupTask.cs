using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using Windows.System.Threading;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace CatFeeders
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        GpioController gpio = GpioController.GetDefault();
        int[] pinNumbers = new int[] { 17, 27 , 22 };
        GpioPin[] pins = new GpioPin[3];

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            foreach (int i in pinNumbers)
            {
                using (GpioPin p = gpio.OpenPin(i))
                {
                    p.Write(GpioPinValue.Low);
                    p.SetDriveMode(GpioPinDriveMode.Output);
                    System.Threading.Tasks.Task.Delay(5000).Wait();
                }
            }
        }
    }
}
