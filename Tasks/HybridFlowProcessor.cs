using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private DoublyLinkedList<T> list;

        public HybridFlowProcessor()
        {
            list = new DoublyLinkedList<T>();
        }
        public T Dequeue()
        {
            if (list.Length == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            T item = list.ElementAt(0);
            list.RemoveAt(0);

            return item;
        }

        public void Enqueue(T item)
        {
            list.Add(item);
        }

        public T Pop()
        {
            if (list.Length == 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }

            T item = list.ElementAt(list.Length - 1);
            list.RemoveAt(list.Length - 1);

            return item;
        }

        public void Push(T item)
        {
            list.Add(item);
        }
    }
}
