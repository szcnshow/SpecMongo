using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share.Argument
{
    /// <summary>
    /// 参数作用范围
    /// </summary>
    public enum EnumArgumentScope
    {
        /// <summary>
        /// 需要每次设置
        /// </summary>
        EachTime = 0,
        /// <summary>
        /// 相同Guid只设置一次
        /// </summary>
        SameGuid = 2,
        /// <summary>
        /// 相同类型的组件只设置一次
        /// </summary>
        SameComponent = 4,
    }

    /// <summary>
    /// 参数修改权限
    /// </summary>
    public enum EnumArgumentAuthor
    {
        /// <summary>
        /// 使用者
        /// </summary>
        Operator = 0,
        /// <summary>
        /// 管理员
        /// </summary>
        Administrator = 2,
        /// <summary>
        /// 系统
        /// </summary>
        System = 4,
        /// <summary>
        /// 应用前面的值
        /// </summary>
        Reference = 6
    }
}
