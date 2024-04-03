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
    private class Node
    {
        // Define Node structure for 2-3-4 Tree

    }

    public TwoThreeFourTree()
    {
        // Initialize TwoThreeFourTree
    }

    public bool Insert(T key)
    {
        // Insert method implementation
        return false;
    }

    public bool Delete(T key)
    {
        // Delete method implementation
        return false;
    }

    public bool Search(T key)
    {
        // Search method implementation
        return false;
    }

    public BSTforRBTree<T> Convert()
    {
        // Convert to Red-Black Tree method implementation
        return null;
    }

    public void Print()
    {
        // Print method implementation
    }



}

public class BSTforRBTree<T> where T : IComparable<T>
{
    // Define BSTforRBTree structure


}








