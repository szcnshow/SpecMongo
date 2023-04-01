using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.DeviceDriver.Share.Argument;
using System.Threading;
using AH.DeviceDriver.Share.Hardware;

namespace AH.DeviceDriver.Share.Component
{
    public interface IComponent:IDisposable
    {
        Guid GetComponentID();
        EnumHardwareType GetComponentType();
        string GetComponentName();
        List<ArgumentItem> GetArguments();
        List<DataItem.DataItem> GetResults();        
        Task<List<DataItem.DataItem>> CallFunctionAsync(
            EnumComponentFunction func,
            Dictionary<string, dynamic>? args=null,
            List<DataItem.DataItem>? inputDatas=null,
            Delegates.NotifyAction? notification = null, 
            CancellationToken? cancelToken = null);
    }

}
