using UnityEngine;
using System.Collections.Generic;
using Aljava.Game;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Level Base", menuName = "ScriptableObject/LevelBase")]
public class LevelBase : ScriptableObject
{
    public bool isLevelTest = false;

    public bool isTutorialLevel = false;
    [ShowIf("isTutorialLevel")]
    public GameObject tutorialPrefabs;

    public int level;


    [TabGroup("Menu Classification")]
    [HideLabel]
    public List<MenuClassification> MenuClassifications;
    [TabGroup("Menu Type Unlock")]
    [HideLabel]
    public List<MenuListName> MenuTypeUnlock;
    [TabGroup("Buyer Type Unlock")]
    [HideLabel]
    [ListDrawerSettings]
    public List<enumBuyerType> BuyerTypeUnlock;
    public float delayPerCustomer = 10;
    public int healthTotal = 3;

    [EnumToggleButtons]
    public GameMode gameMode = GameMode.TIME;
    [Header("GameMode")]
    public int gameDuration;
    public int minPoint;
    public int minOrder;
    public int minBuyer;


#if UNITY_EDITOR
    [DisplayAsString(false)][PropertyOrder(5)][HideLabel]
    public string message = "Validate Message";

    [Button]
    public void validateData()
    {
        message = "";
        try
        {
            if(!isLevelTest) renameFile();
            if (!tutorialPrefabs.GetComponent<Tutorial>()) throw new System.Exception("Tutorial Object empyt");
            else message += "\nTutorial level validated";

        } catch (System.Exception e)
        {
            message += $"Error : {e}";
        }
    }

    void renameFile()
    {
        string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this.GetInstanceID());
        UnityEditor.AssetDatabase.RenameAsset(assetPath, $"Level_{level}");
        message += "Success Change File Name";
    }
    #endif
}