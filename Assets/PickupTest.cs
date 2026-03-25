using UnityEngine;
using UnityEngine.InputSystem;

public class PickupTest : MonoBehaviour
{
    public ItemData[] testItems;

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame && testItems.Length > 0)
        {
            ItemData item = testItems[Random.Range(0, testItems.Length)];
            InventorySystem.Instance.AddItem(item);
            Debug.Log("Added: " + item.itemName);
        }
    }
}