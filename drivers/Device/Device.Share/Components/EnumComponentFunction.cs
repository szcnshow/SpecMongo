using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share.Component
{
    public enum EnumComponentFunction
    {
        /// <summary>
        /// 枚举
        /// </summary>
        EnumerateAsync = 0,
        /// <summary>
        /// 初始化
        /// </summary>
        InitializeAsync = 2,
        /// <summary>
        /// 操作
        /// </summary>
        OperateAsync = 4,
        /// <summary>
        /// 设备属性
        /// </summary>
        GetPropertyAsync = 6,
        /// <summary>
        /// 设状态
        /// </summary>
        GetStatusAsync = 8,
        /// <summary>
        /// 关闭
        /// </summary>
        TerminateAsync = 10,
        /// <summary>
        /// 附加
        /// </summary>
        ExtensionalAsync = 12
    }
}
