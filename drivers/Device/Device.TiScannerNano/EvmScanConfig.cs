using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace AH.DeviceDriver.TiScannerNano
{
    /// <summary>
    /// EVM Scan Config operate
    /// </summary>
    internal class EvmScanConfig
    {
        #region const
        internal const float MAX_WAVELENGTH = 1700;
        internal const float MIN_WAVELENGTH = 900;
        const int NANO_PIXEL_WIDTH = 854;
        const int NANO_PIXEL_HEIGHT = 480;
        const int MAX_PATTERNS_PER_SCAN = 624;
        const int NUM_WIDTH_ITEMS = 60;
        const int DEFAULT_WIDTH_INDEX = 5;
        const int MIN_PIXEL_INDEX = 2;

        /// <summary>
        /// nm width in one pixel
        /// </summary>
        const double PIXEL_NM_WIDTH = (MAX_WAVELENGTH - MIN_WAVELENGTH) / (NANO_PIXEL_WIDTH * 0.8);

        #endregion

        #region class and enum

        static Dictionary<string, string> ScanType = new Dictionary<string, string>
        {
            {"-1", "None"},
            {"0", "Column" },
            {"1", "Hardmard" },
            {"2", "MultiRange" },
        };

        static Dictionary<string, string> ExposureTime = new Dictionary<string, string>
        {
            {"0", "0.635"},
            {"1", "1.27" },
            {"2", "2.54" },
            {"3", "5.08" },
            {"4", "15.24" },
            {"5", "30.48" },
            {"6", "60.96" },
        };

        #endregion


    }
}
