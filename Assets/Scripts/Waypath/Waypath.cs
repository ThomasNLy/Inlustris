using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypath : MonoBehaviour
{
    public TNode head;
    public TNode tail;
    public int count;

    public Transform waypath;
    public TNode targetNode;
    public Transform target;
    
    protected virtual void Start()
    {
        foreach (Transform child in waypath)
        {

            TNode childNode = new TNode(child, null);

            if (count == 0)
            {
                count++;
                head = childNode;
                tail = head;
            }
            else if (count == 1)
            {
                count++;
                tail = childNode;
                head.Next = tail;
            }
            else
            {
                count++;
                TNode n = head;
                while (n.Next != null)
                {
                    n = n.Next;
                }
                n.Next = childNode;
                tail = childNode;
            }
        }
        targetNode = head;
        target = targetNode.Data;
    }
}

