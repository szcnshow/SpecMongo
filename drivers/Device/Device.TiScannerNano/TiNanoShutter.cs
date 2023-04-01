using AH.DeviceDriver.Share;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AH.DeviceDriver.Share.Hardware;
using AH.DeviceDriver.Share.Component;
using AH.DeviceDriver.Share.Argument;
using AH.DeviceDriver.Share.DataItem;

namespace AH.DeviceDriver.TiScannerNano
{
    public class TiNanoShutter : IOpticalPath
    {
        #region properties
        private int Reference_On_Port = 27;
        private int Reference_Off_Port = 18;
        private int Sample_On_Port = 17;
        private int Sample_Off_Port = 15;
        private const EnumHardwareType curCompType = EnumHardwareType.OpticalPath;
        private Guid componentID;

        //'初始化GPIO引脚'
        //self.inner_on = 27
        //self.inner_off = 18
        //self.outer_on = 17
        //self.outer_off = 15
        //self.source_on = 16

        const string componentName = "TiNanoShutter";

        private List<ArgumentItem> defaultArgments = new List<ArgumentItem>
        {
            new ArgumentItem($"{componentName}.{nameof(Reference_On_Port)}", typeof(int).Name, "27", EnumArgumentScope.SameComponent, EnumArgumentAuthor.System),
            new ArgumentItem($"{componentName}.{nameof(Reference_Off_Port)}", typeof(int).Name, "18", EnumArgumentScope.SameComponent, EnumArgumentAuthor.System),
            new ArgumentItem($"{componentName}.{nameof(Sample_On_Port)}", typeof(int).Name, "17", EnumArgumentScope.SameComponent, EnumArgumentAuthor.System),
            new ArgumentItem($"{componentName}.{nameof(Sample_Off_Port)}", typeof(int).Name, "15", EnumArgumentScope.SameComponent, EnumArgumentAuthor.System),
            new ArgumentItem(nameof(EnumHardwareCapacity), typeof(EnumHardwareCapacity).Name, EnumHardwareCapacity.Dark.ToString(), EnumArgumentScope.EachTime, EnumArgumentAuthor.Reference),
        };
        #endregion

        #region interface implement

