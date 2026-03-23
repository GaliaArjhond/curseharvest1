using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("Settings")]
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 6;

    public List<InventorySlot> slots = new List<InventorySlot>();

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    void Awake()
    {
        Instance = this;

        // create empty slots
        for (int i = 0; i < rows * columns; i++)
            slots.Add(new InventorySlot());
    }

    // ── add item to inventory ──
    public bool AddItem(ItemData item, int quantity = 1)
    {
        // first try to stack on existing slot
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item && slot.quantity < item.maxStack)
            {
                int spaceLeft = item.maxStack - slot.quantity;
                int toAdd = Mathf.Min(quantity, spaceLeft);
                slot.quantity += toAdd;
                quantity -= toAdd;

                onInventoryChangedCallback?.Invoke();

                if (quantity <= 0) return true;
            }
        }

        // then find empty slot
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.item = item;
                slot.quantity = Mathf.Min(quantity, item.maxStack);
                quantity -= slot.quantity;

                onInventoryChangedCallback?.Invoke();

                if (quantity <= 0) return true;
            }
        }

        Debug.Log("Inventory full!");
        return false;
    }

    // ── remove item from inventory ──
    public bool RemoveItem(ItemData item, int quantity = 1)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                slot.quantity -= quantity;
                if (slot.quantity <= 0) slot.Clear();
                onInventoryChangedCallback?.Invoke();
                return true;
            }
        }
        return false;
    }

    // ── check if player has item ──
    public bool HasItem(ItemData item, int quantity = 1)
    {
        int total = 0;
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
                total += slot.quantity;
        }
        return total >= quantity;
    }
}