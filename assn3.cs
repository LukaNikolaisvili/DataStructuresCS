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
            index = point1[i] < point2[i] ? 2 * index + 1 : 2 * index + 2;
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
    public TwoThreeFourTree()
    {
        // Constructor implementation
    }

    public bool Insert(T k)
    {
        // Implement the Insert method
        return false;
    }

    public bool Delete(T k)
    {
        // Implement the Delete method
        return false;
    }

    public bool Search(T k)
    {
        // Implement the Search method
        return false;
    }

    //we have to check this!!!

    // public BSTforRBTree<int> Convert()
    // {
    //     // Implement the Convert method
    //     return new BSTforRBTree<int>();
    // }

    public void Print()
    {
        // Implement the Print method
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
        int[] point1 = { 3, 4 };
        int[] point2 = { 2, 5 };
        int index = quadTree.ComparePoints(point1, point2);
        Console.WriteLine("\nIndex: " + index);

        Console.WriteLine("\nLazyBinomialHeap TESTING");
        Console.WriteLine("\n-------------------------\n");
        // Testing LazyBinomialHeap
        LazyBinomialHeap<int> heap = new LazyBinomialHeap<int>();
        heap.Add(1);
        heap.Add(2);
        heap.Add(3);
        heap.Add(4);
        heap.Add(5);
        heap.Print();
        Console.WriteLine("\n-------------------------\n");
        Console.WriteLine("\nBST TESTING");
        Console.WriteLine("\n-------------------------\n");
        BSTforRBTree<int> bst = new BSTforRBTree<int>();
        Color red = new Color();
        red = Color.RED;
        Color black = new Color();
        black = Color.BLACK;
        
        bst.Add(10,red);
        bst.Add(20,black);
        bst.Add(12,red);
        bst.Add(25,black);

        bst.Print();
        Console.WriteLine("\n-------------------------");


    }
}
