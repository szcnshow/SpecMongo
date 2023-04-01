using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AH.DeviceDriver.Interface;
using AH.DeviceDriver.Share;
using Newtonsoft.Json;

using static AH.DeviceDriver.Share.EnumComponentType;

namespace AH.DeviceDriver.TiScannerNano
{

    public class TiNanoInstrument : IInstrument
    {
        #region properties

        /*
        TiScanNano functions list
        OpenDevice();   return {  };
        CloseDevice();  return {  };
        EnumerateDevices();    return deviceProps
        GetDeviceProperty();    return deviceProps
        GetDeviceEnvironment();
        return {
            { "AmbientTemp", typeof(float).Name},
            { "DetectorTemp", typeof(float).Name},
            { "TivaTemp", typeof(float).Name},
            { "Humidity", typeof(float).Name},
            { "HDCTemp", typeof(float).Name},
            { "BattVolt", typeof(uint).Name},
            { "PhotoDetector", typeof(uint).Name},
        }
        GetDeviceStatus();
        return {
            { "SCAN_IN_PROGRESS", typeof(bool).Name},
            { "SD_CARD_PRESENT", typeof(bool).Name},
            { "SD_CARD_OPER_IN_PROG", typeof(bool).Name},
            { "BLE_STACK_OPEN", typeof(bool).Name},
            { "ACTIVE_BLE_CONNECTION", typeof(bool).Name},
            { "SCAN_INTERPRET_IN_PROGRESS", typeof(bool).Name},
        }

        GetScanConfigCount();   {"Count", int}
        
        ReadScanConfig(json& inJson);  input{ {"Index":int} }, output {configJson}

        ApplyScanConfig(json& configJson);
        input {
        	{"config_name", string},
		    {"num_repeats", uint16},
		    { "num_sections", uint8},
		    { "sections", [
			    {"exposure_time", uint16},
			    {"num_patterns", uint16},
			    {"section_scan_type", uint8},
			    {"wavelength_end_nm", uint16},
			    {"wavelength_start_nm", uint16},
			    {"width_px", secCfg->uint8,                 
            ]},
        }
        GetScanTime();  //call after ApplyScanConfig, return {"scan_time_ms", scantime}
        PerformScan();  //return {"scan_time_ms", scantime}
        CheckScanComplete();  //return {"scan_completed", ret} ret=O = Scan in progress, 1 = Scan complete, <0 = Status read failed.
        InterpretScan();    return { }
        GetScanResults();
        return {
		    {"device_status", {
		        {"scan_time", timestr},
		        {"detector_temp", data.detector_temp_hundredths / 100.0},
		        {"humidity", data.humidity_hundredths / 100.0},
		        {"lamp_pd", data.lamp_pd},
		        {"system_temp", data.system_temp_hundredths / 100.0},
            }
		    {"device_info", {
		        {"adc_data_length", data.adc_data_length},
		        {"black_pattern_first", data.black_pattern_first},
		        {"black_pattern_period", data.black_pattern_period},
		        {"PixelToWavelengthCoeffs", vector<double>(data.calibration_coeffs.PixelToWavelengthCoeffs, data.calibration_coeffs.PixelToWavelengthCoeffs + NUM_PIXEL_NM_COEFFS)},
		        {"ShiftVectorCoeffs", vector<double>(data.calibration_coeffs.ShiftVectorCoeffs, data.calibration_coeffs.ShiftVectorCoeffs + NUM_SHIFT_VECTOR_COEFFS)},
		        {"serial_number", string(data.serial_number)},
		        {"header_version", data.header_version},
            }
		    {"scan_data", {
		        {"data_length", data.length},
		        {"intensity", vector<int>(data.intensity, data.intensity + data.length)},
		        {"wavelength", vector<double>(data.wavelength, data.wavelength + data.length)},
            }
		    {"scan_config", configJson}
        }
        */

