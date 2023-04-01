using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Interface
{
    interface IDeviceEnum
    {
        Task InitEnumEnvironment(Dictionary<string, string> enumParameter, CancellationToken? token);
        Task<List<Device.Model.DeviceEnumResult>> EnumDevice(CancellationToken? token);
        Task ClearEnumEnviroment(CancellationToken? token);
    }
}
