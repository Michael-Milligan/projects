using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections;
using System.IO;

namespace Data_Structures_and_Algorithms
{
    public class Program
    {
        public static void Main()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(new IntComparer());
            tree.Add(5);
            tree.Add(65);
        }
    }

    public class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x > y) return 1;
            else if (x == y) return 0;
            else return -1;
        }
    }
}
