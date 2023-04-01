using AH.Driver.Share.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.Driver.Share
{
    public class Delegates
    {
       public delegate void NotifyCallback(Guid moduleID, EnumModuleNotify notifyType, string? message, Dictionary<string, dynamic>? addInfo);
    }
}