        public string GetComponentName() => "TiNanoShutter";
        public EnumHardwareType GetComponentType() => curCompType;
        public Guid GetComponentID() => componentID;
        public List<ArgumentItem> GetArguments() => defaultArgments;

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
            IsDisposed = true;
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
                    return await InitializeAsync(args);
                case EnumComponentFunction.GetStatusAsync:
                    return await GetStatusAsync(realArgs);
                case EnumComponentFunction.OperateAsync:
                    return await OperateAsync(realArgs);
                default:
                    return new List<DataItem> { };
            }
        }

        #endregion

        #region private functions

        private void CheckArgument(Dictionary<string, dynamic>? args, bool needStatus)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            string port = $"{componentName}.{nameof(Reference_On_Port)}";
            if (!args.ContainsKey(port) || int.TryParse(args[port], out Reference_On_Port) == false)
                throw new ArgumentNullException(nameof(args));
            foreach (var item in defaultArgments)
            {
                if (!args.ContainsKey(item.InnerName) || int.TryParse(args[item.InnerName], out int portNum) == false)
                    throw new ArgumentNullException(nameof(args));
            }

            if (needStatus == false)
                return;

            if (!args.ContainsKey(nameof(Shutter1)) || bool.TryParse(args[nameof(Shutter1)], out Shutter1) == false ||
                !args.ContainsKey(nameof(Shutter2)) || bool.TryParse(args[nameof(Shutter2)], out Shutter2) == false)
                    throw new ArgumentNullException(nameof(args));
        }

        private int ParsePortNumber(Dictionary<string, dynamic> args, string portname)
        {
            int portNo = 0;
            if (!args.ContainsKey(portname) || int.TryParse(args[portname], out portNo) == false)
                throw new ArgumentNullException(nameof(args));
            return portNo;
        }

        private void WriteOnPin(GpioController controller, int pinNo, bool pinValue)
        {
            controller.OpenPin(pinNo);
            controller.Write(pinNo, pinValue);
            controller.ClosePin(pinNo);
        }

        private async Task<List<DataItem>> InitializeAsync(Dictionary<string, dynamic>? args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            Reference_On_Port = ParsePortNumber(args, $"{componentName}.{nameof(Reference_On_Port)}");
            Reference_Off_Port = ParsePortNumber(args, $"{componentName}.{nameof(Reference_Off_Port)}");
            Sample_On_Port = ParsePortNumber(args, $"{componentName}.{nameof(Sample_On_Port)}");
            Sample_Off_Port = ParsePortNumber(args, $"{componentName}.{nameof(Sample_Off_Port)}");

            using (GpioController controller = new GpioController())
            {
                await Task.Run(() =>
                {
                    controller.SetPinMode(Reference_On_Port, PinMode.Output);
                    controller.SetPinMode(Reference_Off_Port, PinMode.Output);
                    controller.SetPinMode(Sample_On_Port, PinMode.Output);
                    controller.SetPinMode(Sample_Off_Port, PinMode.Output);

                    //关闭所有开关
                    WriteOnPin(controller, Reference_On_Port, false);
                    WriteOnPin(controller, Reference_Off_Port, true);
                    WriteOnPin(controller, Sample_On_Port, false);
                    WriteOnPin(controller, Sample_Off_Port, true);
                });
            }
            return new List<DataItem> {
                    new DataItem(){
                        DataOwner = componentID,
                        DataID=Guid.NewGuid(),
                        DataName=nameof(EnumHardwareCapacity),
                        ValueType=typeof(bool).Name,
                        DataValue=EnumHardwareCapacity.Dark,
                        DataType = EnumDataType.NodeStatus
                    } };
        }

        private async Task<Dictionary<string, dynamic>> GetStatusAsync(Dictionary<string, dynamic>? args)
        {
            CheckArgument(args, false);
            using GpioController controller = new GpioController();

            bool curShutter1 = bool.Parse(args?[nameof(Shutter1)]);
            bool curShutter2 = bool.Parse(args?[nameof(Shutter2)]);
            await Task.Run(() =>
            {
                curShutter1 = controller.Read(int.Parse(args?[nameof(Shutter1_On_Port)])) == true && controller.Read(int.Parse(args?[nameof(Shutter1_Off_Port)])) == false;
                curShutter2 = controller.Read(int.Parse(args?[nameof(Shutter2_On_Port)])) == true && controller.Read(int.Parse(args?[nameof(Shutter2_Off_Port)])) == false;
            });

            return new Dictionary<string, dynamic> { { nameof(Shutter1), curShutter1 }, { nameof(Shutter2), curShutter2 } };
        }

        private async Task<Dictionary<string, dynamic>> OperateAsync(Dictionary<string, dynamic>? args)
        {
            CheckArgument(args, false);
            using GpioController controller = new GpioController();

            bool curShutter1 = bool.Parse(args?[nameof(Shutter1)]);
            bool curShutter2 = bool.Parse(args?[nameof(Shutter2)]);

            await Task.Run(() =>
            {
                controller.Write(int.Parse(args?[nameof(Shutter1_On_Port)]), curShutter1 ? true : false);   //Open: Shutter1_On_Port=1, Close: Shutter1_On_Port=0
                controller.Write(int.Parse(args?[nameof(Shutter1_Off_Port)]), curShutter1 ? false : true);
                controller.Write(int.Parse(args?[nameof(Shutter2_On_Port)]), curShutter2 ? true : false);
                controller.Write(int.Parse(args?[nameof(Shutter2_Off_Port)]), curShutter2 ? false : true);

                curShutter1 = controller.Read(int.Parse(args?[nameof(Shutter1_On_Port)])) == true && controller.Read(int.Parse(args?[nameof(Shutter1_Off_Port)])) == false;
                curShutter2 = controller.Read(int.Parse(args?[nameof(Shutter2_On_Port)])) == true && controller.Read(int.Parse(args?[nameof(Shutter2_Off_Port)])) == false;
            });

            return new Dictionary<string, dynamic> { { nameof(Shutter1), curShutter1 }, {nameof(Shutter2), curShutter2 }};
        }

        #endregion
    }
}
