using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share.DataItem
{
    /// <summary>
    /// 流程中传递的数据类型
    /// </summary>
    public enum EnumDataType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知,Unknown")]
        Unknown,
        /// <summary>
        /// 计时数据
        /// </summary>
        [Description("计时,Timer")]
        Timer,
        /// <summary>
        /// 循环计数
        /// </summary>
        [Description("运行次数,Counter")]
        Counter,
        /// <summary>
        /// 暗电流
        /// </summary>
        [Description("暗电流,Dark Spec")]
        SpecDarkFile,
        /// <summary>
        /// 背景谱
        /// </summary>
        [Description("背景谱,Background Spec")]
        SpecBackgroundFile,
        /// <summary>
        /// 参考谱图
        /// </summary>
        [Description("参考谱图,Reference Spec")]
        SpecRefFile,
        /// <summary>
        /// 原始谱图
        /// </summary>
        [Description("原始谱图,Raw Spec")]
        SpecRawFile,
        /// <summary>
        /// 结果谱图
        /// </summary>
        [Description("结果谱图,Result Spec")]
        SpecResultFile,
        /// <summary>
        /// 中间谱图
        /// </summary>
        [Description("中间谱图,Internal Spec")]
        SpecInternalFile,
        /// <summary>
        /// 主机地址
        /// </summary>
        [Description("主机地址,Host Address")]
        HostAddress,
        /// <summary>
        /// 交换数据，是bool,short,int,float等基础数据
        /// </summary>
        [Description("交换数据,Exchange Data")]
        ExchangeData,
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
        /// 节点执行结果
        /// </summary>
        [Description("节点执行结果,Execute Result")]
        ExecuteResult,
        /// <summary>
        /// 传输下载结果
        /// </summary>
        [Description("传输下载结果,Transfer Download")]
        TransferDownload,
        /// <summary>
        /// NodeProperty，节点属性，包括节点名称，配置等等
        /// </summary>
        [Description("节点属性,Node Property")]
        NodeProperty,
        /// <summary>
        /// NodeStatus，节点状态，包括节点执行状态、温度、各种参数等等
        /// </summary>
        [Description("节点状态,Node NodeStatus")]
        NodeStatus,
    }
}
