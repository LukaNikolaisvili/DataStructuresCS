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
using System.Drawing;

// Part A: Point Quadtree
public class PointQuadTree
{
    private Node[] B; //Array for storing node 

    // used to initialize the quadtree with dimenions 
    public PointQuadTree(int dimensions)
    {

        // used to initialize an array size based on the dimensions 
        B = new Node[(int)Math.Pow(2, dimensions)];
    }

    // method used to compare 2 point to determine their index in the quadtree
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
        return index;  // return the calcuted ined 
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
    // keep track of the biggest item in the heap.
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

    //The constructor,used in settings up the heap.
    public LazyBinomialHeap()
    {
        roots = new Dictionary<int, List<BinomialNode>>();
        maxNode = null;
    }
    // Adds a new item into the heap.
    public void Add(T item)
    {

        var newNode = new BinomialNode(item);  // create a new node for the item

        //vaildates the new item add 
        if (!roots.ContainsKey(0))
        {
            roots[0] = new List<BinomialNode>();
        }
        //Adds the new item to the list.
        roots[0].Add(newNode);


        //check if the new item is the biggest then save it as maxnode .
        if (maxNode == null || item.CompareTo(maxNode.Key) > 0)
        {
            maxNode = newNode;
        }
    }
    // This method get the biggest item in the heap.
    public T Front()
    {
        // checks that there is item in the heap
        if (maxNode == null)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        return maxNode.Key;
    }
    // This method removes the biggest item from the heap.
    public void Remove()
    {

        if (maxNode == null)
        {
            try
            {
                throw new NullReferenceException("Heap is empty");
            }
            catch
            {
                throw new Exception();
            }

        }

        // calls the method 
        Coalesce();
    }

    //This method helps to put together or merge the trees in the heap
    private void Coalesce()
    {
        // Checks if there are no items in the heap
        if (maxNode == null)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        // Remove the tree containing the maxNode from the root list
        List<BinomialNode> rootList = roots[maxNode.Order];
        rootList.Remove(maxNode);

        // Insert the children of the removed tree into a new binomial heap called H
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
            //checking if the roots does not contain the order key
            if (!roots.ContainsKey(order))
            {   //roots binomialnode at index order is nodelist
                roots[order] = nodeList;
            }
            //otherwise we will call the AddRange
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
            {   //checking if the maxNode is null or if we compare the nodes key with maxnode and it will be > 0 we will found the maxnode
                if (maxNode == null || node.Key.CompareTo(maxNode.Key) > 0)
                {   //so we make the maxnode to be the node
                    maxNode = node;
                }
            }
        }
    }

    public void Print()
    {
        //printing each nodelist that will be in the roots binomialNode
        foreach (var nodeList in roots.Values)
        {   //traversing through the each node in the nodelist
            foreach (var node in nodeList)
            {   //printing the node
                PrintTree(node, 0);
                Console.WriteLine(); // For better readability between trees
            }
        }
    }

    //for recursive calls the private method of the PrintTree that accepts 2 parameters 1) node, 2) depth
    private void PrintTree(BinomialNode node, int depth)
    {
        //it will print new string object all the time depth * 2 with the key assigned to it
        Console.Write(new string(' ', depth * 2) + node.Key);
        foreach (var child in node.Children)
        {   //for better readability
            Console.WriteLine();
            //recursive call
            PrintTree(child, depth + 1);
        }
    }
}


// Part C: 2-3-4 Trees to Red Black Trees
public class TwoThreeFourTree<T> where T : IComparable<T>
{
    private Node<T> root;

    public bool IsEmpty()
    {
        // The tree is considered empty if the root is null or the root has no keys
        return root == null || root.getKeyNum() == 0;
    }

    private class Node<U> where U : IComparable<U>
    {

        public U[] key;
        public Node<U>[] children;
        private int n; // number of keys
        private bool leaf; // true if a leaf node; false otherwise
        private Node<T>[] c; // array of child references
        public Node(int t)
        {
            n = 0;
            leaf = true;
            key = new U[2 * t - 1];
            c = new Node<T>[2 * t];
        }
        // Public getter method for the key array
        public U[] getKey()
        {
            return key;
        }

