using System.Device.Gpio;

namespace GPIOControl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //判断Raspberry pi
                if (System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString().Contains("Arm") == false)
                {
                    Console.WriteLine("Only run on raspberry pi");
                    return;
                }

                //显示软件用法
                if (args.Length == 0 || args[0] == "-h" || args[0] == "h")
                {
                    Console.WriteLine("Usage:");
                    Console.WriteLine("power off: 16(portnumber)=on");
                    Console.WriteLine("Power on:  17(portnumber)=off");
                    Console.WriteLine("multi ports:  13=on  14=off  15=on  16=off repeat=1000 delay=1(s)");
                    Console.WriteLine("source=16, inner=27,18, outer=17,15");
                    Console.WriteLine("help: h");

                    return;
                }

                //解析参数
                Dictionary<int, bool> ports = new Dictionary<int, bool>();
                int repeat = 0;
                int delay = 0;
                foreach (var arg in args)
                {
                    var tempstr = arg.Split('=');
                    if (tempstr.Length == 2)
                    {
                        if (tempstr[0] == "repeat")
                            repeat = int.Parse(tempstr[1]);
                        else if (tempstr[0] == "delay")
                            delay = int.Parse(tempstr[1]);
                        else if (int.TryParse(tempstr[0], out int portnumber))
                        {
                            if (string.Compare(tempstr[1], "on", true) == 0)
                                ports.Add(portnumber, true);
                            else if (string.Compare(tempstr[1], "off", true) == 0)
                                ports.Add(portnumber, false);
                        }
                    }
                }

                GpioControl ctrl = new GpioControl(ports);
                ctrl.OperatePorts(repeat, delay);
                ctrl.Dispose();

                Console.WriteLine("Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}