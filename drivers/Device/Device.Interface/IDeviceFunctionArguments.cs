using System;
using System.Collections.Generic;
using System.Text;

namespace AH.DeviceDriver.Interface
{
    /// <summary>
    /// 获取设备function调用和返回参数
    /// </summary>
    public interface IDeviceFunctionArguments
    {
        Dictionary<string, string>? GetArguments(string functionName, bool inArgs);
    }
}
