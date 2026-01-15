using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public GameObject itemModel;
    public int maxReserves = 1;
}

public enum ItemType
{
    Gun,
    Ammo,
    Key,
    Coagulant,
    Corpse
}