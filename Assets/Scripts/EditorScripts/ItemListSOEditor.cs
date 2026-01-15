using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(ItemListSO))]
public class ItemListSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ItemListSO itemListSO = (ItemListSO)target;

        GUILayout.Space(10);
        if (GUILayout.Button("모든 아이템 SO 자동 수집", GUILayout.Height(30)))
        {
            CollectAllItems(itemListSO);
        }
    }

    private void CollectAllItems(ItemListSO itemListSO)
    {
        string[] guids = AssetDatabase.FindAssets("t:ItemSO");
        ItemSO[] items = guids
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Select(path => AssetDatabase.LoadAssetAtPath<ItemSO>(path))
            .Where(item => item != null)
            .ToArray();

        Undo.RecordObject(itemListSO, "Collect All Items");
        itemListSO.itemList = items;
        EditorUtility.SetDirty(itemListSO);

        Debug.Log($"총 {items.Length}개의 아이템이 수집되었습니다.");
    }
}
