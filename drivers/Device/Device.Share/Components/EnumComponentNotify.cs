using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share.Component
{

    /// <summary>
    /// 硬件通知状态枚举
    /// </summary>
    public enum EnumComponentNotify
    {
        Initialized = 1,
        Enumerated = 3,
        Connected = 5,
        Prepared = 7,
        Processing = 9,
        StageFilished = 11,
        AllFilished = 13,
        InternalFailed = 15,
        UserAborted = 17,
    }
}
