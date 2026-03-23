using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon;
    public string description = "";
    public int maxStack = 99;
    public ItemType itemType;

    public enum ItemType
    {
        Resource,
        Food,
        Tool,
        Seed,
        Misc
    }
}