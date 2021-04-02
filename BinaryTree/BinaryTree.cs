using System;

namespace BinaryTrees
{
  class BinaryTree
  {
    /*
      --Node--
      The binary tree is built using this nested node class.
      Each node stores one data element, and has left and right
      sub-tree pointer which may be null.
      The node is a "dumb" nested class -- we just use it for
      storage; it does not have any methods.
    */
    public class Node
    {
      public int data;
      public Node left;
      public Node right;

      public Node(int newData)
      {
        left = null;
        right = null;
        data = newData;
      }
    }

    // Root node pointer. Will be null for an empty tree.
    private Node root;

    /**
      Creates an empty binary tree -- a null root pointer.
    */
    public BinaryTree()
    {
      root = null;
    }

    /**
      Returns true if the given target is in the binary tree.
      Uses a recursive helper.
    */
    public bool Lookup(int data)
    {
      return Lookup(root, data);
    }

    static bool Lookup(Node node, int target)
    {
      // 1. Base case == empty tree
      // in that case, the target is not found so return false
      if (node == null)
      {
        return false;
      }

      // 2. see if found here
      if (target == node.data)
      {
        return true;
      }
      // 3. otherwise recur down the correct subtree
      else if (target < node.data)
      {
        return Lookup(node.left, target);
      }
      else return Lookup(node.right, target);
    }

    /**
      Inserts the given data into the binary tree.
      Uses a recursive helper.
    */
    public void Insert(int data)
    {
      root = Insert(root, data);
    }

    /**
      Recursive insert -- given a node pointer, recur down and
      insert the given data into the tree. Returns the new
      node pointer (the standard way to communicate
      a changed pointer back to the caller).
    */
    private Node Insert(Node node, int data)
    {
      if (node == null)
      {
        node = new Node(data);
      }
      else
      {
        if (data <= node.data)
        {
          node.left = Insert(node.left, data);
        }
        else
        {
          node.right = Insert(node.right, data);
        }
      }

      return node; // in any case, return the new pointer to the callr
    }

    /**
      Returns the number of nodes in the tree.
      Uses a recursive helper that recurs
      down the tree and counts the nodes.
    */
    public int Size()
    {
      return Size(root);
    }

    private int Size(Node node)
    {
      if (node == null) return 0;
      else
      {
        return Size(node.left) + 1 + Size(node.right);
      }
    }

    /**
      Returns the max root-to-leaf depth of the tree.
      Uses a recursive helper that recurs down to find
      the max depth.
    */
    public int MaxDepth()
    {
      return MaxDepth(root);
    }

    private int MaxDepth(Node node)
    {
      if (node == null)
      {
        return 0;
      }
      else
      {
        int lDepth = MaxDepth(node.left);
        int rDepth = MaxDepth(node.right);

        // use the larger + 1
        return Math.Max(lDepth, rDepth) + 1;
      }
    }

    /**
      Returns the min value in a non-empty binary search tree.
      Uses a helper method that iterates to the left to find
      the min value.
    */
    public int MinValue()
    {
      return MinValue(root);
    }

    /**
     Finds the min value in a non-empty binary search tree.
    */
    private int MinValue(Node node)
    {
      Node current = node;
      while (current.left != null)
      {
        current = current.left;
      }

      return current.data;
    }

    /**
      Returns the max value in a non-empty binary search tree.
      Uses a helper method that iterates to the right to find
      the max value.
    */
    public int MaxValue()
    {
      return MaxValue(root);
    }

    /**
      Finds the max value in a non-empty binary search tree.
    */
    private int MaxValue(Node node)
    {
      Node current = node;
      while (current.right != null)
      {
        current = current.right;
      }

      return current.data;
    }

    public void PrettyPrintTree()
    {
      PrettyPrintTree(root, "", true);
    }