        public int getKeyNum()
        {
            return n;
        }
        public void updateKeyNum(int check)
        {
            // if the passed value is 0, increment number of keys
            if (check == 0)
            {
                n++;
            }
            // if the passed value is 1, decrement number of keys
            else if (check == 1)
            {
                n--;
            }

            // if the number of keys is less than 0, set it to 0.
            if (n < 0)
            {
                n = 0;
            }

        }

        // Public add method for the key array
        public void addKey(U value)
        {
            int numkeys = getKeyNum();
            if (numkeys >= 3)
            {
                throw new IndexOutOfRangeException("Cannot insert, the node is full");
            }

            // Find the correct position to insert the new key while maintaining ascending order
            int insertIndex = 0;
            while (insertIndex < numkeys && key[insertIndex] != null && key[insertIndex].CompareTo(value) < 0)
            {
                insertIndex++;
            }

            // Shift keys to make space for the new key
            for (int i = n; i > insertIndex; i--)
            {
                key[i] = key[i - 1];
            }

            // Insert the new key at the correct position
            key[insertIndex] = value;

            // Increment number of keys
            updateKeyNum(0);
        }

        // Public remove method for the key array
        public void removeKey(int index)
        {
            // Error if index is out of bounds
            if (index > 2 || index < 0)
            {
                throw new IndexOutOfRangeException("Index is out of bounds.");
            }
            else
            {
                // For every key starting from the index, 
                // shift neighbouring key to the left
                for (int i = index; i < 2; i++)
                {
                    key[i] = key[i + 1];
                }
                // Set the last element to default value
                key[2] = default;
                // Decrement number of keys
                updateKeyNum(1);
            }
            return;

        }

        // Public method to check if the value is a leaf node
        public bool checkLeaf()
        {
            return leaf;
        }
        // Public setter method for leaf variable, 0 for leaf node, 1 for non-leaf
        public void setLeaf(int check)
        {
            // if the passed value is 0, set parameter to true
            if (check == 0)
            {
                leaf = true;
            }
            // if the passed value is 1, set parameter to false
            else if (check == 1)
            {
                leaf = false;
            }
        }

        // Public method to get child node references
        public Node<T>[] getChildren()
        {
            return c;
        }

        // Public method to add a child to the array
        public void addChild(int index, Node<T> child)
        {
            // Error statement if index is out of bounds
            if (index > 3 || index < 0)
            {
                throw new IndexOutOfRangeException("Index is out of bounds.");
            }
            // if the child at the current index slot is full, shift to the right
            if (c[index] != null)
            {
                // Shift each child right until index
                for (int i = 3; i > index; i--)
                {
                    c[i] = c[i - 1];
                }
            }
            // Now insert the value at the index
            setLeaf(1);
            c[index] = child;
            return;
        }

        // Public method to remove a child from the array
        public void removeChild(int index)
        {
            // Error if index is out of bounds
            if (index > 3 || index < 0)
            {
                throw new IndexOutOfRangeException("Index is out of bounds.");
            }
            else
            {
                // For every key starting from the index, 
                // shift neighbouring key to the left
                for (int i = index; i < 3; i++)
                {
                    c[i] = c[i + 1];
                }
                // Set the last element to default value
                c[3] = null;
            }

            return;

        }

    }

    // Private method for split
    private void Split(Node<T> x, int i)
    {

        // Create a new node to hold the other keys
        Node<T> q = new Node<T>(2);
        // Array to hold the children of node x
        Node<T>[] children = x.getChildren();

        Node<T> p = children[i];    // Set p as the child at index i
        T[] keys = p.getKey();      // Get the keys of the child node p
        T value = keys[2];          // Get the last key from the array

        // Move the last key of p into q
        q.addKey(value);
        // Remove the last key of p
        p.removeKey(2);

        // If p is not a leaf node, continue with split
        if (!p.checkLeaf())
        {
            // If p isn't a leaf node, copy the last half of its children to q.
            for (int j = 2; j < 4; j++)
            {
                q.addChild(j, children[j]);
                p.removeChild(j);
            }
        }

        // Insert the median key from p to x at position i
        x.addKey(keys[1]);
        p.removeKey(1);

        // Add reference to node q as the child at i+1 (to the right of p)
        x.addChild(i + 1, q);

    }


