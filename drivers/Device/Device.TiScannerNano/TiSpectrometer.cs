using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AH.DeviceDriver.Interface;
using AH.DeviceDriver.Share;
using Newtonsoft.Json;

namespace AH.DeviceDriver.TiScannerNano
{
    public class TiSpectrometerRpi: IMeterSystem
    {
        public string GetComponentName() => "TiSpectrometerRpi";
        private const EnumComponentType curCompType = EnumComponentType.MeterSystem;
        private List<Interface.IComponent> _components;

        public TiSpectrometerRpi(List<Interface.IComponent> components)
        {
            //每一种ComponentType不能重复
            var comptypes = components.Select(p=>p.GetComponentType()).ToList();
            if (comptypes.Distinct().Count() != components.Count)
                throw new ArgumentException("Repetitive component types");

            _components = components;
        }

        #region interface implement

        public EnumComponentType GetComponentType() => curCompType;

        public List<EnumComponentFunction> GetFunctions()
        {
            return new List<EnumComponentFunction> {
                EnumComponentFunction.InitializeAsync,
                EnumComponentFunction.EnumerateAsync,
                EnumComponentFunction.OperateAsync,
                EnumComponentFunction.GetStatusAsync,
                EnumComponentFunction.TerminateAsync,
            };
        }

        public Dictionary<EnumComponentType, List<ArgumentItem>> GetDefaultSettings()
        {
            var retDict = new Dictionary<EnumComponentType, List<ArgumentItem>>();
            foreach (var component in _components)
            {
                retDict = retDict.DictConcat(component.GetDefaultSettings());
            }
            return retDict;
        }

        public Dictionary<EnumComponentType, Dictionary<string, string>> GetInputArguments(EnumComponentFunction func)
        {
            var retDict = new Dictionary<EnumComponentType, Dictionary<string, string>>();
            foreach (var component in _components)
            {
                retDict = retDict.DictConcat(component.GetInputArguments(func));
            }
            return retDict;
        }

        public Dictionary<EnumComponentType, Dictionary<string, string>> GetResultArguments(EnumComponentFunction func)
        {
            var retDict = new Dictionary<EnumComponentType, Dictionary<string, string>>();
            foreach (var component in _components)
            {
                retDict = retDict.DictConcat(component.GetResultArguments(func));
            }
            return retDict;
        }

        public async Task<Dictionary<EnumComponentType, Dictionary<string, dynamic>>> CallFunctionAsync(EnumComponentFunction func,
            Dictionary<EnumComponentType, Dictionary<string, dynamic>>? args,
            Delegates.NotifyAction? notification = null,
            CancellationToken? canelToken = null)
        {
            Dictionary<EnumComponentType, Dictionary<string, dynamic>>? callArgs = null;

            var retDict = new Dictionary<EnumComponentType, Dictionary<string, dynamic>>();
            foreach (var component in _components)
            {
                callArgs = null;
                if (args != null)
                {
                    var comptype = component.GetComponentType();
                    if (args.ContainsKey(comptype))
                        callArgs = new Dictionary<EnumComponentType, Dictionary<string, dynamic>> { { comptype, args[comptype] } };
                }
                retDict = retDict.DictConcat(await component.CallFunctionAsync(func, callArgs));
            }
            return retDict;
        }
        #endregion

    }
}
