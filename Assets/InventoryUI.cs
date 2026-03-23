using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Inventory inventory;

    [Header("Item Info Panel")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Image infoIcon;
    [SerializeField] private TextMeshProUGUI infoName;
    [SerializeField] private TextMeshProUGUI infoDescription;
    [SerializeField] private TextMeshProUGUI infoQuantity;

    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();
    private int selectedIndex = -1;

    void Start()
    {
        inventory = Inventory.Instance;
        inventory.onInventoryChangedCallback += RefreshUI;

        BuildSlots();
        RefreshUI();
        HideInfo();
    }

    void BuildSlots()
    {
        // clear existing
        foreach (Transform child in slotsParent)
            Destroy(child.gameObject);

        slotUIs.Clear();

        // create one UI slot per inventory slot
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotsParent);
            InventorySlotUI ui = go.GetComponent<InventorySlotUI>();
            ui.Setup(inventory.slots[i], i, this);
            slotUIs.Add(ui);
        }
    }

    public void RefreshUI()
    {
        for (int i = 0; i < slotUIs.Count; i++)
            slotUIs[i].Refresh();
    }

    public void SelectSlot(int index)
    {
        // deselect previous
        if (selectedIndex >= 0 && selectedIndex < slotUIs.Count)
            slotUIs[selectedIndex].SetSelected(false);

        selectedIndex = index;
        slotUIs[index].SetSelected(true);

        // show info
        InventorySlot slot = inventory.slots[index];
        if (!slot.IsEmpty())
            ShowInfo(slot);
        else
            HideInfo();
    }

    void ShowInfo(InventorySlot slot)
    {
        if (infoPanel == null) return;
        infoPanel.SetActive(true);
        infoIcon.sprite = slot.item.icon;
        infoName.text = slot.item.itemName;
        infoDescription.text = slot.item.description;
        infoQuantity.text = "Quantity: " + slot.quantity;
    }

    void HideInfo()
    {
        if (infoPanel != null)
            infoPanel.SetActive(false);
    }
}