    // Initializes an empty 2-3-4 tree. (2 marks)
    public TwoThreeFourTree()
    {
        // Create a new node of size 2 (For the 2-3-4 tree) and set that as the root.
        Node<T> newNode = new Node<T>(2);
        root = newNode;
    }

    // Returns true if key k is successfully inserted; false otherwise. (6 marks)
    public bool Insert(T k)
    {
        // Set the current node as the root node
        Node<T> p = root;
        // Node to hold reference to parent node
        Node<T> parent = p;
        int max = 3; // maximum number of keys something can hold (2t-1, where t is always 2)
        // This will hold the number of items in the keys array and update as code progresses
        int index = p.getKeyNum();
        // Array that will hold the children which will be updated as code progresses
        Node<T>[] children;

        // This number will hold the index of the child we went down!
        int childindex = 0;

        // If the current root node is full, split and make a new one!
        if (index >= max)
        {
            // Root is full, make a new root and then split.
            root = new Node<T>(2);

            // Set the first child of the new root to p (the old root)
            root.addChild(childindex, p);

            // Now split the first child (full) of the new root
            Split(root, childindex);

            // Set current node as the new root for proper traversal
            p = root;
        }

        // Continue with the rest of insert
        while (p != null) // Recursion without a recursive call
        {
            // Get the array of keys from the current node
            // Array that will hold the keys which will be updated as code progresses
            T[] keys = p.getKey();

            // update index based on new keys array
            index = p.getKeyNum();

            // Check if the current node is a leaf node
            // Insert here!
            if (p.checkLeaf() == true)
            {
                // Check if the key already exists or not
                for (int j = 0; j < index; j++)
                {
                    // See if the key matches, if so stop the insertion
                    if (keys[j].Equals(k))
                    {
                        Console.WriteLine("Key already exists. Insertion cancelled");
                        return false;
                    }
                }

                // If for some reason we ended up in a leaf node with maximum keys
                if (index >= max)
                {
                    // split the node
                    Split(parent, childindex);
                    // Set the current node back as the parent so insertions can properly occur
                    p = parent;
                    break;
                }
                // Add the key to the correct slot and return true.
                p.addKey(k);
                return true;
            }
            // If it's not a leaf node proceed to the correct child subtree!
            else
            {
                // Update the array of keys
                children = p.getChildren();
                // Get the amount of keys from the array
                int length = p.getKeyNum();

                // Iterate through each key from left to right
                for (int i = 0; i < length; i++)
                {
                    // See if the key matches, if so stop the insertion
                    if (keys[i].Equals(k))
                    {
                        Console.WriteLine("Key already exists. Insertion cancelled");
                        return false;
                    }
                    // If the key is bigger than k
                    if (keys[i].CompareTo(k) > 0)
                    {
                        // Set child index to the current key index (The child left of the key!)
                        childindex = i;
                        if (children[childindex] == null)
                        {
                            p.addChild(childindex, new Node<T>(2));
                        }
                        index = children[childindex].getKeyNum();
                        // If child is full, split
                        if (index >= max)
                        {
                            Split(p, childindex);
                            p = root;
                            break; // Set p back to the root and traverse down again to ensure proper key insertions
                        }
                        // Set parent as the current node
                        parent = p;
                        // Proceed to the child
                        p = children[childindex];
                        // Break the loop and start over from the top!
                        break;
                    }
                    // Otherwise if the key is less than k
                    else if (keys[i].CompareTo(k) < 0)
                    {
                        // Check if the next key exists
                        if (i + 1 < index)
                        {
                            // If it does check if the next key is greater than k
                            // If it isn't, the loop will simply go to the next iteration
                            // No additional code needed for that.
                            if (keys[i + 1].CompareTo(k) > 0)
                            {
                                // Set child index to i+1 (the child right of the current key!)
                                childindex = i + 1;
                                if (children[childindex] == null)
                                {
                                    // Add a child if one does not exist at the location!
                                    p.addChild(childindex, new Node<T>(2));
                                }
                                index = children[childindex].getKeyNum();
                                // If child is full, split
                                if (index >= max)
                                {
                                    // Split and return back to root.
                                    Split(p, childindex);
                                    p = root;
                                    break;
                                }
                                // Set parent as the current node
                                parent = p;
                                // Then move to the next child
                                p = children[childindex];
                                // break out of the loop and start from the top
                                break;
                            }
                        }
                        // If there is no next key, go to the rightmost child
                        else
                        {
                            // Set child index to i+1 (the child right of the current key!)
                            childindex = i + 1;
                            if (childindex >= max)
                            {
                                // Make sure child is in bounds
                                childindex = 3;
                            }
                            if (children[childindex] == null)
                            {
                                // Add a child if one does not exist at the location!
                                p.addChild(childindex, new Node<T>(2));
                            }
                            index = children[childindex].getKeyNum();
                            // If child is full, split
                            if (index >= max)
                            {
                                // Split and return back to root.
                                Split(p, childindex);
                                p = root;
                                break;
                            }
                            // Set the parent node as the current node
                            parent = p;
                            // Then set the current node as the rightmost child
                            p = children[childindex];
                            // break the loop and start from the top
                            break;
                        }
                    }
                }
            }
        }
        // No insertion, return false
        return false;
    }

