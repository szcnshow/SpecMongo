using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AH.Driver.Share.Argument
{

    /// <summary>
    /// 信息的基础类，
    /// 序列化时，只序列化InnerName和Value，
    /// 反序列化后，需要调用方通过InnerName来提供ChineseName等其它信息，
    /// 这样可以减少序列化后的数据量
    /// </summary>
    public class ArgumentItem:ArgumentBase
    {
        #region properties

        /// <summary>
        /// 显示名称
        /// </summary>
        [JsonIgnore]
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// 是否可用
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// 选项列表(Key=值, Value=显示）
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, string>? Selections { get; set; }

        /// <summary>
        /// 是否可以录入
        /// </summary>
        [JsonIgnore]
        public bool Inputable { get; set; }=false;

        /// <summary>
        /// 是否为列表项
        /// </summary>
        [JsonIgnore]
        public bool IsSelection { get { return Selections != null; } }

        /// <summary>
        /// 属性最小值
        /// </summary>
        [JsonIgnore]
        public float MinValue { get; set; } = float.MinValue;

        /// <summary>
        /// 属性最大值
        /// </summary>
        [JsonIgnore]
        public float MaxValue { get; set; } = float.MaxValue;

        /// <summary>
        /// 是否为文件名项(将会夸两列，还显示文件浏览按钮)
        /// 如果IsCommandButton=true, IsFilename=true表示占用整行，IsFilename=false表示占用一个属性位置
        /// </summary>
        [JsonIgnore]
        public bool IsFilename { get; set; } = false;

        /// <summary>
        /// 显示的时候是否需要新起一行
        /// </summary>
        [JsonIgnore]
        public bool NeedNewLine { get; set; } = false;

        /// <summary>
        /// 本属性显示的关联属性名称, 必须是InnerName, 比如: IsSelected. 目前针对Boolean类型和有下拉列表选项的类型
        /// NULL表示无条件显示
        /// </summary>
        [JsonIgnore]
        public string? VisibleSource { get; set; }

        /// <summary>
        /// 本属性显示的关联属性值，比如:True, 目前针对Boolean类型和有下拉列表选项的类型
        /// </summary>
        [JsonIgnore]
        public string? VisibleValue { get; set; }

        /// <summary>
        /// True:设定值与属性值相等是显示, False=设定值与属性值不等时显示,缺省:True
        /// </summary>
        [JsonIgnore]
        public bool VisibleEqual { get; set; } = true;

        /// <summary>
        /// 是否为DataGrid数据，如果是，则Value的值为JSon格式的Dictionary，
        /// </summary>
        [JsonIgnore]
        public bool IsDataGrid { get; set; } = false;

        /// <summary>
        /// 是否只读，2021-01-15添加
        /// </summary>
        [JsonIgnore]
        public bool IsReadonly { get; set; } = false;
        #endregion

        public ArgumentItem():base()
        {
        }

        public ArgumentItem(string innerName, string valueType, string? value = null, EnumArgumentScope scope = EnumArgumentScope.Transient, EnumArgumentAuthor author = EnumArgumentAuthor.System)
            :base(innerName, valueType, value, scope, author)
        {
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public ArgumentItem Clone()
        {
            var ret = this.MemberwiseClone() as ArgumentItem;
            return ret ?? new ArgumentItem();
        }

        /// <summary>
        /// 拷贝除值之外的全部属性
        /// </summary>
        /// <param name="sourceInfo"></param>
        public void CopyWithoutValue(ArgumentItem sourceInfo)
        {
            var props = typeof(ArgumentItem).GetProperties();
            foreach (var prop in props)
            {
                if (prop.CanWrite && prop.Name != nameof(Value))
                {
                    var value = prop.GetValue(this);
                    prop.SetValue(sourceInfo, value);
                }
            }
        }

        /// <summary>
        /// 根据缺省的属性，初始化加载的属性
        /// 用于在Deserialize后，参数的值与固定的参数属性结合起来
        /// </summary>
        /// <param name="loadedProperties">加载的样品信息</param>
        /// <param name="defaultProperties">缺省的样品信息</param>
        /// <returns></returns>
        public static List<ArgumentItem> CombineProperties(List<ArgumentItem> loadedProperties, List<ArgumentItem> defaultProperties)
        {
            List< ArgumentItem> retDatas = new List<ArgumentItem>();
            if (loadedProperties == null)
            {
                retDatas.AddRange(defaultProperties);
                return retDatas;
            }

            foreach (var item in defaultProperties)
            {
                var info = item.Clone();
                var oldinfo = loadedProperties.FirstOrDefault(p => p.InnerName == item.InnerName);
                if (oldinfo != null)
                    info.Value = oldinfo.Value;

                retDatas.Add(info);
            }

            return retDatas;
        }
    };
}
