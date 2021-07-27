using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    class BinarySearchTree<T>
    {
        public BinarySearchTree(IComparer<T> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public Node<T> root = null;
        public int count = 0;
        public IComparer<T> comparer;

        public delegate void Action<T>(T Data, Stream stream);

        public void Add(T Data)
        {
            if (root == null)
            {
                root = new Node<T>(Data);
                root.level = 0;
                ++count;
                return;
            }
            
            Node<T> current = root;
            Node<T> previous = root;

            while (current != null)
            {
                if (comparer.Compare(Data, current.data) > 0)
                {
                    previous = current;
                    current = current.pLeft;
                }
                else if (comparer.Compare(Data, current.data) < 0)
                {
                    previous = current;
                    current = current.pRight;
                }
                else throw new Exception("Tree contains already element with such a data");
            }
            current = new Node<T>(Data);
            if (comparer.Compare(current.data, previous.data) > 0) previous.pLeft = current;
            else previous.pRight = current;
            current.level = previous.level + 1;
            ++count;
        }

        public Node<T> Search(T data)
        {
            Node<T> current = root;

            while (current != null)
            {
                if (comparer.Compare(data, current.data) > 0)
                {
                    current = current.pLeft;
                }
                else if (comparer.Compare(data, current.data) < 0)
                {
                    current = current.pRight;
                }
                else return current;
            }
            throw new Exception("Tree contains already element with such a data");
        }

        public void PreorderTraversal(Node<T> root, Action<T> action, Stream outputStream)
        {
            action(root.data, outputStream);
            if (root.pLeft != null) PreorderTraversal(root.pLeft, action, outputStream);
            if (root.pRight != null) PreorderTraversal(root.pRight, action, outputStream);
        }

        public void PrintTree()
        {
            //object lockObject = new();
            //lock (lockObject)
            //{
            //    PreorderTraversal(root, new Action<T>(PrintNode), outputStream);
            //}
            root.Print<T>();
        }

        public void PrintNode(T Data, Stream outputStream)
        {

        }
    }

    public class Node<T>
    {
        public T data;
        public Node<T> pLeft;
        public Node<T> pRight;
        public int level;

        public Node(T data, ref Node<T> pLeft, ref Node<T> pRight)
        {
            this.data = data;
            this.pLeft = pLeft;
            this.pRight = pRight;
        }
        public Node(T data)
        {
            this.data = data;
            pLeft = null;
            pRight = null;
        }
    }

    public static class BTreePrinter
    {
        public class NodeInfo<T>
        {
            public Node<T> Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo<T> Parent, Left, Right;
        }

        public static void Print<T>(this Node<T> root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo<T>>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo<T> { Node = next, Text = next.data.ToString() };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.pLeft)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.pLeft ?? next.pRight;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;
                    Print(item.Text, top, item.StartPos);
                    if (item.Left != null)
                    {
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.Node.pRight;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }
    }
}
