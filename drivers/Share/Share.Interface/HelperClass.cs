using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Share
{
    public static class PropertiesHelper
    {
        public static Dictionary<EnumComponentType, T> AddComponentType<T>(this T dict, EnumComponentType types)
        {
            return new Dictionary<EnumComponentType, T> { { types, dict } };
        }

        public static Dictionary<EnumComponentType, T> DictConcat<T>(this Dictionary<EnumComponentType, T> dest, Dictionary<EnumComponentType, T>? source)
        {
            if (dest == null)
                throw new ArgumentNullException("Concate");
            if (source != null)
            {
                foreach (var item in source)
                {
                    dest.Add(item.Key, item.Value);
                }
            }
            return dest;
        }

        public static Dictionary<EnumComponentType, Dictionary<string, dynamic>> DynamicDict(EnumComponentType compType, Dictionary<string, dynamic> properties)
        {
            return new Dictionary<EnumComponentType, Dictionary<string, dynamic>> { { compType, properties } };
        }
    }
}