    public void PrettyPrintTree(Node node, string prefix, bool isLeft)
    {
      if (node == null)
      {
        Console.WriteLine("Empty tree");
        return;
      }

      if (node.right != null)
      {
        PrettyPrintTree(node.right, prefix + (isLeft ? "│   " : "    "), false);
      }

      Console.WriteLine(prefix + (isLeft ? "└── " : "┌── ") + node.data);

      if (node.left != null)
      {
        PrettyPrintTree(node.left, prefix + (isLeft ? "    " : "│   "), true);
      }
    }

    /**
      Prints the node values in the "inorder" order.
      Uses a recursive helper to do the traversal.
    */
    public void PrintInorder()
    {
      PrintInorder(root);
      Console.WriteLine();
    }

    private void PrintInorder(Node node)
    {
      if (node == null) return;

      // left, node itself, right
      PrintInorder(node.left);
      Console.Write(node.data + "  ");
      PrintInorder(node.right);
    }


    /**
      Prints the node values in the "postorder" order.
      Uses a recursive helper to do the traversal.
    */
    public void PrintPostorder()
    {
      PrintPostorder(root);
      Console.WriteLine();
    }

    public void PrintPostorder(Node node)
    {
      if (node == null) return;

      // first recur on both subtrees
      PrintPostorder(node.left);
      PrintPostorder(node.right);

      // then deal with the node
      Console.Write(node.data + "  ");
    }

    /**
      Prints the node values in the "preorder" order.
      Uses a recursive helper to do the traversal.
    */
    public void PrintPreOrder()
    {
      PrintPreOrder(root);
      Console.WriteLine();
    }

    public void PrintPreOrder(Node node)
    {
      if (node == null) return;

      // first deal with the node
      Console.Write(node.data + "  ");

      // then recur on both subtrees
      PrintPostorder(node.left);
      PrintPostorder(node.right);
    }

    /**
      Given a tree and a sum, returns true if there is a path from the root
      down to a leaf, such that adding up all the values along the path
      equals the given sum.
      Strategy: subtract the node value from the sum when recurring down,
      and check to see if the sum is 0 when you run out of tree.
    */
    public bool HasPathSum(int sum)
    {
      return HasPathSum(root, sum);
    }

    bool HasPathSum(Node node, int sum)
    {
      // return true if we run out of tree and sum==0
      if (node == null)
      {
        return sum == 0;
      }
      else
      {
        // otherwise check both subtrees
        int subSum = sum - node.data;
        return HasPathSum(node.left, subSum) || HasPathSum(node.right, subSum);
      }
    }

    /**
      Given a binary tree, prints out all of its root-to-leaf
      paths, one per line. Uses a recursive helper to do the work.
    */
    public void PrintPaths()
    {
      int[] path = new int[1000];
      PrintPaths(root, path, 0);
    }

    /**
      Recursive PrintPaths helper -- given a node, and an array containing
      the path from the root node up to but not including this node,
      prints out all the root-leaf paths.
    */
    private void PrintPaths(Node node, int[] path, int pathLen)
    {
      if (node == null) return;

      // append this node to the path array
      path[pathLen] = node.data;
      pathLen++;

      // it's a leaf, so print the path that led to here
      if (node.left == null && node.right == null)
      {
        PrintArray(path, pathLen);
      }
      else
      {
        // otherwise try both subtrees
        PrintPaths(node.left, path, pathLen);
        PrintPaths(node.right, path, pathLen);
      }
    }

    /**
      Utility that prints ints from an array on one line.
    */
    private void PrintArray(int[] ints, int len)
    {
      int i;
      for (i = 0; i < len; i++)
      {
        Console.Write(ints[i] + " ");
      }
      Console.WriteLine();
    }

    /**
      Changes the tree into its mirror image.

      So the tree...
            4
            / \
          2   5
          / \
        1   3

      is changed to...
            4
            / \
          5   2
              / \
            3   1

      Uses a recursive helper that recurs over the tree,
      swapping the left/right pointers.
    */
    public void Mirror()
    {
      Mirror(root);
    }

