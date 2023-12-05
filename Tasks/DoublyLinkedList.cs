using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public Node Previous { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public int Length => count;

        public void Add(T e)
        {
            Node newNode = new Node(e);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }

            count++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            Node newNode = new Node(e);

            if (index == 0)
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            else if (index == count)
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
            else
            {
                Node current = head;
                for (int i = 0; i < index - 1; i++)
                {
                    current = current.Next;
                }

                newNode.Next = current.Next;
                newNode.Previous = current;
                current.Next.Previous = newNode;
                current.Next = newNode;
            }

            count++;
        }

        public T ElementAt(int index)
        {
            if (index < 0 || index >= count || count == 0)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            Node current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Data;
        }

        public void Remove(T item)
        {
            Node current = head;

            while (current != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Data, item))
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                    }
                    else
                    {
                        head = current.Next;
                    }

                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                    }
                    else
                    {
                        tail = current.Previous;
                    }

                    count--;
                    return;
                }

                current = current.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index < 0 || index >= count || count == 0)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            Node current = head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            if (current.Previous != null)
            {
                current.Previous.Next = current.Next;
            }
            else
            {
                head = current.Next;
            }

            if (current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }
            else
            {
                tail = current.Previous;
            }

            count--;

            return current.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator(this);
        }

        private class DoublyLinkedListEnumerator : IEnumerator<T>
        {
            private Node current;
            private DoublyLinkedList<T> list;

            public DoublyLinkedListEnumerator(DoublyLinkedList<T> list)
            {
                this.list = list;
                current = null;
            }

            public T Current => current.Data;

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (current == null)
                {
                    current = list.head;
                }
                else
                {
                    current = current.Next;
                }

                return current != null;
            }

            public void Reset()
            {
                current = null;
            }

            public void Dispose()
            {

            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}
