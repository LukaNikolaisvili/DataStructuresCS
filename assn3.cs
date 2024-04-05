﻿/*

COIS-3020 ASSIGNMENT 3
CONTRIBUTORS:
LUKA NIKOLAISVILI
FARZAD IMRAN 
FREDERICK NKWONTA
DUE DATE: APR 7th 2024

*/
using System;
using System.Collections.Generic;

// Part A: Point Quadtree
public class PointQuadTree
{
    private Node[] B;

    public PointQuadTree(int dimensions)
    {
        B = new Node[(int)Math.Pow(2, dimensions)];
    }

    public int ComparePoints(int[] point1, int[] point2)
    {
        int index = 0;

        for (int i = 0; i < point1.Length; i++)
        {
            index <<= 1;
            if (point1[i] >= point2[i])
            {
                index |= 1; // Set the last bit to 1 if point1's coordinate is greater than or equal to point2's
            }

        }
        return index;
    }

    private class Node
    {
        // Node structure implementation
    }
}

// Part B: Lazy Binomial Heap
public class LazyBinomialHeap<T> where T : IComparable<T>
{
    private Dictionary<int, List<BinomialNode>> roots;
    private BinomialNode minNode;

    private class BinomialNode
    {
        public T Key { get; set; }
        public List<BinomialNode> Children { get; set; }
        public int Order { get; set; }

        public BinomialNode(T key)
        {
            Key = key;
            Children = new List<BinomialNode>();
            Order = 0;
        }
    }

    public LazyBinomialHeap()
    {
        roots = new Dictionary<int, List<BinomialNode>>();
        minNode = null;
    }

    public void Add(T item)
    {
        var newNode = new BinomialNode(item);
        if (!roots.ContainsKey(0))
        {
            roots[0] = new List<BinomialNode>();
        }
        roots[0].Add(newNode);

        if (minNode == null || item.CompareTo(minNode.Key) < 0)
        {
            minNode = newNode;
        }
    }

    public T Front()
    {
        if (minNode == null)
        {
            throw new InvalidOperationException("Heap is empty");
        }
        return minNode.Key;
    }

    public void Remove()
    {
        // Implement the Remove method
    }

    public void Print()
    {
        foreach (var nodeList in roots.Values)
        {
            foreach (var node in nodeList)
            {
                PrintTree(node, 0);
                Console.WriteLine(); // For better readability between trees
            }
        }
    }

    private void PrintTree(BinomialNode node, int depth)
    {
        Console.Write(new string(' ', depth * 2) + node.Key);
        foreach (var child in node.Children)
        {
            Console.WriteLine();
            PrintTree(child, depth + 1);
        }
    }
}

// Part C: 2-3-4 Trees to Red Black Trees
public class TwoThreeFourTree<T> where T : IComparable<T>
{
    private Node<T> root;
    private class Node<T>
    {
        private int n; // number of keys
        private bool leaf; // true if a leaf node; false otherwise
        private T[] key; // array of keys
        private Node<T>[] c; // array of child references
        public Node(int t)
        {
            n = 0;
            leaf = true;
            key = new T[2 * t - 1];
            c = new Node<T>[2 * t];
        }
        //Public getter method for the key array
        public T[] getKey()
        {
            return key;
        }

        // Public method to check if the value is a leaf node
        public bool checkLeaf()
        {
            return leaf;
        }

        // Public method to get child node references
        public Node<T>[] getChildren()
        {
            return c;
        }

    }
    // Initializes an empty 2-3-4 tree. (2 marks)
    public TwoThreeFourTree()
    {
        root = null;
    }

    // Returns true if key k is successfully inserted; false otherwise. (6 marks)
    public bool Insert(T k)
    {
        return false;
    }

    // Returns true if key k is successfully deleted; false otherwise. (10 marks)
    public bool Delete(T k)
    {
        return false;
    }

    // Returns true if key k is found; false otherwise (4 marks).
    public bool Search(T k)
    {
        Node<T> p = root;
        T[] keys = p.getKey();
        Node<T>[] children;
        int length = 0;

        while (p != null) // Recursion without a recursive call
        {
            foreach (T key in keys)
            {
                if (key.Equals(k))
                {
                    return true; // Key found
                }
            }

            if (p.checkLeaf() == true)
            {
                return false;
            }
            else
            {
                // Here onwards is iffy and might be completely incorrect process wise, Instructions are as follows:

                /*  If the key k is not found and p is a leaf node then return false
                    else move to the subtree which would otherwise contain k    */
                // I do not fully know the logic to find the right subtree and dont want to use a for loop for each

                children = p.getChildren();
                length = children.Length;

                // p = children[0]; // bogus line for now, change with proper index and method to find index

                // // if () // some statement to find the right subtree index
                // // {

                // // }
                // else // no correct subtree index. Exit
                // {
                //     return false;
                // }
            }
        }

        // Key not found despite all of this
        // Return false
        return false;
    }

