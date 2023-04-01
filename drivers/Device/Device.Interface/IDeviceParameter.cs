using System;
using System.Collections.Generic;
using System.Text;

namespace AH.DeviceDriver.Interface
{
    interface IParameter
    {
        void GetDefaultParameter(IInstrument device);
    }
}
