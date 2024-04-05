/*
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
    private BinomialNode maxNode;

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
        maxNode = null;
    }

    public void Add(T item)
    {
        var newNode = new BinomialNode(item);
        if (!roots.ContainsKey(0))
        {
            roots[0] = new List<BinomialNode>();
        }
        roots[0].Add(newNode);

        if (maxNode == null || item.CompareTo(maxNode.Key) > 0)
        {
            maxNode = newNode;
        }
    }

    public T Front()
    {
        if (maxNode == null)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        return maxNode.Key;
    }

    public void Remove()
    {
        if (maxNode == null)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        Coalesce();
    }

    private void Coalesce()
    {
        if (maxNode == null)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        // Remove the tree containing the maxNode from the root list
        List<BinomialNode> rootList = roots[maxNode.Order];
        rootList.Remove(maxNode);

        // Insert the children of the removed tree into a new binomial heap H
        LazyBinomialHeap<T> H = new LazyBinomialHeap<T>();
        foreach (var child in maxNode.Children)
        {
            H.roots[child.Order] = new List<BinomialNode> { child };
        }

        // Merge H with the current binomial heap
        foreach (var kvp in H.roots)
        {
            int order = kvp.Key;
            List<BinomialNode> nodeList = kvp.Value;

            if (!roots.ContainsKey(order))
            {
                roots[order] = nodeList;
            }
            else
            {
                roots[order].AddRange(nodeList);
            }
        }

        // Update maxNode if necessary
        maxNode = null;
        foreach (var nodeList in roots.Values)
        {
            foreach (var node in nodeList)
            {
                if (maxNode == null || node.Key.CompareTo(maxNode.Key) > 0)
                {
                    maxNode = node;
                }
            }
        }
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
        // Create a new node of size 0 and set that as the root.
        Node<T> newNode = new Node<T>(0);
        root = newNode;
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
        // Set the current node as the root node
        Node<T> current = root;
        // Array that will hold the children which will be updated as code progresses
        Node<T>[] children;

        while (current != null) // Recursion without a recursive call
        {
            // Get the array of keys from the current node
            T[] keys = current.getKey();


            // For every key in the array,
            foreach (T key in keys)
            {
                // See if the key matches, if so return true!
                if (key.Equals(k))
                {
                    return true; // Key found
                }
            }

            // By reaching this point it means the keys did not match
            // Check if the current node is a leaf node, if so, return false.
            if (current.checkLeaf() == true)
            {
                return false;
            }
            // If it's not a leaf node proceed to the correct child subtree!
            else
            {
                // Get the array of children
                children = current.getChildren();
                // Get the amount of children from the array
                int length = children.Length;

                // Iterate through each key from left to right
                for (int i = 0; i < length; i++)
                {
                    // If the key is bigger than k
                    if (keys[i].CompareTo(k) > 0)
                    {
                        // Go to the child before the key
                        current = children[i];
                        // Break the loop and start over from the top!
                        break;
                    }
                    // Otherwise if the key is less than k
                    else if (keys[i].CompareTo(k) < 0)
                    {
                        // Check if the next key exists
                        if (keys[i + 1] != null)
                        {
                            // If it does check if the next key is greater than k
                            // If it isn't, the loop will simply go to the next iteration
                            // No additional code needed for that.
                            if (keys[i + 1].CompareTo(k) > 0)
                            {
                                // if it is move to the next child
                                current = children[i + 1];
                                // break out of the loop and start from the top
                                break;
                            }
                        }
                        // If there is no next key, go to the rightmost child
                        else
                        {
                            current = children[i + 1];
                            // break the loop and start from the top
                            break;
                        }
                    }
                }
            }
        }

        // Some error occurred in the logic
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
