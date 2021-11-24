using UnityEngine;
using System.Collections.Generic;
using Aljava.Game;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Level Base", menuName = "ScriptableObject/LevelBase")]
public class LevelBase : ScriptableObject
{
    public bool isTutorialLevel = false;
    public Tutorial tutorialScript;
    public bool isLevelTest = false;
    public int level;
    public GameMode gameMode = GameMode.TIME;
    public List<MenuClassification> MenuClassifications;
    public List<MenuListName> MenuTypeUnlock;
    public List<enumBuyerType> BuyerTypeUnlock;
    public float delayPerCustomer = 10;
    public int healthTotal = 3;

    [Header("Game Mode")]
    public int gameDuration;
    public int minPoint;
    public int minOrder;
    public int minBuyer;
}

#if UNITY_EDITOR
[CustomEditor(typeof(LevelBase))]
public class LevelEditorScript : Editor
{
    public LevelBase levelBase;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        levelBase = (LevelBase) target;

        EditorGUILayout.LabelField("Validate");
        if (GUILayout.Button("Validate "))
        {
            if (!levelBase.isLevelTest)
                renameFile();
        }
    }

    public void renameFile()
    {
        string name = "Level_"+levelBase.level.ToString();
        string assetPath = AssetDatabase.GetAssetPath(target.GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, name);
    }
}
#endif