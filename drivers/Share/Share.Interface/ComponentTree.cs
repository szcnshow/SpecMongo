using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share
{
    public class ComponentTree
    {
        public string ComponentName { get; set; } = null!;
        public EnumComponentType ComponentType { get;set; }
        public IComponent? component { get;set; }
        public List<ComponentTree> Children { get;} = new List<ComponentTree>();
        public Dictionary<string, ArgumentItem>? Settings { get; set; }
        public Dictionary<string, string>? Settings { get; set; }
    }
}
