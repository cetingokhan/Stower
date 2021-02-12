using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stower.Base
{
    public interface ICustomStack
    {
        event EventHandler OnAdd;
        Type GetStackType();
        void Add<TObject>(TObject item);
        int StackCount();
        IEnumerable<object> GetAndClear();
    }


    public class CustomStack<TObject> : ConcurrentBag<TObject>, ICustomStack
    {
        public event EventHandler OnAdd;
        public event EventHandler OnRemove;

        public TObject AddedNewItem { get; set; }

        public TObject RemovedItem { get; set; }

        public Type GetStackType()
        {
            return typeof(TObject);
        }

        public int StackCount()
        {
            return this.Count();
        }

        public void Add<TItem>(TItem item)
        {
            var convertedItem = (TObject)Convert.ChangeType(item, typeof(TObject));

            try
            {
                base.Add(convertedItem);

#if DEBUG
                Console.WriteLine("ConcurrentBag'e kayıt eklendi");
#endif
            }
            finally
            {
                if (null != OnAdd)
                {
                    AddedNewItem = convertedItem;
                    OnAdd(this, null);
                }
            }
        }

        public new bool TryTake(out TObject item)
        {
            bool result = false;
            try
            {

                result = base.TryTake(out TObject output);

#if DEBUG
                if (result)
                    Console.WriteLine("ConcurrentBag'e kayıt silindi");
#endif
                RemovedItem = output;
                item = output;
                return result;
            }
            finally
            {
                if (result)
                {
                    if (OnRemove != null)
                    {
                        OnRemove(this, null);
                    }
                }
            }

        }

        public IEnumerable<object> GetAndClear()
        {
            TObject item;
            while (this.TryTake(out item))
            {
                yield return (object)item;
            }
        }
    }
}
