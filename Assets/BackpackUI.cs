using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackpackUI : MonoBehaviour
{
    public GameObject slotPrefab;

    private Image[] icons;
    private TextMeshProUGUI[] qtys;
    private TextMeshProUGUI[] names;

    void Awake()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    void Start()
    {
        if (InventorySystem.Instance == null)
        {
            Debug.LogError("InventorySystem is NULL — add it to Player!");
            return;
        }

        int total = InventorySystem.Instance.totalSlots;
        icons = new Image[total];
        qtys = new TextMeshProUGUI[total];
        names = new TextMeshProUGUI[total];

        Debug.Log("Building " + total + " slots...");

        for (int i = 0; i < total; i++)
        {
            GameObject go = Instantiate(slotPrefab, transform);

            Transform iconT = go.transform.Find("Icon");
            Transform qtyT = go.transform.Find("QtyText");
            Transform nameT = go.transform.Find("ItemNameText");

            if (iconT == null) { Debug.LogError("Icon not found on prefab!"); return; }
            if (qtyT == null) { Debug.LogError("QtyText not found on prefab!"); return; }

            icons[i] = iconT.GetComponent<Image>();
            qtys[i] = qtyT.GetComponent<TextMeshProUGUI>();

            if (nameT != null)
                names[i] = nameT.GetComponent<TextMeshProUGUI>();
        }

        InventorySystem.Instance.onChanged += Refresh;
        Refresh();

        Debug.Log("Done! " + total + " slots created.");
    }

    void Refresh()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            ItemData item = InventorySystem.Instance.items[i];
            int qty = InventorySystem.Instance.quantity[i];

            if (item == null)
            {
                icons[i].sprite = null;
                icons[i].color = Color.clear;
                qtys[i].text = "";
                if (names[i] != null) names[i].text = "";
            }
            else
            {
                icons[i].sprite = item.icon;
                icons[i].color = Color.white;
                qtys[i].text = qty > 1 ? qty.ToString() : "";
                if (names[i] != null) names[i].text = item.itemName;
            }
        }
    }
}