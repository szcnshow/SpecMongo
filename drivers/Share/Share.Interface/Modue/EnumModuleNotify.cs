using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Driver.Share.Module
{
    /// <summary>
    /// 硬件通知状态枚举
    /// </summary>
    public enum EnumModuleNotify
    {
        Initialized = 1,
        Enumerated = 3,
        Connected = 5,
        Prepared = 7,
        Processing = 9,
        StageFilished = 11,
        AllFilished = 13,
        InternalFailed = 15,
        UserAborted = 157,
    }
}
