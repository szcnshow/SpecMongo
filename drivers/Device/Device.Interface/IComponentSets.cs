using AH.DeviceDriver.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Interface
{
    public interface IComponentSets
    {
        string GetName();
        bool AddComponent(IComponent components);
        bool AddComponentSets(IComponentSets components);
        List<IComponent> GetComponents();
        List<IComponentSets> GetComponentSets();
        EnumComponentType GetComponentType();
        string GetComponentName();
        List<EnumComponentFunction> GetFunctions();
        Dictionary<EnumComponentType, List<ArgumentItem>> GetDefaultSettings();
        Dictionary<EnumComponentType, Dictionary<string, string>> GetInputArguments(EnumComponentFunction func);
        Dictionary<EnumComponentType, Dictionary<string, string>> GetResultArguments(EnumComponentFunction func);
        Task<Dictionary<EnumComponentType, Dictionary<string, dynamic>>> CallFunctionAsync(
            EnumComponentFunction func,
            Dictionary<EnumComponentType, Dictionary<string, dynamic>>? args,
            Delegates.NotifyAction? notification = null,
            CancellationToken? cancelToken = null);
    }
}
