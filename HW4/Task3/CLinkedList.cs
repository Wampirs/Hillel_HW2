using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4.Task3
{
    internal class CLinkedList
    {
        private class Item
        {
            public Item(object item)
            {
                This = item;
            }

            public object This;
            public Item Next;
        }

        private Item _first;
        public object First => _first.This;

        private Item _last;
        public object Last=>_last.This;

        public uint Count { get; private set; } = 0;

        public void Add(object item)
        {
            if(Count==0) 
            {
                _first = _last = new Item(item);
                Count++;
                return;
            }
            if (Count==1)
            {
                _first.Next = _last = new Item(item);
                Count++;
                return;
            }
            var @new = new Item(item) ; 
            _last.Next = @new;
            _last = @new;
            Count++;
        }

        public void AddFirst(object item)
        {
            if (Count==0) 
            {
                Add(item);
                return;
            }
            if (Count==1)
            {
                _first = new Item(item) {Next=_last};
                Count++;
                return;
            }
            _first = new Item(item) { Next = _first };
            Count++;
        }

        public void Insert(uint index, object item)
        {
            Item beforeIt = _first;
            for(int i = 0; i < index-1; i++) 
            {
                if(beforeIt.Next == null) throw new ArgumentOutOfRangeException(nameof(index));
                beforeIt = beforeIt.Next;
            }
            beforeIt.Next = new Item(item) { Next = beforeIt.Next?.Next };
            Count++;
        }

        public void Clear()
        {
            _first = _last = null;
            Count = 0;
        }

        public bool Contains(object item)
        {
            if (Count==0) return false;
            var tarIt = _first;
            for(int i = 0;i < Count;i++) 
            {
                if (tarIt.This==item)return true;
                tarIt = tarIt.Next;
            }
            return false;
        }

        public object[] ToArray()
        {
            var res = new object[Count];
            var tarIt = _first;
            for(var i = 0; i < Count; i++)
            {
                res[i] = tarIt.This;
                tarIt = tarIt.Next;
            }
            return res;
        }
    }
}
