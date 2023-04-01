using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.DeviceDriver.Share;
using AH.DeviceDriver.Share.Argument;
using AH.DeviceDriver.Share.Component;
using AH.DeviceDriver.Share.DataItem;
using AH.DeviceDriver.Share.Hardware;

namespace AH.DeviceDriver.Interface
{
    public interface IComponent
    {
        Guid GetComponentGuid();
        EnumHardwareType GetComponentType();
        string GetComponentName();
        List<EnumComponentFunction> GetFunctions();
        List<ArgumentItem> GetDefaultSettings();
        List<ArgumentItem> GetInputArguments(EnumComponentFunction func);
        List<DataItem> GetResultDatas(EnumComponentFunction func);        
        Task<List<DataItem>> CallFunctionAsync(
            EnumComponentFunction func,
            List<ArgumentBase> inputArgs,
            List<DataItem> inputDatas,
            Dictionary<string, dynamic>? args,
            Delegates.NotifyAction? notification = null, 
            CancellationToken? cancelToken = null);
    }

    public interface ILigthSource : IComponent
    {
    }

    public interface IOpticalPath : IComponent
    {
    }

    public interface ISamplePath : IComponent
    {
    }

    public interface IAccessory : IComponent
    {
    }

    public interface ISensor : IComponent
    {
    }

    public interface IInstrument : IComponent
    {
    }

    public interface IMeterSystem : IComponent
    {
    }
}