    // Returns true if key k is successfully deleted; false otherwise. (10 marks)
    public bool Delete(T k)
    {
        // if the root is null return false
        if (root == null || (root.getKeyNum() == 0 && root.getChildren()[0] == null))
        {
            Console.WriteLine("The tree is empty. You cannot delete anything else.");
            return false;
        }
        // recursivly call helper method, starting at the root
        bool deleted = Delete(root, k);
        // return the result of the deletion
        return deleted;
    }

    // Helper function for Delete to call recursively and go down the tree
    private bool Delete(Node<T> node, T k)
    {
        // if the root is null return false
        if (node == null)
        {
            return false;
        }

        // Store the keys of the current node in the keys array
        T[] keys = node.getKey();
        // Store the children of the current node
        Node<T>[] children = node.getChildren();

        // Incremental index
        int i = 0;

        // If index is in bounds, not null, and less than k 
        while (i < node.getKeyNum() && keys[i] != null && keys[i].CompareTo(k) < 0)
        {
            // increment the index up until it isn't
            i++;
        }

        // If the index is in bounds, not null, and equal to k
        if (i < node.getKeyNum() && keys[i] != null && keys[i].CompareTo(k) == 0)
        {
            // remove the key and return true
            node.removeKey(i);
            return true;
        }

        // Otherwise check if the curren node is not a leaf node
        else if (!node.checkLeaf())
        {
            // Set the child of the current node to the entry at the index
            Node<T> child = children[i];
            // If left child is out of bounds (i = 0), move the index up to 1.
            // This will set the left child as the current child as well!

            // Get the left child of current node, and Right child of current node based on i
            Node<T> childLeft = children[i];
            if (childLeft == null)
            {
                childLeft = child;
            }
            Node<T> childRight = children[i];
            if (childRight == null)
            {
                childRight = child;
            }

            // Get the key arrays
            T[] keysR = childRight.getKey();
            T[] keysL = childLeft.getKey();
            // Get the number of keys in the current child
            int childKeyNum = child.getKeyNum();
            bool childDeleted = Delete(child, k);

            // If child wasn't deleted
            if (!childDeleted)
            {
                // Check if the number of keys is less than t (2)
                if (childKeyNum < 2)
                {
                    // If right sibling isn't null and has more than 2 keys
                    if (childRight != null && childRight.getKeyNum() > 1)
                    {
                        // Borrow a key from the right sibling
                        T borrowedKey = keysR[0];
                        childRight.removeKey(0);
                        node.addKey(borrowedKey);
                        child.addKey(keys[i]);
                        return true;
                    }
                    // Otherwise look towards the left sibling
                    else if (childLeft != null && childLeft.getKeyNum() > 1)
                    {
                        // Borrow a key from the left sibling
                        int keyindex = childLeft.getKeyNum() - 1;
                        T borrowedKey = keysL[keyindex];
                        childLeft.removeKey(keyindex);
                        node.addKey(keys[i - 1]);
                        keys[i - 1] = borrowedKey;
                        return true;
                    }
                    // If the right sibling isn't null and doesnn't have enough keys
                    else if (childRight != null)
                    {
                        // Merge with right sibling
                        Merge(node, i);
                    }
                    // If the left sibling isn't null and doesnn't have enough keys
                    else if (childLeft != null)
                    {
                        // Merge with left sibling
                        Merge(node, i - 1);
                    }
                }
            }
        }

        if (root.getKeyNum() == 0)
        {
            if (root.getChildren()[0] != null)
            {
                root = root.getChildren()[0];
            }
        }

        return false;
    }


