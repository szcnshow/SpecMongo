using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share
{
    /// <summary>
    /// fxtype X坐标类型
    /// </summary>
    public enum XAXISTYPE
    {
        Arbitrary = 0,   /* Arbitrary */
        Wavenumber = 1, /* Wavenumber (cm-1) */
        Micrometers = 2, /* Micrometers (um) */
        Nanometers = 3, /* Nanometers (nm) */
        Seconds = 4,  /* Seconds */
        Minutes = 5,    /* Minutes */
        Hertz = 6, /* Hertz (Hz) */
        Kilohertz = 7,    /* Kilohertz (KHz) */
        Megahertz = 8,    /* Megahertz (MHz) */
        Mass = 9,    /* Mass (M/z) */
        PPM = 10,  /* Parts per million (PPM) */
        Days = 11, /* Days */
        Years = 12,    /* Years */
        RamanShift = 13        /* Raman Shift (cm-1) */
    }

    /// <summary>
    /// fytype Y坐标类型
    /// </summary>
    public enum YAXISTYPE
    {
        ArbitraryIntensity = 0,   /* Arbitrary Intensity */
        Interferogram = 1, /* Interferogram */
        Absorbance = 2, /* Absorbance */
        KubelkaMonk = 3, /* Kubelka-Monk */
        Counts = 4, /* Counts */
        Volts = 5, /* Volts */
        Degrees = 6, /* Degrees */
        Milliamps = 7,  /* Milliamps */
        Millimeters = 8,    /* Millimeters */
        Millivolts = 9,    /* Millivolts */
        LogR = 10,    /* Log(1/R) */
        Percent = 11,   /* Percent */
        Intensity = 12,   /* Intensity */
        RelativeIntensity = 13,   /* Relative Intensity */
        Energy = 14,   /* Energy */
        Decibel = 16,    /* Decibel */
        TemperatureF = 19,    /* Temperature (F) */
        TemperatureC = 20,    /* Temperature (C) */
        TemperatureK = 21,    /* Temperature (K) */
        IndexofRefraction = 22,    /* Index of Refraction [N] */
        ExtinctionCoeff = 23,    /* Extinction Coeff. [K] */
        Real = 24, /* Real */
        Imaginary = 25, /* Imaginary */
        Complex = 26,    /* Complex */
        Transmission = 128,   /* Transmission (ALL HIGHER MUST HAVE VALLEYS!) */
        Reflectance = 129,  /* Reflectance */
        SingleBeam = 130,  /* Arbitrary or Single Beam with Valley Peaks */
        Emission = 131,   /* Emission */
        ATR = 140,
        BackgroundSingleBeam = 141, 
        SampleSingleBeam = 142, 
        BackgroundInterfergoram = 143, 
        SampleInterfergoram = 144,
        Pixel = 145,
        DARK = 146, 
        RefIntensity = 147,
        ReferencePixel = 148,
    }
}
