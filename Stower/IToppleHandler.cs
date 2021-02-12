using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stower
{
    public interface IToppleHandler
    {
        Task Handle(List<object> objects);
    }
}
