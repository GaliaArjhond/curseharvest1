using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private Image slotBackground;

    [Header("Colors")]
    [SerializeField] private Color normalColor = new Color(0.16f, 0.16f, 0.16f, 0.86f);
    [SerializeField] private Color selectedColor = new Color(0.9f, 0.7f, 0.1f, 1f);

    private InventorySlot slot;
    private int slotIndex;
    private InventoryUI inventoryUI;

    public void Setup(InventorySlot slot, int index, InventoryUI ui)
    {
        this.slot = slot;
        this.slotIndex = index;
        this.inventoryUI = ui;

        Refresh();

        // wire click
        GetComponent<Button>().onClick.AddListener(() => inventoryUI.SelectSlot(slotIndex));
    }

    public void Refresh()
    {
        if (slot == null || slot.IsEmpty())
        {
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
            quantityText.text = "";
            itemNameText.text = "";
        }
        else
        {
            itemIcon.sprite = slot.item.icon;
            itemIcon.color = Color.white;
            quantityText.text = slot.quantity > 1 ? "x" + slot.quantity : "";
            itemNameText.text = slot.item.itemName;
        }
    }

    public void SetSelected(bool selected)
    {
        slotBackground.color = selected ? selectedColor : normalColor;
    }
}