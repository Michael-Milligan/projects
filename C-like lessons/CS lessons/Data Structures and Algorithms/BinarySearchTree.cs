using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    class BinarySearchTree<T>
    {
        public Node<T> Root = null;
        public IComparer<T> Comparer;

        public void Add(T Data)
        {
            if (Root == null)
            {
                Root = new Node<T>(Data);
                return;
            }

            Node<T> Current = Root;
            Node<T> Previous = Root;

            while (Current != null)
            {
                if (Comparer.Compare(Data, Current.Data) > 0)
                {
                    Previous = Current;
                    Current = Current.pLeft;
                }
                else if (Comparer.Compare(Data, Current.Data) < 0)
                {
                    Previous = Current;
                    Current = Current.pRight;
                }
                else throw new Exception("Tree contains already element with such a data");
            }
            Current = new Node<T>(Data);
            if (Comparer.Compare(Current.Data, Previous.Data) > 0) Previous.pLeft = Current;
            else Previous.pRight = Current;
        }

        public BinarySearchTree(IComparer<T> comparer)
        {
            Comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }
    }

    class Node<T>
    {
        public T Data;
        public Node<T> pLeft;
        public Node<T> pRight;

        public Node(T Data, ref Node<T> pLeft, ref Node<T> pRight)
        {
            this.Data = Data;
            this.pLeft = pLeft;
            this.pRight = pRight;
        }
        public Node(T Data)
        {
            this.Data = Data;
            pLeft = null;
            pRight = null;
        }
    }
}
