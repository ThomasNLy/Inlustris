using UnityEngine;

public class TNode
{
    Transform data;
    TNode next;
    public TNode(Transform data, TNode next)
    {
        this.data = data;
        this.next = next;
    }
    public Transform Data
    {
        get { return data; }
        set { data = value; }
    }

    public TNode Next
    {
        get { return next; }
        set { next = value; }
    }
}
