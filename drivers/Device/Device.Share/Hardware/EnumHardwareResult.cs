using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share.Hardware
{
    public enum EnumHardwareCapacity
    {
        Dark = 1,
        Background = 2,
        Reference = 4,
        Sample = 8,
        Temperature = 16,
        Humidity = 32,
        Pressure = 64,
    }
}
