using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "MenuBase", menuName = "ScriptableObject/MenuBase")]
public class MenuBase : ScriptableObject
{
    public string menuName;
    public MenuListName menuListName;
    public GameObject menuPrefab;
    public List<MachineIgrendient> Igrendients;
    public List<Sprite> stepRecipes;
    public string menuDesc;
    public Sprite menuSprite;
    public int pointInGame;
    public int price;
}


#if UNITY_EDITOR
[CustomEditor(typeof(MenuBase))]
public class MenuEditorScript : Editor
{
    public MenuBase menuBase;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        menuBase = (MenuBase)target;

        EditorGUILayout.LabelField("Validate");
        if(GUILayout.Button("Validate Menu"))
        {
            renameFile();
        }
    }

    public void renameFile()
    {
        string name = menuBase.menuListName.ToString();
        string assetPath = AssetDatabase.GetAssetPath(target.GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, name);
    }
}
#endif