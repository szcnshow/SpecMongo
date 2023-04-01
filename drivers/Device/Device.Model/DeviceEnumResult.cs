using System;

namespace Device.Model
{
    public class DeviceEnumResult
    {
        public string Name { get; set; }
        public string SerialNo { get; set; }

        /// <summary>
        /// 仪器厂商
        /// </summary>
        public string Factory { get; set; }
        /// <summary>
        /// 仪器商标,同一个厂商的不同仪器品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 仪器种类
        /// </summary>
        public EnumDeviceCategory Category { get; set; }
        /// <summary>
        /// 仪器型号
        /// </summary>
        public EnumDeviceModel Model { get; set; }
    }
}
