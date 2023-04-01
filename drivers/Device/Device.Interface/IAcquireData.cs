using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AH.DeviceDriver.Interface
{
    internal interface IAcquireData<T, V> 
        where T : struct, IComparable where V : struct, IComparable
    {
        Task Start(IInstrument device, CancellationToken? token);
        Task Stop();
        Task<T[]> GetXDatas();
        Task<V[]> GetYDatas();
    }
}
