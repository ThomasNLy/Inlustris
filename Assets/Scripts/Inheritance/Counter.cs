using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour {
    public int amount;

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    public virtual void AddCount() {
        amount++;
        //GetComponentInChildren<Text>().text = " x " + amount;
    }

    public virtual void SubtractCount()
    {
        if (amount > 0)
            amount--;
        //GetComponentInChildren<Text>().text = " x " + amount;
    }
}
