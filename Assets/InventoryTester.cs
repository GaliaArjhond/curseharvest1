using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] private ItemData[] testItems;
    [SerializeField] private int testQuantity = 5;

    void Update()
    {
        // press I to add a random test item
        if (UnityEngine.InputSystem.Keyboard.current.iKey.wasPressedThisFrame)
        {
            if (testItems.Length > 0)
            {
                ItemData item = testItems[Random.Range(0, testItems.Length)];
                Inventory.Instance.AddItem(item, testQuantity);
                Debug.Log("Added " + testQuantity + "x " + item.itemName);
            }
        }
    }
}