    // Private method for Merge, merges right child into left
    private void Merge(Node<T> x, int i)
    {
        Node<T>[] children = x.getChildren();
        // if the right child would be out of bounds, move index left
        if (i + 1 <= children.Length)
        {
            i--;
        }
        // Store the siblings
        Node<T> left = children[i];
        Node<T> right = children[i + 1];
        // Store the current keys
        T[] keys = x.getKey();
        // Store keys of right sibling
        T[] keysR = right.getKey();
        // Store the number of keys in left and right
        int rightNum = right.getKeyNum();
        int leftnum = left.getKeyNum();


        // Add the key from node x at index i to the left node
        left.addKey(keys[i]);

        // Move keys from the right node to the left node
        for (int j = 0; j < rightNum; j++)
        {
            left.addKey(keysR[j]);
        }

        // If the right node is not a leaf, move its children to the left node
        if (!right.checkLeaf())
        {
            for (int j = 0; j < rightNum; j++)
            {
                left.addChild(leftnum, right.getChildren()[j]);
            }
        }

        if (x == root && x.getKeyNum() == 0 && x.getChildren()[0] != null)
        {
            // If the leftmost child is full, we need to merge it with its sibling
            if (x.getChildren()[0].getKeyNum() == 3)
            {

                if (x.getChildren()[1] != null)
                {

                    Merge(x, 0);
                    root = x.getChildren()[0]; // The merge operation will ensure the first child is now the new root
                }
                else
                {
                    // Handle the case where there is no sibling to merge with This would be a special case handling
                    root = x.getChildren()[0];
                }
            }
            else
            {
                // If the leftmost child is not full, it can safely become the new root
                root = x.getChildren()[0];
            }
        }

        // Remove the key from node x at index i
        x.removeKey(i);

        // Remove the right node from x
        x.removeChild(i + 1);
    }

