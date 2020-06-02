class LinkedList
{
  public class ListNode
  {
    public int val;
    public ListNode next;
    public ListNode(int x) => val = x;
  }

  public static int[] StringToIntegerArray(string input)
  {
    input = input.Trim().Substring(1, input.Length - 2);
    if (input.Length == 0)
    {
      return new int[0];
    }

    string[] parts = input.Split(",");
    int[] output = new int[parts.Length];
    for (int index = 0; index < parts.Length; index++)
    {
      string part = parts[index].Trim();
      output[index] = int.Parse(part);
    }
    return output;
  }

  public static ListNode StringToListNode(string input)
  {
    // Generate array from the input
    int[] nodeValues = StringToIntegerArray(input);

    // Now convert that list into linked list
    ListNode dummyRoot = new ListNode(0);
    ListNode ptr = dummyRoot;
    foreach (int item in nodeValues)
    {
      ptr.next = new ListNode(item);
      ptr = ptr.next;
    }
    return dummyRoot.next;
  }

  public static void PrettyPrintLinkedList(ListNode node)
  {
    while (node != null && node.next != null)
    {
      Console.Write(node.val + "->");
      node = node.next;
    }

    if (node != null)
    {
      Console.WriteLine(node.val);
    }
    else
    {
      Console.WriteLine("Empty LinkedList");
    }
  }
}

public class MainClass
{
  public static void Main()
  {
    // string line;
    // while ((line = Console.ReadLine()) != null)
    // {
    //   ListNode node = LinkedList.StringToListNode(line);
    //   LinkedList.PrettyPrintLinkedList(node);
    // }

    TextReader reader = Console.In;
    string line;
    while ((line = reader.ReadLine()) != null)
    {
      ListNode node = LinkedList.StringToListNode(line);
      LinkedList.PrettyPrintLinkedList(node);
    }
  }
}