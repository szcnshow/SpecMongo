using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share.Hardware
{
    public enum EnumHardwareType
    {
        /// <summary>
        /// 光源
        /// </summary>
        LigthSource,
        /// <summary>
        /// 光路
        /// </summary>
        OpticalPath,
        /// <summary>
        /// 样品通道
        /// </summary>
        SamplePath,
        /// <summary>
        /// 附件
        /// </summary>
        Accessory,
        /// <summary>
        /// 传感器
        /// </summary>
        Sensor,
        /// <summary>
        /// 主机
        /// </summary>
        Instrument,
        /// <summary>
        /// 仪器系统
        /// </summary>
        MeterSystem,
        /// <summary>
        /// 继电器
        /// </summary>
        Relay,
    }

}
