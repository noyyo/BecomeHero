using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> _items = new List<Item>();

    private void Awake()
    {

    }

    public void AddItem(Item item)
    {
        _items.Add(item);
    }
    public void RemoveItem(Item item)
    {
        _items.Remove(item);
    }
    public List<Item> GetItemList()
    {
        return _items;
    }
}
