using Microsoft.Extensions.Options;
using Stower.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stower
{
    public class BaseStower : IStower
    {
        private readonly StowerOptions _options;
        private readonly IToppleHandler _toppleHandler;
        private static object _stackLock = new object();

        public BaseStower(IOptions<StowerOptions> options, IToppleHandler toppleHandler)
        {
            _options = options.Value;

            foreach (var stack in _options.Stacks)
            {
                stack.OnAdd += Stack_OnAdd;
            }

            _toppleHandler = toppleHandler;
        }

        void ThreadWork(Object stateInfo)
        {
            _toppleHandler.Handle((List<object>)stateInfo);
            _options.ThrowEvent((List<object>)stateInfo);
        }

        private void Stack_OnAdd(object sender, EventArgs e)
        {
            var stack = (ICustomStack)sender;
            if (_options.MaxStackLenght == stack.StackCount())
            {
                List<object> list;
                lock (_stackLock)
                {
                    list = stack.GetAndClear().ToList();
                }
                ThreadPool.QueueUserWorkItem(ThreadWork, list);
            }
        }

        public async Task Add<T>(T item)
        {
            lock (_stackLock)
            {
                var stack = _options.Stacks.Single(x => x.GetStackType() == item.GetType());
                stack.Add<T>(item);
            }
            await Task.CompletedTask;

        }
    }
}
