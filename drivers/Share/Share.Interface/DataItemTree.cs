using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share
{
    public class DataItemsTree<T>
    {
        public EnumComponentType ComponentType { get; set; }
        public Dictionary<string, T>? DataItems { get; set; }
        public DataItemsTree<T>? Parent { get; set; } = null;
        public List<DataItemsTree<T>> Children { get;} = new List<DataItemsTree<T>>();
    }
}
