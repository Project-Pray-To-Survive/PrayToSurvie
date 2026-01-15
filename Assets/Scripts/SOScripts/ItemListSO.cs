using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "Game/ItemList")]
public class ItemListSO : ScriptableObject
{
    public ItemSO[] itemList;
}