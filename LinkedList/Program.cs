using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public sealed class Node<T> {
        public T Data;
        public Node<T> Next;

        public Node(T data, Node<T> next = null) {
            Data = data;
            Next = next;
        }
    }

    public class MyList<T> : IEnumerable<T> where T: IComparable<T>, IEquatable<T> {
        private Node<T> head;
        private Node<T> tail;

        public MyList() { }
        public MyList(IEnumerable<T> other) {
            foreach (var value in other) {
                AddToEnd(value);
            }
        }

        public void AddToEnd(T data) {
            var d = new Node<T>(data);
            if (head == null && tail == null) {
                head = d;
                tail = d;
            } else {
                tail.Next = d;
                tail = d;
            }
        }

        public void AddToStart(T data) {
            var d = new Node<T>(data);
            if (head == null && tail == null) {
                head = d;
                tail = d;
            } else {
                d.Next = head;
                head = d;
            }
        }

        public void InsertAfter(T after, T data) {
            var afterNode = FindNode(after);

            var d = new Node<T>(data, afterNode.Next);
            afterNode.Next = d;
            if (afterNode == tail) {
                tail = d;
            }
        }

        public void InsertBefore(T before, T data) {
            var beforeNode = FindNode(before);

            var d = new Node<T>(data, beforeNode);
            if (beforeNode == head) {
                head = d;
            } else {
                var prevBeforeNode = FindPreviousNode(before);
                prevBeforeNode.Next = d;
            }
        }

        public bool Contains(T data) {
            for (Node<T> d = head; d != null; d = d.Next) {
                if (d.Data.Equals(data)) {
                    return true;
                }
            }
            return false;
        }

        private Node<T> FindNode(T data) {
            for (Node<T> d = head; d != null; d = d.Next) {
                if (d.Data.Equals(data)) {
                    return d;
                }
            }
            return null;
        }

        private Node<T> FindPreviousNode(T data) {
            for (Node<T> d = head; d != null && d.Next != null; d = d.Next) {
                if (d.Next.Data.Equals(data)) {
                    return d;
                }
            }
            return null;
        }

        private Node<T> FindPreviousNode(Node<T> node) {
            for (Node<T> d = head; d != null && d.Next != null; d = d.Next) {
                if (d.Next == node) {
                    return d;
                }
            }
            return null;
        }

        public void SwapPointer(T a, T b) {
            var nodeA = FindNode(a);
            var nodeB = FindNode(b);
            if (nodeA == null || nodeB == null) return;

            var prevNodeA = FindPreviousNode(a);
            var prevNodeB = FindPreviousNode(b);

            var afterA = nodeA.Next;
            var afterB = nodeB.Next;
            if (prevNodeA == null) { // value A is at start and has not previous
                head = nodeB;
                nodeB.Next = afterA;

                prevNodeB.Next = nodeA;
                nodeA.Next = afterB;
            } else if (prevNodeB == null) { // value B is at start and has not previous
                prevNodeA.Next = nodeB;
                nodeB.Next = afterA;

                head = nodeA;
                nodeA.Next = afterB;
            } else {
                prevNodeA.Next = nodeB;
                nodeB.Next = afterA;

                prevNodeB.Next = nodeA;
                nodeA.Next = afterB;
            }
        }

        public void SwapPointer(Node<T> nodeA, Node<T> nodeB) {
            if (nodeA == null || nodeB == null) return;

            var prevNodeA = FindPreviousNode(nodeA);
            var prevNodeB = FindPreviousNode(nodeB);

            var afterA = nodeA.Next;
            var afterB = nodeB.Next;
            if (prevNodeA == null) { // value A is at start and has not previous
                head = nodeB;
                nodeB.Next = afterA;

                prevNodeB.Next = nodeA;
                nodeA.Next = afterB;
            } else if (prevNodeB == null) { // value B is at start and has not previous
                prevNodeA.Next = nodeB;
                nodeB.Next = afterA;

                head = nodeA;
                nodeA.Next = afterB;
            } else {
                prevNodeA.Next = nodeB;
                nodeB.Next = afterA;

                prevNodeB.Next = nodeA;
                nodeA.Next = afterB;
            }
        }

        private void RemoveNode(Node<T> node) {
            if (node == head) {
                head = head.Next;
            } else {
                var prev = FindPreviousNode(node);
                prev.Next = node.Next;
            }
        }

        public void RemoveValue(T data) {
            RemoveNode(FindNode(data));
        }

        public void RemoveFirst() {
            if (head != null) return;
            head = head.Next;
        }

        public void RemoveLast() {
            var prev = FindPreviousNode(tail);
            prev.Next = null;
            tail = prev;
        }

        public void SwapValue(T a, T b) {
            var nodeA = FindNode(a);
            var nodeB = FindNode(b);

            T temp = nodeA.Data;
            nodeA.Data = nodeB.Data;
            nodeB.Data = temp;
        }

        public void BubbleSort() {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (Node<T> d = head; d != null && d.Next != null; d = d.Next)
                {
                    if(d.Data.CompareTo(d.Next.Data) > 0)
                    {
                        T temp = d.Data;
                        d.Data = d.Next.Data;
                        d.Next.Data = temp;
                        flag = true;
                    }
                }
            }
        }

        public void SelectionSort() {
            for (Node<T> d = head; d.Next != null; d = d.Next) {
                var min = d;
                for (Node<T> g = d.Next; g != null; g = g.Next) {
                    if(min.Data.CompareTo(g.Data) > 0) {
                        min = g;
                    }
                }

                T temp = d.Data;
                d.Data = min.Data;
                min.Data = temp;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> d = head; d != null; d = d.Next)
                yield return d.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MyFictiveList<T> : IEnumerable<T> where T: IComparable<T>, IEquatable<T> {
        private Node<T> head; // fiktyvys
        private Node<T> tail; // fiktyvys

        private Node<T> prevTail;

        public MyFictiveList() {
            tail = new Node<T>(default);
            head = new Node<T>(default, tail);
            prevTail = head;
        }
        public MyFictiveList(IEnumerable<T> other) {
            foreach (var value in other) {
                AddToEnd(value);
            }
        }

        public void AddToEnd(T data) {
            var d = new Node<T>(data, tail);
            prevTail.Next = d;
            prevTail = d;
        }

        public void AddToStart(T data) {
            head.Next = new Node<T>(data, head.Next);
        }

        public void InsertAfter(T after, T data) {
            var afterNode = FindNode(after);

            var d = new Node<T>(data, afterNode.Next);
            afterNode.Next = d;
            if (afterNode == tail) {
                prevTail = d;
            }
        }

        public void InsertBefore(T before, T data) {
            var beforeNode = FindNode(before);

            var d = new Node<T>(data, beforeNode);
            if (beforeNode == head.Next) {
                head.Next = d;
            } else {
                var prevBeforeNode = FindPreviousNode(before);
                prevBeforeNode.Next = d;
            }
        }

        private Node<T> FindNode(T data) {
            for (Node<T> d = head.Next; d != tail; d = d.Next) {
                if (d.Data.Equals(data)) {
                    return d;
                }
            }
            return null;
        }

        private Node<T> FindPreviousNode(T data) {
            for (Node<T> d = head.Next; d != tail && d.Next != tail; d = d.Next) {
                if (d.Next.Data.Equals(data)) {
                    return d;
                }
            }
            return null;
        }

        private Node<T> FindPreviousNode(Node<T> node) {
            for (Node<T> d = head.Next; d != tail && d.Next != tail; d = d.Next) {
                if (d.Next == node) {
                    return d;
                }
            }
            return null;
        }

        public void SwapPointer(T a, T b) {
            var nodeA = FindNode(a);
            var nodeB = FindNode(b);
            if (nodeA == null || nodeB == null) return;
            var prevNodeA = FindPreviousNode(a);
            var prevNodeB = FindPreviousNode(b);

            var afterA = nodeA.Next;
            var afterB = nodeB.Next;

            prevNodeA.Next = nodeB;
            nodeB.Next = afterA;

            prevNodeB.Next = nodeA;
            nodeA.Next = afterB;
        }

        public void SwapPointer(Node<T> nodeA, Node<T> nodeB) {
            if (nodeA == null || nodeB == null) return;
            var prevNodeA = FindPreviousNode(nodeA);
            var prevNodeB = FindPreviousNode(nodeB);

            var afterA = nodeA.Next;
            var afterB = nodeB.Next;

            prevNodeA.Next = nodeB;
            nodeB.Next = afterA;

            prevNodeB.Next = nodeA;
            nodeA.Next = afterB;
        }

        public void SwapValue(T a, T b) {
            var nodeA = FindNode(a);
            var nodeB = FindNode(b);

            T temp = nodeA.Data;
            nodeA.Data = nodeB.Data;
            nodeB.Data = temp;
        }

        public void RemoveFirst() {
            if (head.Next != tail) return;
            head.Next = head.Next.Next;
        }

        public void RemoveLast() {
            var prev = FindPreviousNode(prevTail);
            prev.Next = tail;
            prevTail = prev;
        }

        public void BubbleSort() {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (Node<T> d = head.Next; d != tail && d.Next != tail; d = d.Next)
                {
                    if(d.Data.CompareTo(d.Next.Data) > 0)
                    {
                        T temp = d.Data;
                        d.Data = d.Next.Data;
                        d.Next.Data = temp;
                        flag = true;
                    }
                }
            }
        }

        public void SelectionSort() {
            for (Node<T> d = head.Next; d.Next != tail; d = d.Next) {
                var min = d;
                for (Node<T> g = d.Next; g != tail; g = g.Next) {
                    if(min.Data.CompareTo(g.Data) > 0) {
                        min = g;
                    }
                }

                T temp = d.Data;
                d.Data = min.Data;
                min.Data = temp;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> d = head.Next; d != tail; d = d.Next)
                yield return d.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var list = new MyFictiveList<int>();

            list.AddToEnd(1);
            list.AddToEnd(2);
            list.AddToEnd(3);
            list.AddToEnd(4);
            list.AddToEnd(5);

            Console.WriteLine("list:");
            foreach (var value in list) {
                Console.WriteLine(value);
            }
        }
    }
}
