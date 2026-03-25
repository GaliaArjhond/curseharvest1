using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public int totalSlots = 20;
    public List<ItemData> items = new List<ItemData>();
    public List<int> quantity = new List<int>();

    public delegate void OnChanged();
    public OnChanged onChanged;

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < totalSlots; i++)
        {
            items.Add(null);
            quantity.Add(0);
        }
    }

    public void AddItem(ItemData item, int qty = 1)
    {
        // try stack
        for (int i = 0; i < totalSlots; i++)
        {
            if (items[i] == item && quantity[i] < item.maxStack)
            {
                quantity[i] += qty;
                onChanged?.Invoke();
                return;
            }
        }
        // empty slot
        for (int i = 0; i < totalSlots; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                quantity[i] = qty;
                onChanged?.Invoke();
                return;
            }
        }
        Debug.Log("Inventory full!");
    }
}