    // Returns true if key k is found; false otherwise (4 marks).
    public bool Search(T k)
    {
        // Set the current node as the root node
        Node<T> current = root;
        // Set the parent node also as the root node
        Node<T> parent = root;
        // Array that will hold the children which will be updated as code progresses
        Node<T>[] children;
        // Index to hold what child to go down!
        int childindex = 0;
        // index to hold number of children
        int numchildren = 0;
        // Maximum number of keys
        int max = 3;

        while (current != null) // Recursion without a recursive call
        {
            Node<T> check = current;
            // Get the array of keys from the current node
            // Array that will hold the keys which will be updated as code progresses
            T[] keys = current.getKey();

            // update index based on new keys array
            int index = current.getKeyNum();

            // Update the array of keys
            children = current.getChildren();

            for (int j = 0; j < 4; j++)
            {
                if (children[j] != null)
                {
                    numchildren++;
                }
            }

            // Iterate through each key from left to right
            for (int i = 0; i < index; i++)
            {
                // See if the key matches, if so stop the search
                if (keys[i].Equals(k))
                {
                    return true;
                }
                // If the key is bigger than k
                if (keys[i].CompareTo(k) > 0)
                {
                    // Set child index to the current key index (The child left of the key!)
                    childindex = i;
                    if (children[childindex] == null)
                    {
                        // No child here, so no key. So break out
                        break;
                    }
                    // Set parent as the current node
                    parent = current;
                    // Proceed to the child
                    current = children[childindex];
                    // Break the loop and start over from the top!
                    break;
                }
                // Otherwise if the key is less than k
                else if (keys[i].CompareTo(k) < 0)
                {
                    // Check if the next key exists
                    if (i + 1 < index)
                    {
                        // If it does check if the next key is greater than k
                        // If it isn't, the loop will simply go to the next iteration
                        // No additional code needed for that.
                        if (keys[i + 1].CompareTo(k) > 0)
                        {
                            // Set child index to i+1 (the child right of the current key!)
                            childindex = i + 1;
                            if (children[childindex] == null)
                            {
                                // No child here, so no key. So break out
                                break;
                            }
                            index = children[childindex].getKeyNum();
                            // Set parent as the current node
                            parent = current;
                            // Then move to the next child
                            current = children[childindex];
                            // break out of the loop and start from the top
                            break;
                        }
                    }
                    // If there is no next key, go to the rightmost child
                    else
                    {
                        // Set child index to i+1 (the child right of the current key!)
                        childindex = i + 1;
                        if (childindex >= max)
                        {
                            // Make sure child is in bounds
                            childindex = 3;
                        }
                        if (children[childindex] == null)
                        {
                            // No child here, so no key. So break out
                            break;
                        }
                        index = children[childindex].getKeyNum();
                        // Set the parent node as the current node
                        parent = current;
                        // Then set the current node as the rightmost child
                        current = children[childindex];
                        // break the loop and start from the top
                        break;
                    }
                }
            }
            // If we're in a leaf node, and we haven't found the key
            if (check.checkLeaf())
            {
                // Get the children of the parent node again
                children = parent.getChildren();
                // Get number of children
                numchildren = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (children[j] != null)
                    {
                        numchildren++;
                    }
                }
                // Search for the index of the current child
                for (int i = 0; i < 4; i++)
                {
                    // Once we find it, store it and break out
                    if (children[i] == current)
                    {
                        childindex = i;
                        break;
                    }
                    else
                    {
                        // set child index to out of bounds until otherwise.
                        childindex = 5;
                    }
                }
                // Now based on what the childindex variable is, in bounds or out of bounds
                if (childindex < numchildren)
                {
                    // Go down the next sibling
                    current = children[childindex + 1];
                }
                else
                {
                    // No more siblings, no keys found, finally return false.
                    return false;
                }
            }
        }
        // No result, return false
        return false;
    }



    // Builds and returns the equivalent red-black tree. (8 marks)
    public BSTforRBTree<T> Convert()
    {
        // Create new Red Black Tree structure
        BSTforRBTree<T> rbTree = new BSTforRBTree<T>();
        // Create a list to hold all keys in order.
        List<T> keys = new List<T>();
        // Set the default colour for nodes to red
        // This is so the root node who hahs no parents,
        // will in turn be black.
        Color defaultColor = Color.RED;
        // Recursive in order traversal to get the keys, starting from root
        InOrderTraversal(root, keys);
        // After recursion ends, build a balanced RB Tree based on the keys scraped.
        rbTree = BuildRBTree(keys, defaultColor);
        return rbTree;
    }

    private void InOrderTraversal(Node<T> current, List<T> keys)
    {
        // Make sure current node isn't null
        if (current == null)
        {
            return;
        }
        // Store the key array
        T[] keyitems = current.getKey();

        // Store the children array
        Node<T>[] children = current.getChildren();
        int childindex = children.Length - 1;

        // For every integer up until the last child
        for (int i = 0; i < childindex; i++)
        {
            // perform an in-order traversal and then add the keys to the list on return.
            InOrderTraversal(children[i], keys);
            // Only add to keys array if the value is not default!
            if (keyitems[i].CompareTo(default(T)) != 0)
            {
                keys.Add(keyitems[i]);
            }
        }
        // Perform a last in-order traversal on the last child
        InOrderTraversal(children[childindex], keys);
        keys.Sort();
    }

    // Private method for building the tree, called helper build method
    private BSTforRBTree<T> BuildRBTree(List<T> keys, Color defaultColor)
    {
        BSTforRBTree<T> rbTree = new BSTforRBTree<T>();
        BuildRBTreeRecursive(keys, rbTree, defaultColor);
        return rbTree;
    }

    // Helper build method, requires a key list, a tree structure and the colour of the parent node.
    private void BuildRBTreeRecursive(List<T> keys, BSTforRBTree<T> rbTree, Color parentColor)
    {
        // If the list iis empty, exit the method
        if (keys.Count == 0)
        {
            return;
        }

        // obtain the key in the middle of the list to use as the root
        int mid = keys.Count / 2;
        T midKey = keys[mid];
        // Variable to adjust with the current node's colour
        Color currentNodeColor;

        // If parent is red, make the child black
        if (parentColor == Color.BLACK)
        {
            currentNodeColor = Color.RED;
        }
        // if parent is black, make child red
        else
        {
            currentNodeColor = Color.BLACK;
        }

        // Add the middle key and current node to the tree structure
        rbTree.Add(midKey, currentNodeColor);

        // Now use the two halves of thhe list to build the tree recursively
        // Lists will always have the subtree root at the middle in order to balance
        List<T> leftKeys = keys.GetRange(0, mid);
        List<T> rightKeys = keys.GetRange(mid + 1, keys.Count - mid - 1);

        // This call will build the left subtree
        BuildRBTreeRecursive(leftKeys, rbTree, currentNodeColor);
        // This call will build the right subtree
        BuildRBTreeRecursive(rightKeys, rbTree, currentNodeColor);
    }




    public void PrintBTree()
    {
        if (root == null) // Check if the tree is empty
        {
            Console.WriteLine("The tree is empty.");
            return;
        }

        PrintNode(root, 0); // Start printing from the root with an indentation level of 0
    }

    private void PrintNode(Node<T> node, int indentation)
    {
        if (node == null)
        {
            return;
        }

        int childIndentation = indentation + 1;
        Node<T>[] children = node.getChildren(); // Assuming GetChildren returns an array of child nodes
        T[] keys = node.getKey(); // Assuming GetKey returns an array of keys

        // Print the keys of the current node, excluding zeros
        List<T> nonZeroKeys = keys.Where(key => !key.Equals(default(T)) && !key.Equals(0)).ToList();
        if (nonZeroKeys.Count > 0)
        {
            Console.Write(new string(' ', indentation * 4)); // 4 spaces for each level of indentation
            Console.Write("(Keys: ");
            for (int i = 0; i < nonZeroKeys.Count; i++)
            {
                Console.Write(nonZeroKeys[i]);
                if (i < nonZeroKeys.Count - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.Write(") ");
            Console.WriteLine();
        }

        // Recursively print children nodes
        for (int i = 0; i < children.Length; i++)
        {
            PrintNode(children[i], childIndentation);
        }
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
public class BSTforRBTree<T> : ISearchable<T> where T : IComparable<T>
{
    private class Node
    {
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
                        curr = curr.Left;               // Move to the left
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
                        curr = curr.Right;          // Move to the right
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

        Console.WriteLine("\nHello there!");
        bool flag = false;

        flag = true;
        while (flag)
        {

            Console.WriteLine("\nChoose any of those operations: ");
            Console.WriteLine("\n1 - Part A (Quad Trees) \n2 - Part B (Lazy Binomial Heap) \n3 - Part C (TwoThreeFourTree) \nx - Exit the program");
            Console.WriteLine("-----------------");


            Console.WriteLine("Enter the UID of the operation: ");

            string op = Console.ReadLine();

            if (op == "1")
            {
                PointQuadTree quadTree = new PointQuadTree(2); // 2-dimensional space
                int[] point1 = new int[2];
                int[] point2 = new int[2];

                while (true)
                {
                    Console.WriteLine("Enter values for Point 1 (in the format x,y):");
                    string[] point1Input = Console.ReadLine().Split(',');
                    if (point1Input.Length == 2 && int.TryParse(point1Input[0], out point1[0]) && int.TryParse(point1Input[1], out point1[1]))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You have to enter two integer values for x and y. Please try again.");
                    }
                }

                while (true)
                {
                    Console.WriteLine("Enter values for Point 2 (in the format x,y):");
                    string[] point2Input = Console.ReadLine().Split(',');
                    if (point2Input.Length == 2 && int.TryParse(point2Input[0], out point2[0]) && int.TryParse(point2Input[1], out point2[1]))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You have to enter two integer values for x and y. Please try again.");
                    }
                }

                // Use the quadTree to compare points
                int index = quadTree.ComparePoints(point1, point2);
                int index2 = quadTree.ComparePoints(point2, point1);

                // Print the results
                Console.WriteLine("\n// Quadtree Point Comparison Test\n");
                Console.WriteLine($"Point 1: ({point1[0]}, {point1[1]}), Point 2: ({point2[0]}, {point2[1]})");
                Console.WriteLine("Index of Point 1 relative to Point 2: " + index);
                Console.WriteLine("Index of Point 2 relative to Point 1: " + index2);
            }

            if (op == "2")
            {
                Console.WriteLine("\n-------------------------\n");
                // Testing LazyBinomialHeap
                LazyBinomialHeap<int> heap = new LazyBinomialHeap<int>();

                Console.WriteLine("Enter values to insert into LazyBinomialHeap (separated by comma): ");
                string input = Console.ReadLine();
                string[] separator = input.Split(',');

                foreach (string value in separator)
                {
                    if (int.TryParse(value.Trim(), out int intValue))
                    {
                        heap.Add(intValue);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid input '{value}'. Skipping...");
                    }
                }

                try
                {
                    // Print the heap
                    Console.WriteLine("Heap after adding elements:");
                    heap.Print();

                    // Get and remove the maximum element 
                    Console.WriteLine("\nFront item: " + heap.Front());
                    heap.Remove();
                // Gets the maximum element 
                Console.WriteLine("\nFront item: " + heap.Front());

                // Remove the maximum element
                heap.Remove();

                    // Print the heap after removal
                    Console.WriteLine("\nHeap after removing the maximum element:");
                    heap.Print();

                    // Get the new maximum element 
                    Console.WriteLine("\nNew maximum element: " + heap.Front());
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Operation failed: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

                // Gets the updated front element 
                Console.WriteLine("\nFront item : " + heap.Front());
                Console.WriteLine("\n-------------------------\n");
            }


            if (op == "3")
            {
                // Assuming TwoThreeFourTree and BSTforRBTree classes are already defined elsewhere
                BSTforRBTree<int> redBlackTree;
                Console.WriteLine("\nEnter values to insert into TwoThreeFourTree (separated by comma): ");
                string input = Console.ReadLine();
                string[] separator = input.Split(',');
                TwoThreeFourTree<int> tt4t = new TwoThreeFourTree<int>();
                foreach (string value in separator)
                {
                    if (int.TryParse(value, out int intValue))
                    {
                        tt4t.Insert(intValue);
                    }
                }

                // Print the tree
                Console.WriteLine("\nTwoThreeFourTree:");
                tt4t.PrintBTree();

                Console.WriteLine("\nRBTree:");
                redBlackTree = tt4t.Convert();
                redBlackTree.Print();

                while (true)
                {

                    if (tt4t.IsEmpty())
                    {
                        Console.WriteLine("The tree is empty. There are no elements to remove.");
                        break;
                    }
                    else
                    {



                        Console.WriteLine("\nDo you want to remove something from the tree? (y/n)");
                        string usercall = Console.ReadLine().ToLower();
                        if (usercall == "y")
                        {
                            Console.WriteLine("Please Enter the number from the tree you want to remove: ");

                            if (int.TryParse(Console.ReadLine(), out int numToDelete))
                            {
                                if (tt4t.Search(numToDelete))
                                {
                                    tt4t.Delete(numToDelete);
                                    Console.WriteLine("\nAfter deletion:");
                                    tt4t.PrintBTree();
                                    redBlackTree = tt4t.Convert();
                                    Console.WriteLine("RB tree conversion:");
                                    redBlackTree.Print();
                                }
                                else
                                {
                                    Console.WriteLine("The number is not in the tree.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid number.");
                            }
                        }
                        else if (usercall == "n")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                        }
                    }

                    Console.WriteLine("Please Enter the number from the tree you want to search for: ");
                    if (int.TryParse(Console.ReadLine(), out int numToSearch))
                    {
                        bool result = tt4t.Search(numToSearch);
                        if (result)
                        {
                            Console.WriteLine("The key was found within the tree.");
                        }
                        else
                        {
                            Console.WriteLine("The key was not found within this tree.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number.");
                    }
                }
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