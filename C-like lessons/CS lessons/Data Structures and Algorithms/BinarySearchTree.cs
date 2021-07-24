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
        public List<int[]> levelItemsMap = new();
        public IComparer<T> comparer;
        private static int levelCount;

        public delegate void Action<T>(T Data, Stream stream);

        public void Add(T Data)
        {
            if (root == null)
            {
                root = new Node<T>(Data);
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

        public void PrintTree(Stream outputStream)
        {
            object lockObject = new();
            lock (lockObject)
            {
                levelCount = 0;
                PreorderTraversal(root, new Action<T>(PrintNode), outputStream);
                levelCount = 0;
            }
        }

        public void PrintNode(T Data, Stream outputStream)
        {

        }
    }

    class Node<T>
    {
        public T data;
        public Node<T> pLeft;
        public Node<T> pRight;
        public int level;
        public int position;

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
}
