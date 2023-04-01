using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Interface
{
    public interface IDeviceEnvironment
    {
        Task<Dictionary<string, dynamic>?> InitEnvironmentAsync(Dictionary<string, dynamic>? args, CancellationToken? cancelToken = null);
        Task<Dictionary<string, dynamic>?> ClearEnvironmentAsync(Dictionary<string, dynamic>? args, CancellationToken? cancelToken = null);
        Dictionary<string, string>? GetArguments(string functionName, bool inArgs);
    }
}
