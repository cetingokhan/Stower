using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stower
{
    public interface IStower
    {
        Task Add<T>(T item);
    }
}
