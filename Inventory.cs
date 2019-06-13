using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> inventory;
    public int keys;
    public GameManager gm;
    public void Initialize(GameManager g)
    {
        gm = g;
        keys = 0;
        inventory = new List<string>();
    }
    public void AddItem(string n)
    {
        inventory.Add(n);
    }
    public void AddKey()
    {
        keys += 1;
        if (keys > 0)
        {
            gm.keyOb.SetActive(true);
        }
    }
    public void ResetKeys()
    {
        keys = 0;
        gm.keyOb.SetActive(false);
    }
    public void DecrementKeys()
    {
        if (keys <= 0)
        {
            
            return;
        }
        keys -= 1;
        gm.keyOb.SetActive(false);

    }
    public bool hasKey()
    {
        return keys > 0;
    }

    public void RemoveItem(string name)
    {
        inventory.Remove(name);
    }

}
