using AH.DeviceDriver.Share.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share.Hardware
{
    public interface IHardwareComponent : IComponent
    {
        /// <summary>
        /// 获取硬件的能力
        /// </summary>
        /// <returns></returns>
        List<EnumHardwareCapacity> GetCapacities();
        /// <summary>
        /// 是否自动操作
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>
        bool IsAutomatic(EnumHardwareCapacity capacity);
        /// <summary>
        /// 人工操作提示
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns></returns>
        string? ManualOperateNotification(EnumHardwareCapacity capacity);

    }


    public interface ILigthSource : IHardwareComponent
    {
    }

    public interface IOpticalPath : IHardwareComponent
    {
    }

    public interface ISamplePath : IHardwareComponent
    {
    }

    public interface IAccessory : IHardwareComponent
    {
    }

    public interface ISensor : IHardwareComponent
    {
    }

    public interface IInstrument : IHardwareComponent
    {
    }

    public interface IMeterSystem : IHardwareComponent
    {
    }
}
