using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NantauCommon.Extensions
{
    public static class LinkedListExtensions
    {
        //Stolen from stackoverflow so that you can get a searchable linkedlist
        public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedList<T> list)
        {
            for (var node = list.First; node != null; node = node.Next)
            {
                yield return node;
            }
        }

        public static T First<T>(this LinkedList<T> list, Func<T,bool> predicate)
        {
            return list.Nodes().First(x => predicate(x.Value)).Value;
        }

        public static T FirstOrDefault<T>(this LinkedList<T> list, Func<T,bool> predicate)
        {
            return list.Nodes().FirstOrDefault(x => predicate(x.Value)).Value;
        }

        public static T PopFront<T>(this LinkedList<T> list)
        {
            if (list.Count == 0)
                throw new InvalidOperationException();

            var popped = list.First;
            list.RemoveFirst();
            return popped.Value;
        }

        public static T PopRear<T>(this LinkedList<T> list)
        {
            if (list.Count == 0)
                throw new InvalidOperationException();

            var popped = list.Last;
            list.RemoveLast();
            return popped.Value;
        }
    }
}
