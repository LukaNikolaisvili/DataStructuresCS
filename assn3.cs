/*

COIS-3020 ASSIGNMENT 2
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

    public int ComparePoints(int[] point1, int[] point2)
    {

        return 0;

    }



    private class Node
    {
        // Define Node structure

    }


    public static void Main(string[] args)
    {   int count = 1;
        while (count <=3)
        {
            TwoThreeFourTree<int> tree = new TwoThreeFourTree<int>();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Print();
            Console.WriteLine("Last Assignment Wooohooo!");
            Console.WriteLine(".\n" +
"   ……..…../´¯/)………… (\\¯`\\\n" +
"   …………/….//……….. …\\\\….\\\n" +
"   ………../….//………… ….\\\\….\\\n" +
"   …../´¯/…./´¯\\………../¯ `\\….\\¯`\\\n" +
"   .././…/…./…./.|_……_| .\\….\\….\\…\\\n" +
"   (.(….(….(…./.)..)..(..(. \\….)….)\n" +
"   .\\…………….\\/…/….\\. ..\\/……………./\n" +
"   ..\\…………….. /……..\\……………..…/\n" +
"    ….\\…………..(…………)……………./\n" +
"   ");
        count++;
        }


       
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