        private const EnumComponentType curCompType = Instrument;
        const string OpenDevice = nameof(OpenDevice);
        const string CloseDevice = nameof(CloseDevice);
        const string EnumerateDevices= nameof(EnumerateDevices);
        const string GetDeviceProperty = nameof(GetDeviceProperty);
        const string GetDeviceEnvironment = nameof(GetDeviceEnvironment);
        const string GetDeviceStatus=nameof(GetDeviceStatus);
        const string GetScanConfigCount=nameof(GetScanConfigCount);
        const string ReadScanConfig = nameof(ReadScanConfig);
        const string ApplyScanConfig = nameof(ApplyScanConfig);
        const string GetScanTime = nameof(GetScanTime);
        const string PerformScan=nameof(PerformScan);
        const string CheckScanComplete=nameof(CheckScanComplete);
        const string InterpretScan=nameof(InterpretScan);
        const string GetScanResults=nameof(GetScanResults);

        [DllImport("TiScanNano", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CppFuntioinCall")]
        internal static extern bool CppFuntioinCall(string funcName, byte[]? inData, int inDataSize, out IntPtr outData, out int outDataSize);

        [DllImport("TiScanNano", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CppFreeMemory")]
        internal static extern int CppFreeMemory(IntPtr mempt);

        static Dictionary<string, string> ScanType_Section = new Dictionary<string, string>
        {
            {"-1", "None"},
            {"0", "Column" },
            {"1", "Hardmard" },
        };

        static Dictionary<string, string> ExposureTime_Section = new Dictionary<string, string>
        {
            {"0", "0.635"},
            {"1", "1.27" },
            {"2", "2.54" },
            {"3", "5.08" },
            {"4", "15.24" },
            {"5", "30.48" },
            {"6", "60.96" },
        };

        static Dictionary<string, string> Gain_Selection = new Dictionary<string, string> { { "1", "1" }, { "2", "2" }, { "4", "4" }, { "8", "8" }, { "16", "16" }, { "32", "32" }, { "64", "64" } };
        static Dictionary<string, string> PixelWidth_Selection = Enumerable.Range(1, 30).ToDictionary(p => (p * 2).ToString(), q => (q * 2).ToString());
        const float MAX_WAVELENGTH = 1700;
        const float MIN_WAVELENGTH = 900;

        static Dictionary<string, string> deviceProps = new Dictionary<string, string>
        {
            { "TivaSWVer", typeof(uint).Name},
            { "DLPCSWVer", typeof(uint).Name},
            { "DLPCFlashBuildVer", typeof(uint).Name},
            { "SpecLibVer",  typeof(uint).Name},
            { "CalDataVer",  typeof(uint).Name},
            { "RefCalDataVer",typeof(uint).Name},
            { "CfgDataVer", typeof(uint).Name},
            { "SerialNo", typeof(string).Name},
            { "DeviceName", typeof(string).Name},
        };

        static Dictionary<string, string> statusProps = new Dictionary<string, string>
        {
            { "AmbientTemp", typeof(float).Name},
            { "DetectorTemp", typeof(float).Name},
            { "TivaTemp", typeof(float).Name},
            { "Humidity", typeof(float).Name},
            { "HDCTemp", typeof(float).Name},
            { "BattVolt", typeof(uint).Name},
            { "PhotoDetector", typeof(uint).Name},
            { "SCAN_IN_PROGRESS", typeof(bool).Name},
            { "SD_CARD_PRESENT", typeof(bool).Name},
            { "SD_CARD_OPER_IN_PROG", typeof(bool).Name},
            { "BLE_STACK_OPEN", typeof(bool).Name},
            { "ACTIVE_BLE_CONNECTION", typeof(bool).Name},
            { "SCAN_INTERPRET_IN_PROGRESS", typeof(bool).Name},
        };

        private List<ArgumentItem> defaultArgments
        {
            get
            {
                var retItems = new List<ArgumentItem>
                {
                    new ArgumentItem("config_name", typeof(string).Name, "mascot") { IsValid = false },
                    new ArgumentItem("num_repeats", typeof(string).Name, "5"),
                    new ArgumentItem("pga_gain", typeof(string).Name, "4", false, Gain_Selection),
                };

                for (int i = 0; i < 3; i++)
                    retItems.AddRange(GenerateOneSection(i));

                return retItems;
            }
        }

        /// <summary>
        /// 基本的Section得ScanType不能设置为NONO
        /// </summary>
        /// <param name="index"></param>
        /// <param name="scantype"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private static List<ArgumentItem> GenerateOneSection(int index)
        {
            string ext = "_"+ index.ToString();

            var retProps = new List<ArgumentItem>
            {
                new ArgumentItem($"section_scan_type{ext}", typeof(string).Name, "1", false, ScanType_Section),
                new ArgumentItem($"exposure_time{ext}", typeof(string).Name, "1.27", false, ExposureTime_Section),
                new ArgumentItem($"width_px{ext}", typeof(string).Name, "10", false, PixelWidth_Selection),
                new ArgumentItem($"wavelength_start_nm{ext}", typeof(string).Name, "900", true, null) { MaxValue = MAX_WAVELENGTH, MinValue = MIN_WAVELENGTH },
                new ArgumentItem($"wavelength_end_nm{ext}", typeof(string).Name, "1700", true, null) { MaxValue = MAX_WAVELENGTH, MinValue = MIN_WAVELENGTH },
            };

            var type_prop = retProps.Find(p => p.InnerName == $"section_scan_type{ext}");
            if (type_prop == null)
                throw new Exception("Program exceptions");
            if (index > 0)
                type_prop.NeedNewLine = true;

            //第一个区间不能有None选项
            if (index == 0)
            {
                type_prop.Selections = new Dictionary<string, string>
                {
                    { "0", ScanType_Section["0"] },
                    { "1", ScanType_Section["1"] },
                };
            }
            else  //后面区间可以设置不可见属性
            {
                //设置选择可见项
                for (int i = 1; i < retProps.Count; i++)
                {
                    retProps[i].IsValid = false;
                    retProps[i].VisibleSource = type_prop.InnerName;
                    retProps[i].VisibleValue = "None";
                    retProps[i].VisibleEqual = false;
                }
            }

            return retProps;
        }
        #endregion

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
            return new Dictionary<EnumComponentType, List<ArgumentItem>> { { curCompType, defaultArgments } };
        }

        public Dictionary<EnumComponentType, Dictionary<string, string>> GetInputArguments(EnumComponentFunction func)
        {
            switch (func)
            {
                case EnumComponentFunction.OperateAsync:
                    return defaultArgments.ToDictionary(p => p.InnerName, q => q.ValueType).AddComponentType(curCompType);
                default:
                    return new Dictionary<string, string>().AddComponentType(curCompType);
            }
        }

        public Dictionary<EnumComponentType, Dictionary<string, string>> GetResultArguments(EnumComponentFunction func)
        {
            switch (func)
            {
                case EnumComponentFunction.EnumerateAsync:
                    return new Dictionary<string, string> 
                    {
                        { PredefinedNames.Factory, typeof(string).Name },
                        { PredefinedNames.Model, typeof(string).Name },
                        { PredefinedNames.MeterType, typeof(string).Name },
                        { PredefinedNames.SerialNo, typeof(string).Name },
                        { PredefinedNames.Name, typeof(string).Name },
                    }.AddComponentType(curCompType);
                case EnumComponentFunction.GetPropertyAsync:
                    return deviceProps.AddComponentType(curCompType);
                case EnumComponentFunction.GetStatusAsync:
                    return statusProps.AddComponentType(curCompType);
                case EnumComponentFunction.OperateAsync:
                    return new Dictionary<string, string>{    
                        { "device_status", nameof(Newtonsoft.Json)},
                        { "device_info", nameof(Newtonsoft.Json)},
                        { "scan_data", nameof(Newtonsoft.Json)},
                        { "scan_config", nameof(Newtonsoft.Json)},
                    }.AddComponentType(curCompType);
                default:
                    return new Dictionary<string, string>().AddComponentType(curCompType);
            }
        }

        public async Task<Dictionary<EnumComponentType, Dictionary<string, dynamic>>> CallFunctionAsync(EnumComponentFunction func,
            Dictionary<EnumComponentType, Dictionary<string, dynamic>>? args,
            Delegates.NotifyAction? notification = null,
            CancellationToken? cancelToken = null)
        {
            if (args?.ContainsKey(curCompType) == false)
                throw new ArgumentException("LigthSource args");

            var realArgs = args?[OpticalPath];


            switch (func)
            {
                case EnumComponentFunction.InitializeAsync:
                    return (await CallTiDllFunction(OpenDevice)).AddComponentType(curCompType);
                case EnumComponentFunction.EnumerateAsync:
                    return (await CallTiDllFunction(EnumerateDevices)).AddComponentType(curCompType);
                case EnumComponentFunction.GetPropertyAsync:
                    return (await CallTiDllFunction(GetDeviceProperty)).AddComponentType(curCompType);
                case EnumComponentFunction.GetStatusAsync:
                    return (await CallTiDllFunction(GetDeviceEnvironment)).AddComponentType(curCompType);
                case EnumComponentFunction.OperateAsync:
                    return (await OperateAsync(realArgs, notification, cancelToken)).AddComponentType(curCompType);
                default:
                    return new Dictionary<string, dynamic> { }.AddComponentType(curCompType);
            }
        }
        #endregion

        #region private functions
        private async Task<Dictionary<string, dynamic>> CallTiDllFunction(string funcName, Dictionary<string, dynamic>? callParas=null)
        {
            byte[]? byteCallParas = null;
            IntPtr outPtrData = IntPtr.Zero;
            int outDataSize = 0;

            if (callParas != null && callParas.Count != 0)
            {
                var strparas = JsonConvert.SerializeObject(callParas);
                byteCallParas = Encoding.UTF8.GetBytes(strparas);
                Array.Resize(ref byteCallParas, byteCallParas.Length + 1);
                byteCallParas[byteCallParas.Length - 1] = 0;
            }

            var ret = await Task.Run<bool>(() => { 
                return CppFuntioinCall(funcName, byteCallParas, byteCallParas == null ? 0 : byteCallParas.Length, out outPtrData, out outDataSize);
            }) ;

            if (ret && outPtrData != IntPtr.Zero)
            {
                var retstr = Marshal.PtrToStringUTF8(outPtrData, outDataSize);
                CppFreeMemory(outPtrData);
                if (retstr == null)
                    throw new Exception($"Invalid return data when call {funcName} with {callParas}");

                var retDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(retstr);
                return retDict ?? new Dictionary<string, dynamic>();
            }
            else if (outPtrData != IntPtr.Zero)
            {
                var errstr = Marshal.PtrToStringUTF8(outPtrData, outDataSize);
                CppFreeMemory(outPtrData);

                if (errstr == null)
                    throw new Exception($"Unknown exception when call {funcName} with {callParas}");

                throw new Exception(errstr);
            }
            else
                throw new Exception($"Unknown exception when call {funcName} with {callParas}");
        }

        private async Task<Dictionary<string, dynamic>> OperateAsync(
            Dictionary<string, dynamic>? args,
            Delegates.NotifyAction? notification = null,
            CancellationToken? cancelToken = null)
        {
            var retJson = await CallTiDllFunction(ApplyScanConfig, args);
            notification?.Invoke(curCompType, EnumComponentNotify.Prepared, nameof(EnumComponentNotify.Prepared), null);

            var ret = await CallTiDllFunction(PerformScan, args);   //return {"scan_time_ms", scantime}
            int needtime = ret["scan_time_ms"];
            notification?.Invoke(curCompType, EnumComponentNotify.Processing, nameof(EnumComponentNotify.Processing), new Dictionary<string, dynamic>{ { PredefinedNames.Progress, 0f } });

            var startTime = DateTime.Now;
            while ((DateTime.Now - startTime).Milliseconds < needtime * 2)
            {
                //{"scan_completed", ret} ret=O = Scan in progress, 1 = Scan complete, <0 = Status read failed.
                ret = await CallTiDllFunction(CheckScanComplete, args);
                int retcode = (int)ret["scan_completed"];
                if (retcode == 0)
                {
                    if (notification != null)
                    {
                        var percent = (DateTime.Now - startTime).Milliseconds / (needtime * 1.0f);
                        if (percent > 100.0f)
                            percent = 100.0f;
                        if (notification != null)
                            notification.Invoke(curCompType, EnumComponentNotify.Processing, nameof(EnumComponentNotify.Processing), new Dictionary<string, dynamic> { { PredefinedNames.Progress, 0f } });
                    }
                    if (cancelToken != null)
                    {
                        if (cancelToken.Value.IsCancellationRequested)
                            break;
                        await Task.Delay(100, cancelToken.Value);
                    }
                    else
                        await Task.Delay(100);
                }
                else if(retcode == 1)
                {
                    notification?.Invoke(curCompType, EnumComponentNotify.Processing, nameof(EnumComponentNotify.Processing), new Dictionary<string, dynamic> { { PredefinedNames.Progress, 100.0f } });
                    break;
                }
                else
                {
                    notification?.Invoke(curCompType, EnumComponentNotify.InternalFailed, nameof(EnumComponentNotify.InternalFailed), new Dictionary<string, dynamic> { { PredefinedNames.Progress, 100.0f } });
                    throw new Exception(nameof(EnumComponentNotify.InternalFailed));
                }
            }
            if (cancelToken != null && cancelToken.Value.IsCancellationRequested)
            {
                ret = await CallTiDllFunction(InterpretScan, args);
                notification?.Invoke(curCompType, EnumComponentNotify.UserAborted, nameof(EnumComponentNotify.UserAborted), null);
                throw new Exception(nameof(EnumComponentNotify.UserAborted));
            }

            var scandata = await CallTiDllFunction(GetScanResults);

            //构造返回数据结构
            var props = await CallTiDllFunction(GetDeviceProperty);
            var returnProps = new Dictionary<string, dynamic>
            {
                {PredefinedNames.Factory, "mascot"},
                {PredefinedNames.MeterType, "NIR"},
                {PredefinedNames.Model, "VIEWDEEP3"},
                {PredefinedNames.Name, props["DeviceName"]},
                {PredefinedNames.SerialNo, props["SerialNo"]},
                { "TivaSWVer", props["TivaSWVer"]},
                { "DLPCSWVer", props["DLPCSWVer"]},
                { "DLPCFlashBuildVer", props["DLPCFlashBuildVer"]},
                { "SpecLibVer",  props["SpecLibVer"]},
                { "CalDataVer",  props["CalDataVer"]},
                { "RefCalDataVer",props["RefCalDataVer"]},
                { "CfgDataVer", props["CfgDataVer"]},
                {"adc_data_length", scandata["device_info"]["adc_data_length"]},
                {"black_pattern_first", scandata["device_info"]["black_pattern_first"]},
                {"black_pattern_period", scandata["device_info"]["black_pattern_period"]},
                {"PixelToWavelengthCoeffs", scandata["device_info"]["PixelToWavelengthCoeffs"]},
                {"ShiftVectorCoeffs", scandata["device_info"]["ShiftVectorCoeffs"]},
                {"header_version", scandata["device_info"]["header_version"]},
            };

            props = await CallTiDllFunction(GetDeviceEnvironment);
            var returnStatus = new Dictionary<string, dynamic>
            {
                {PredefinedNames.Temperature, props["TivaTemp"]},
                {PredefinedNames.Humidity, props["Humidity"]},
                { "AmbientTemp", typeof(float).Name},
                { "DetectorTemp", typeof(float).Name},
                { "TivaTemp", typeof(float).Name},
                { "HDCTemp", typeof(float).Name},
                { "BattVolt", typeof(uint).Name},
                { "PhotoDetector", typeof(uint).Name},
            };

            var returnDatas = new Dictionary<string, dynamic>
            {
                {PredefinedNames.Count, scandata["scan_data"]["data_length"]},
                {PredefinedNames.XDatas, scandata["scan_data"]["wavelength"]},
                {PredefinedNames.XType, new string[]{XAXISTYPE.Nanometers.ToString()} },
                {PredefinedNames.YDatas, scandata["scan_data"]["intensity"]},
                {PredefinedNames.YTypes, new string[]{ YAXISTYPE.Intensity.ToString()} },
            };

            return new Dictionary<string, dynamic>
            {
                {PredefinedNames.Properties, returnProps },
                {PredefinedNames.Status, returnStatus},
                {PredefinedNames.Settings, scandata["scan_config"] },
                {PredefinedNames.Datas, returnDatas},
            };
        }
        
        #endregion
    }
}
