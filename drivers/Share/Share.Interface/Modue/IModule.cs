using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.Driver.Share.Argument;
using System.Threading;

namespace AH.Driver.Share.Module
{
    /// <summary>
    /// 所有模块的基类
    /// </summary>
    public interface IModule:IDisposable
    {
        /// <summary>
        /// 获取模块ID
        /// </summary>
        /// <returns></returns>
        Guid GeModuelID();
        /// <summary>
        /// 获取模块大类型
        /// </summary>
        /// <returns></returns>
        EnumModuleRole GeModuelRole();
        /// <summary>
        /// 获取子模块
        /// </summary>
        /// <returns></returns>
        List<IModule> GetChildren();
        /// <summary>
        /// 获取模块的名称
        /// </summary>
        /// <returns></returns>
        string GeModuelName();
        /// <summary>
        /// 获取模块参数设置（包括子模块）
        /// </summary>
        /// <returns></returns>
        List<ArgumentItem> GetArguments();
        /// <summary>
        /// 获取模块输出数据设置（包括子模块）
        /// </summary>
        /// <returns></returns>
        List<DataItem.DataItem> GetResults();
        /// <summary>
        /// 操作模块
        /// </summary>
        /// <param name="command">操作命令</param>
        /// <param name="args">操作参数</param>
        /// <param name="inputDatas">输入数据</param>
        /// <param name="callback">操作过程回调</param>
        /// <param name="cancelToken">取消操作通知</param>
        /// <returns></returns>
        Task<List<DataItem.DataItem>> CallFunctionAsync(
            EnumModuleCommand command,
            Dictionary<string, dynamic>? args=null,
            List<DataItem.DataItem>? inputDatas=null,
            Delegates.NotifyCallback? callback = null, 
            CancellationToken? cancelToken = null);
    }

}
