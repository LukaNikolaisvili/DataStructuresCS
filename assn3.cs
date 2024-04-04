/*

COIS-3020 ASSIGNMENT 3
CONTRIBUTORS:
LUKA NIKOLAISVILI
FARZAD IMRAN 
FREDERICK NKWONTA
DUE DATE: APR 7th 2024

*/


using System;

public class PointQuadTree
{
    private Node[] B;

    public PointQuadTree(int dimensions)
    {
        // Initialize B array
        B = new Node[(int)Math.Pow(2, dimensions)];
    }

    // ComparePoints method compares two d-dimensional points and maps the result to an index
    public int ComparePoints(int[] point1, int[] point2)
    {
        int index = 0;
        for (int i = 0; i < point1.Length; i++)
        {
            // Update index based on the comparison of coordinates at each dimension
            if (point1[i] < point2[i])
                index = 2 * index + 1;  // Left branch
            else
                index = 2 * index + 2; // Right branch
        }
        return index;

    }



    private class Node
    {
        // Define Node structure

    }


    public static void Main(string[] args)
    {



        // Test the comparison of points
        PointQuadTree quadTree = new PointQuadTree(2); // 2-dimensional space
        int[] point1 = { 3, 4 };
        int[] point2 = { 2, 5 };

        int index = quadTree.ComparePoints(point1, point2);
        Console.WriteLine("Index: " + index);
        //         int count = 1;
        //         while (count <= 3)
        //         {
        //             TwoThreeFourTree<int> tree = new TwoThreeFourTree<int>();
        //             tree.Insert(1);
        //             tree.Insert(2);
        //             tree.Insert(3);
        //             tree.Print();
        //             Console.WriteLine("Last Assignment Wooohooo!");
        //             Console.WriteLine(".\n" +
        // "   ……..…../´¯/)………… (\\¯`\\\n" +
        // "   …………/….//……….. …\\\\….\\\n" +
        // "   ………../….//………… ….\\\\….\\\n" +
        // "   …../´¯/…./´¯\\………../¯ `\\….\\¯`\\\n" +
        // "   .././…/…./…./.|_……_| .\\….\\….\\…\\\n" +
        // "   (.(….(….(…./.)..)..(..(. \\….)….)\n" +
        // "   .\\…………….\\/…/….\\. ..\\/……………./\n" +
        // "   ..\\…………….. /……..\\……………..…/\n" +
        // "    ….\\…………..(…………)……………./\n" +
        // "   ");
        //             count++;
        //         }



    }


}

public class LazyBinomialHeap<T>
{
    private class BinomialNode
    {
        // Define BinomialNode structure

    }

    private BinomialNode[] B;

    public LazyBinomialHeap()
    {
        // Initialize Lazy Binomial Heap
    }

    public void Add(T item)
    {
        // Add method implementation
    }

    public T Front()
    {
        // Front method implementation
        return default(T);
    }

    public void Remove()
    {
        // Remove method implementation
    }

    public void Print()
    {
        // Print method implementation
    }

    private void Coalesce()
    {
        // Coalesce method implementation
    }


}

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

                p = children[0]; // bogus line for now, change with proper index and method to find index

                if () // some statement to find the right subtree index
                {

                }
                else // no correct subtree index. Exit
                {
                    return false;
                }
            }
        }

        // Key not found despite all of this
        // Return false
        return false;
    }

    // Builds and returns the equivalent red-black tree. (8 marks).
    public BSTforRBTree<T> Convert()
    {
        // To do, use the BSTforRBTree file provided (Patrick said so)
    }

    // Prints out the keys of the 2-3-4 tree in order. (4 marks)
    public void Print()
    {

    }

}








