using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using Windows.System.Threading;
using System.IO;


// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace CatFeeders
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        GpioController gpio = GpioController.GetDefault();
        int[] pinNumbers = new int[] { 17, 27, 22 };
        GpioPin[] pins = new GpioPin[3];

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            WriteLog("Starting");
            foreach (int i in pinNumbers)
            {
                using (GpioPin p = gpio.OpenPin(i))
                {
                    p.Write(GpioPinValue.Low);
                    p.SetDriveMode(GpioPinDriveMode.Output);
                    WriteLog("Ran pin " + p.PinNumber.ToString());
                    System.Threading.Tasks.Task.Delay(4000).Wait();
                }
            }
            WriteLog("Finished");
            System.Threading.Tasks.Task.Delay(4000).Wait();
            deferral.Complete();
        }
        private static string GetNow()
        {
            string datePatt = @"M/d/yyyy hh:mm:ss tt";
            DateTime currtime = DateTime.Now;
            string now = currtime.ToString(datePatt);
            return now;
        }
        private void WriteLog(string message)
        {
            File.AppendAllText("C:\\logs\\feeder.log", GetNow()+" : "+message + Environment.NewLine);
        }
    }
}