    private void Mirror(Node node)
    {
      if (node != null)
      {
        // do the sub-trees
        Mirror(node.left);
        Mirror(node.right);

        // swap the left/right pointers
        Node temp = node.left;
        node.left = node.right;
        node.right = temp;
      }
    }

    /**
      Changes the tree by inserting a duplicate node
      on each nodes's .left.
      
      
      So the tree...
          2
        / \
        1   3

      Is changed to...
            2
            / \
          2   3
          /   /
        1   3
        /
      1

      Uses a recursive helper to recur over the tree
      and insert the duplicates.
    */
    public void DoubleTree()
    {
      DoubleTree(root);
    }

    private void DoubleTree(Node node)
    {
      Node oldLeft;

      if (node == null) return;

      // do the subtrees
      DoubleTree(node.left);
      DoubleTree(node.right);

      // duplicate this node to its left
      oldLeft = node.left;
      node.left = new Node(node.data);
      node.left.left = oldLeft;
    }

    /*
      Compares the receiver to another tree to
      see if they are structurally identical.
    */
    public bool SameTree(BinaryTree other)
    {
      return SameTree(root, other.root);
    }

    /**
      Recursive helper -- recurs down two trees in parallel,
      checking to see if they are identical.
    */
    bool SameTree(Node a, Node b)
    {
      // 1. both empty -> true
      if (a == null && b == null) return true;

      // 2. both non-empty -> compare them
      else if (a != null && b != null)
      {
        return (
          a.data == b.data &&
          SameTree(a.left, b.left) &&
          SameTree(a.right, b.right)
        );
      }
      // 3. one empty, one not -> false
      else return false;
    }

    /**
      For the key values 1...numKeys, how many structurally unique
      binary search trees are possible that store those keys?
      Strategy: consider that each value could be the root.
      Recursively find the size of the left and right subtrees.
    */
    public static int CountTrees(int numKeys)
    {
      if (numKeys <= 1)
      {
        return 1;
      }
      else
      {
        // there will be one value at the root, with whatever remains
        // on the left and right each forming their own subtrees.
        // Iterate through all the values that could be the root...
        int sum = 0;
        int left, right, root;

        for (root = 1; root <= numKeys; root++)
        {
          left = CountTrees(root - 1);
          right = CountTrees(numKeys - root);

          // number of possible trees with this root == left*right
          sum += left * right;
        }

        return sum;
      }
    }

    /**
      Tests if a tree meets the conditions to be a
      binary search tree (BST).
    */
    public bool IsBST()
    {
      return IsBST(root);
    }

    /**
      Recursive helper -- checks if a tree is a BST
      using MinValue() and maxValue() (not efficient).
    */
    private bool IsBST(Node node)
    {
      if (node == null) return true;

      // do the subtrees contain values that do not
      // agree with the node?
      if (node.left != null && MaxValue(node.left) > node.data) return false;
      if (node.right != null && MinValue(node.right) <= node.data) return false;

      // check that the subtrees themselves are ok
      return IsBST(node.left) && IsBST(node.right);
    }

    /**
      Tests if a tree meets the conditions to be a
      binary search tree (BST). Uses the efficient
      recursive helper.
    */
    public bool IsBST2()
    {
      return IsBST2(root, int.MinValue, int.MaxValue);
    }

    /**
        Efficient BST helper -- Given a node, and min and max values,
        recurs down the tree to verify that it is a BST, and that all
        its nodes are within the min..max range. Works in O(n) time --
        visits each node only once.
    */
    private bool IsBST2(Node node, int min, int max)
    {
      if (node == null)
      {
        return true;
      }
      else
      {
        // left should be in range  min...node.data
        bool leftOk = IsBST2(node.left, min, node.data);

        // if the left is not ok, bail out
        if (!leftOk) return false;

        // right should be in range node.data+1..max
        bool rightOk = IsBST2(node.right, node.data + 1, max);

        return rightOk;
      }
    }
  }
}
