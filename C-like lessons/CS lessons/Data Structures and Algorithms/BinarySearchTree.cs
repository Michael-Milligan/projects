using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures_and_Algorithms
{
    class BinarySearchTree<T>
    {
        Node<T> Root;q = new 
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
    }
}
