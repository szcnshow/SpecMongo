using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share
{
    public class Delegates
    {
       public delegate void NotifyAction(EnumComponentType hardwareType, EnumComponentNotify notifyType, string? message, Dictionary<string, dynamic>? addInfo);
    }
}
