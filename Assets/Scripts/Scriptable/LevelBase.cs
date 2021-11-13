using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Level Base", menuName = "ScriptableObject/LevelBase")]
public class LevelBase : ScriptableObject
{
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
            levelBase.name = $"Level{levelBase.level}";
        }
    }
}
#endif