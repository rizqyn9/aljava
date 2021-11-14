using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "BuyerBase", menuName = "ScriptableObject/BuyerBase")]
public class BuyerBase : ScriptableObject
{
    public enumBuyerType buyerType;
    public string buyerName;
    public GameObject buyerPrefab;
    public float patienceDuration = 10f;
}


#if UNITY_EDITOR
[CustomEditor(typeof(BuyerBase))]
public class BuyerEditorScript : Editor
{
    public BuyerBase buyerBase;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        buyerBase = (BuyerBase)target;

        EditorGUILayout.LabelField("Validate");
        if (GUILayout.Button("Validate Menu"))
        {
            renameFile();
        }
    }

    public void renameFile()
    {
        string name = buyerBase.buyerType.ToString();
        string assetPath = AssetDatabase.GetAssetPath(target.GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, name);
    }
}
#endif