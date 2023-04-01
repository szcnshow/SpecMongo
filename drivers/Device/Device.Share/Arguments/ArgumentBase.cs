using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share.Argument
{
    public class ArgumentBase
    {
        /// <summary>
        /// 内部名称
        /// </summary>
        public string InnerName { get; set; } = null!;

        public string? Value { get; set; }

        /// <summary>
        /// 属性值类型
        /// </summary>
        public string ValueType { get; set; } = null!;

        /// <summary>
        /// 参数作用范围
        /// </summary>
        public EnumArgumentScope Scope { get; set; } = EnumArgumentScope.EachTime;

        /// <summary>
        /// 参数操作对象
        /// </summary>
        public EnumArgumentAuthor Author { get; set; } = EnumArgumentAuthor.Operator;

        public ArgumentBase() { }

        public ArgumentBase(string innerName, string valueType, string? value = null, EnumArgumentScope  scope= EnumArgumentScope.EachTime, EnumArgumentAuthor author= EnumArgumentAuthor.System)
        {
            this.InnerName = innerName;
            this.Value = value;
            this.ValueType = valueType;
            this.Scope = scope;
            this.Author = author;
        }
    }
}
