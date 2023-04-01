using System;
using System.Collections.Generic;
using System.Text;

namespace AH.DeviceDriver.Interface
{
    interface IDeviceProperty
    {
        Dictionary<string, string> GetProperties();
    }
}
