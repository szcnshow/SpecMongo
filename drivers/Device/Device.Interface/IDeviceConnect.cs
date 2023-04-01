using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AH.DeviceDriver.Interface
{
    public interface IDeviceConnect:IDeviceFunctionArguments
    {
        Task<Dictionary<string, dynamic>?> EnumerateDeviceAsync(Dictionary<string, dynamic>? args, CancellationToken? cancelToken = null);
        Task<Dictionary<string, dynamic>?> ConnectDeviceAsync(Dictionary<string, dynamic>? args, CancellationToken? cancelToken = null);
        Task<Dictionary<string, dynamic>?> DisconnectDeviceAsync(Dictionary<string, dynamic>? args, CancellationToken? cancelToken = null);
    }
}
