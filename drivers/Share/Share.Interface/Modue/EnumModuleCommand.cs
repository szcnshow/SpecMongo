using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Driver.Share.Module
{
    public enum EnumModuleCommand
    {
        /// <summary>
        /// 枚举
        /// </summary>
        Enumerate = 0,
        /// <summary>
        /// 初始化
        /// </summary>
        Initialize = 1,
        /// <summary>
        /// 操作
        /// </summary>
        Operate = 2,
        /// <summary>
        /// 设备属性
        /// </summary>
        GetProperty = 3,
        /// <summary>
        /// 设状态
        /// </summary>
        GetStatus = 4,
        /// <summary>
        /// 关闭
        /// </summary>
        Terminate = 5,
        /// <summary>
        /// 附加
        /// </summary>
        Extensional = 6
    }
}
