using Stower.Base;
using System;
using System.Collections.Generic;

namespace Stower
{
    public class StowerOptions
    {
        public int? MaxStackLenght { get; set; }
        public int? MaxWaitInSecond { get; set; }
        public List<ICustomStack> Stacks { get; set; }     

        public event EventHandler OnTopple;
        public void ThrowEvent(List<object> items)
        {
            if (null != OnTopple)
            {
                OnTopple(items, null);
            }
        }
    }
}