    // Builds and returns the equivalent red-black tree. (8 marks).
    // public BSTforRBTree<int> Convert()
    // {   
    //     // To do, use the BSTforRBTree file provided (Patrick said so)
    // }

    // Prints out the keys of the 2-3-4 tree in order. (4 marks)
    public void Print()
    {

    }

}

public enum Color { RED, BLACK }; // Colors of the red-black tree

public interface ISearchable<T>
{
    void Add(T item, Color rb);
    void Print();
}

//-------------------------------------------------------------------------

// Implementation:  BSTforRBTree
public class BSTforRBTree<T> : ISearchable<T> where T : IComparable
{
    // Common generic node class for a BSTforRBTree
    private class Node
    {

        // Read/write properties
        public T Item;
        public Color RB;
        public Node Left;
        public Node Right;

        public Node(T item, Color rb)
        {
            Item = item;
            RB = rb;
            Left = Right = null;
        }
    }

    private Node root;

    public BSTforRBTree()
    {
        root = null; // Empty BSTforRBTree
    }

    // Add 
    // Insert an item into a BSTforRBTRee
    // Duplicate items are not inserted
    // Worst case time complexity:  O(log n) 
    // since the maximum depth of a red-black tree is O(log n)

    public void Add(T item, Color rb)
    {
        Node curr;
        bool inserted = false;

        if (root == null)
            root = new Node(item, rb);   // Create a root
        else
        {
            curr = root;
            while (!inserted)
            {
                if (item.CompareTo(curr.Item) < 0)
                {
                    if (curr.Left == null)              // Empty spot
                    {
                        curr.Left = new Node(item, rb);
                        inserted = true;
                    }
                    else
                        curr = curr.Left;               // Move left
                }
                else
                    if (item.CompareTo(curr.Item) > 0)
                {
                    if (curr.Right == null)         // Empty spot
                    {
                        curr.Right = new Node(item, rb);
                        inserted = true;
                    }
                    else
                        curr = curr.Right;          // Move right
                }
                else
                    inserted = true;                // Already inserted
            }
        }
    }

    public void Print()
    {
        Print(root, 0);                // Call private, recursive Print
        Console.WriteLine();
    }

    // Print
    // Inorder traversal of the BSTforRBTree
    // Time complexity:  O(n)

    private void Print(Node node, int k)
    {
        string s;
        string t = new string(' ', k);

        if (node != null)
        {
            Print(node.Right, k + 4);
            s = node.RB == Color.RED ? "R" : "B";
            Console.WriteLine(t + node.Item.ToString() + s);
            Print(node.Left, k + 4);
        }
    }
}

// Main class for testing
public class Program
{
    public static void Main(string[] args)
    {

        // Quadtree
        PointQuadTree quadTree = new PointQuadTree(2); // 2-dimensional space
        int[] point1 = { 1, 0 }; // 10 is 2 in binary       00 - 0, 01 - 1, 10 - 2, 11 - 3,
        int[] point2 = { 1, 1 }; //11 is 3 in binary
        int index = quadTree.ComparePoints(point1, point2);
        int index2 = quadTree.ComparePoints(point2, point1);

        // Testing LazyBinomialHeap
        LazyBinomialHeap<int> heap = new LazyBinomialHeap<int>();

        BSTforRBTree<int> bst = new BSTforRBTree<int>();
        Color red = new Color();
        red = Color.RED;
        Color black = new Color();
        black = Color.BLACK;

        Console.WriteLine("\nHello there!");
        bool flag = false;

        flag = true;
        while (flag)
        {

            Console.WriteLine("\nChoose any of those operations: ");
            Console.WriteLine("\n1 - print\n2 - blabla");
            Console.WriteLine("-----------------");


            Console.WriteLine("Enter the UID of the operation: ");

            string op = Console.ReadLine();

            if (op == "1")
            {
                Console.WriteLine("// Quadtree");
                Console.WriteLine("\nIndex 1 : " + index + "\nIndex 2 : " + index2);

            }

            if (op == "2")
            {
                Console.WriteLine("\n-------------------------\n");
                heap.Add(1);
                heap.Add(2);
                heap.Add(3);
                heap.Add(4);
                heap.Add(5);
                heap.Print();
                Console.WriteLine("\n-------------------------\n");

            }


            if (op == "3")
            {

                bst.Add(10, red);
                bst.Add(20, black);
                bst.Add(12, red);
                bst.Add(25, black);

                bst.Print();
                Console.WriteLine("\n-------------------------");

            }

            else if (op == "x")
            {
                Console.WriteLine("Exiting...");
                flag = false;

                if (flag == false)
                {
                    Console.WriteLine("program exited succesfully...");
                }


            }

        }

    }
}
