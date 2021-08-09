using Data_Structures_and_Algorithms;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestProject
{
    public class BinarySearchTreeTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(32)]
        public void SuccessorAndPredecessorTest(int value)
        {
            BinarySearchTree<int> tree = BuildDefaultTree();

            Assert.AreEqual(15, tree.GetNodeInorderSuccessor(value).data);
            Assert.AreEqual(47, tree.GetNodeInorderPredecessor(value).data);
        }

        [Test]
        public void MinTreeValueTest()
        {
            BinarySearchTree<int> tree = BuildDefaultTree();
            Assert.AreEqual(1, tree.GetMinValue(tree.root).data);
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

        public static BinarySearchTree<int> BuildDefaultTree()
        {
            BinarySearchTree<int> tree = new(new IntComparer());
            tree.Add(15);
            tree.Add(62);
            tree.Add(48);
            tree.Add(10);
            tree.Add(32);
            tree.Add(78);
            tree.Add(2);
            tree.Add(1);
            tree.Add(47);
            tree.Add(3);

            return tree;
        }
    }
}