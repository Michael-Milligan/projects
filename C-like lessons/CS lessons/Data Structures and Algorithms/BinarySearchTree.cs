using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    public class BinarySearchTree<T>
    {
        public BinarySearchTree(IComparer<T> comparer)
        {
            this.comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public Node<T> root = null;
        public int count = 0;
        public IComparer<T> comparer;

        public delegate void Action<T>(T Data, Stream stream);

        /// <summary>
        /// Adds non-existent element to the tree
        /// </summary>
        /// <param name="Data"></param>
        /// <exception cref="System.ArgumentException">Thrown when element is already in tree.</exception>
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
                    current = current.left;
                }
                else if (comparer.Compare(Data, current.data) < 0)
                {
                    previous = current;
                    current = current.right;
                }
                else throw new ArgumentException("Tree contains already element with such a data");
            }
            current = new Node<T>(Data);
            if (comparer.Compare(current.data, previous.data) > 0) previous.left = current;
            else previous.right = current;
            current.level = previous.level + 1;
            current.parent = previous;
            ++count;
        }

        public Node<T> Search(T data)
        {
            Node<T> current = root;

            while (current != null)
            {
                if (comparer.Compare(data, current.data) > 0)
                {
                    current = current.left;
                }
                else if (comparer.Compare(data, current.data) < 0)
                {
                    current = current.right;
                }
                else return current;
            }
            throw new Exception("Tree contains already element with such a data");
        }

        public void PreorderTraversal(Node<T> root, Action<T> action, Stream outputStream)
        {
            action(root.data, outputStream);
            if (root.left != null) PreorderTraversal(root.left, action, outputStream);
            if (root.right != null) PreorderTraversal(root.right, action, outputStream);
        }
    }

    public class Node<T>
    {
        public T data;
        public Node<T> left;
        public Node<T> right;
        public Node<T> parent;
        public int level;

        public Node(T data, ref Node<T> pLeft, ref Node<T> pRight)
        {
            this.data = data;
            this.left = pLeft;
            this.right = pRight;
        }
        public Node(T data)
        {
            this.data = data;
            left = null;
            right = null;
        }
    }

    public static class BTreePrinter
    {
        public class NodeInfo<T>
        {
            public Node<T> Node;
            public string Text { get { return Node.data.ToString(); } }
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo<T> Parent, Left, Right;
        }

        public static void Print<T>(this Node<T> root, int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo<T>>();
            var next = root;



            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo<T> { Node = next};
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
                    if (next == item.Parent.Node.left)
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
                next = next.left ?? next.right;
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
                        next = item.Parent.Node.right;
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

    public static class BinaryTreePrinter<T>
    {
        class NodeInfo<T>
        {
            public Node<T> node;
            public int displayLevel { get { return node.level * 2 + 1; } }
            public int startPosition;
            public string Text { get { return node.data.ToString(); } }
            public int endPosition { get { return startPosition + Text.Length; } set { startPosition = value - Text.Length; } }
            public NodeInfo<T> parent, left, right;
        }

        static void PrintNode(string data, int top, int left, int right)
        {
            if (right <= 0) right = left + data.Length;
            Console.SetCursorPosition(left, top);
            Console.Write(data);
        }

        public static void DisplayTree<T>(ref BinarySearchTree<T> tree, int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            //null check
            if (tree.root == null) return;

            //initial and operational data
            int topPosition = Console.CursorTop + topMargin;
            Node<T> next = tree.root;
            List<NodeInfo<T>> last = new();

            //building infotree
            for (int level = 0; next != null; ++level)
            {
                NodeInfo<T> item = new() { node = next };

                //adding to last and assignment to startPosition
                if (level < last.Count())
                {
                    item.startPosition = last[level].endPosition + spacing;
                    last[level] = item;
                }
                else
                {
                    item.startPosition = leftMargin;
                    last.Add(item);
                }

                //setting parent and limits for printing
                if (level > 0)
                {
                    item.parent = last[level - 1];

                    if (next == item.parent.node.left)
                    {
                        item.parent.left = item;
                        item.endPosition = Math.Max(item.endPosition, item.parent.startPosition - 1);
                    }
                    else
                    {
                        item.parent.right = item;
                        item.startPosition = Math.Max(item.startPosition, item.parent.endPosition + 1);
                    }
                }
                next = next.left ?? next.right;

                //for

            }
        }
    }
}
