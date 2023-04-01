using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Driver.Share.DataItem
{
    /// <summary>
    /// 流程中传递的数据类型
    /// </summary>
    public enum EnumDataType
    {
        /// <summary>
        /// 基础数据，是bool,short,int,float等基础数据
        /// </summary>
        [Description("基础数据, Base Data")]
        BaseData,
        /// <summary>
        /// NormalJson，常规的Json数据
        /// </summary>
        [Description("常规数据,Normal Json")]
        NormalJson,
        /// <summary>
        /// 结果光谱
        /// </summary>
        [Description("结果光谱,Result Spectrum")]
        ResultSpectrum,
        /// <summary>
        /// 过程光谱
        /// </summary>
        [Description("过程光谱,Internal Spectrum")]
        InternalSpectrum,
        /// <summary>
        /// 定量分析结果
        /// </summary>
        [Description("定量分析结果,Quant Result")]
        QuantResult,
        /// <summary>
        /// 定性分析结果
        /// </summary>
        [Description("定性分析结果,Ident Result")]
        IdentResult,
        /// <summary>
        /// Property，包括名称，配置等等
        /// </summary>
        [Description("属性,Property")]
        Property,
        /// <summary>
        /// Status，包括执行状态、温度、各种参数等等
        /// </summary>
        [Description("状态,Node NodeStatus")]
        Status,
        /// <summary>
        /// 节点执行结果代码（枚举：正常，警告，错误等）
        /// </summary>
        [Description("执行结果代码,Exit Code")]
        ExitCode,
        /// <summary>
        /// 节点执行结果信息
        /// </summary>
        [Description("执行结果信息,Exit Message")]
        ExitMessage,
    }
}
