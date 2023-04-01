using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPIOControl
{
    internal class GpioControl:IDisposable
    {
        GpioController ctrl = new GpioController(PinNumberingScheme.Logical);

        Dictionary<int, bool> ports;

        public GpioControl(Dictionary<int, bool> ports)
        {
            this.ports = ports;
            foreach(var (key, _) in ports)
            {
                ctrl.OpenPin(key);
                ctrl.SetPinMode(key, PinMode.Output);
            }
        }

        public void OperatePorts(int repeat, int delay)
        {
            Console.Write($"repeat={repeat} delay={delay}");
            for(int i=0; i<repeat; i++)
            {
                Console.Write($"{i}/{repeat}, ");
                foreach(var (key, value) in ports)
                {
                    ctrl.Write(key, value? PinValue.High:PinValue.Low);
                }
                Thread.Sleep(delay*1000);

                if (repeat == 0)
                    break;

                foreach (var (key, value) in ports)
                {
                    ctrl.Write(key, value ? PinValue.Low : PinValue.High);
                }
                Thread.Sleep(delay * 1000);
            }
        }

        public void Dispose()
        {
            foreach (var (key, _) in ports)
            {
                ctrl.ClosePin(key);
            }
            ctrl.Dispose();
        }
    }
}
