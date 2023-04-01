using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AH.DeviceDriver.Share.DataItem
{
    /// <summary>
    /// 输出数据类型
    /// </summary>
    public class DataItem
    {
        public string DataName { get; set; } = null!;
        /// <summary>
        /// 数据ID（每一个新数据都会有不同的ID）
        /// </summary>
        public Guid DataID { get; set; }

        /// <summary>
        /// 数据项类型
        /// </summary>
        public EnumDataType DataType { get; set; }

        /// <summary>
        /// 数据项的值
        /// </summary>
        public dynamic? DataValue { get; set; }

        /// <summary>
        /// 数据项拥有者
        /// </summary>
        public Guid DataOwner { get; set; }

        /// <summary>
        /// 数据项消费者列表
        /// </summary>
        public List<Guid>? DataConsumer { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string? ValueType { get; set; }

    }
}
