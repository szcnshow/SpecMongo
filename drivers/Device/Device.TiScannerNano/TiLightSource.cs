using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AH.DeviceDriver.Share;
using System.Xml.Linq;
using System.Device.Gpio;
using System.Threading;
using System.Data.SqlTypes;

using AH.DeviceDriver.Share.Component;
using AH.DeviceDriver.Share.Argument;
using static AH.DeviceDriver.Share.Hardware.EnumHardwareType;
using AH.DeviceDriver.Share.Hardware;
using AH.DeviceDriver.Share.DataItem;
using System.Reflection.Metadata.Ecma335;

namespace AH.DeviceDriver.TiScannerNano
{
    public class TiNanoLightSource : ILigthSource
    {
        #region properties
        private int GpioPort = 8;
        private const EnumHardwareType curCompType = LigthSource;
        private Guid componentID;

        //'初始化GPIO引脚'
        //self.inner_on = 27
        //self.inner_off = 18
        //self.outer_on = 17
        //self.outer_off = 15
        //self.source_on = 16

        const string defaultPort = "16";
        const string componentName = "TiNanoLightSource";
        const string portArgName = $"{componentName}.{nameof(GpioPort)}";

        #endregion

        public TiNanoLightSource()
        {

        }

        #region interface implement
        private List<ArgumentItem> defaultArgments = new List<ArgumentItem>
        {
            new ArgumentItem(portArgName, typeof(int).Name, defaultPort, EnumArgumentScope.SameComponent, EnumArgumentAuthor.System),
        };

        public string GetComponentName() => componentName;
        public Guid GetComponentID() => componentID;

        public EnumHardwareType GetComponentType() => curCompType;

        public List<ArgumentItem> GetArguments()
        {
            return defaultArgments;
        }

        public List<EnumHardwareCapacity> GetCapacities()
        {
            return new List<EnumHardwareCapacity> 
            { 
                EnumHardwareCapacity.Dark, 
                EnumHardwareCapacity.Background, 
                EnumHardwareCapacity.Reference, 
                EnumHardwareCapacity.Sample
            };
        }

        private bool IsDisposed = true;
        public void Dispose() 
        { 
            IsDisposed= true;
        }

        public bool IsAutomatic(EnumHardwareCapacity capacity) => true;

        public string? ManualOperateNotification(EnumHardwareCapacity capacity) => null;
        public List<DataItem> GetResults()
        {
            return new List<DataItem>();
        }

        public async Task<List<DataItem>> CallFunctionAsync(
            EnumComponentFunction func,
            Dictionary<string, dynamic>? args,
            List<DataItem>? inputDatas,
            Delegates.NotifyAction? notification = null,
            CancellationToken? cancelToken = null)
        {
            switch (func)
            {
                case EnumComponentFunction.InitializeAsync:
                    return await OperateSouceAsync(args, true);
                case EnumComponentFunction.GetStatusAsync:
                    return await GetStatusAsync(args);
                case EnumComponentFunction.TerminateAsync:
                    return await OperateSouceAsync(args, false);                    
                default:
                    return new List<DataItem>();
            }
        }

        #endregion

        #region private functions
        private void CheckArgument(Dictionary<string, dynamic>? args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            if (args.ContainsKey(portArgName) == false || int.TryParse(args[portArgName], out GpioPort) == false)
                throw new ArgumentNullException(nameof(args));
        }

        private async Task<List<DataItem>> GetStatusAsync(Dictionary<string, dynamic>? args)
        {
            CheckArgument(args);
            using (GpioController driver = new GpioController())
            {
                var pinValue = await Task.Run(() => { return driver.Read(GpioPort); });
                return new List<DataItem> { 
                    new DataItem(){  
                        DataOwner = componentID, 
                        DataID=Guid.NewGuid(), 
                        DataName=portArgName,
                        ValueType=typeof(bool).Name, 
                        DataValue=(bool)pinValue, 
                        DataType = EnumDataType.NodeStatus 
                    } };
            }
        }

        private async Task<List<DataItem>> OperateSouceAsync(Dictionary<string, dynamic>? args, bool onOff)
        {
            CheckArgument(args);
            using (GpioController driver = new GpioController())
            {
                var pinValue = await Task.Run(() => { driver.Write(GpioPort, onOff); return driver.Read(GpioPort); });
                return new List<DataItem> {
                    new DataItem(){
                        DataOwner = componentID,
                        DataID=Guid.NewGuid(),
                        DataName=portArgName,
                        ValueType=typeof(bool).Name,
                        DataValue=(bool)pinValue,
                        DataType = EnumDataType.NodeStatus
                    } };
            }
        }

        #endregion
    }
}
