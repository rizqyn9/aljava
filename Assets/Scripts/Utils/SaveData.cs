using UnityEngine;
using Game;
using System.IO;
using System.Collections.Generic;
using System;

[System.Serializable]
public struct LevelModel
{
    public bool isOpen;
    public int level;
    public int stars;
    public int playerInstance;
    public int point;
    public bool isWin;
}

[System.Serializable]
public struct UserData
{
    public string userName;
    public int point;
    public UserMachineState levelUserDatas;
    public List<LevelModel> listLevels;
}

[System.Serializable]
public struct UserMachineState
{
    public MachineIgrendient machineIgrendient;
    public int level;
}

public class SaveData : MonoBehaviour
{
    [SerializeField] string saveFilePath;
    public UserData userData;
    public LevelModel levelModel;

    public void init()
    {
        saveFilePath = Application.dataPath + "/Persistant/aljava.json";
        if (Dev.Instance.useCustomUserData)
            userData = Dev.Instance.customUserData;
        else
        {
            if (File.Exists(saveFilePath))
                userData = JsonUtility.FromJson<UserData>(File.ReadAllText(saveFilePath));
            else
            {
                userData = createDefaultData();
                saveIntoJson();
            }   
        }
    }

    //public LevelModel grabData()
    //{
    //    //return new LevelModel
    //    //{
    //    //    level = MainController.Instance.levelBase.level,
    //    //    playerInstance = MainController.RulesController.buyerInstanceTotal,
    //    //    isWin = MainController.RulesController.isWin
    //    //};
    //}

    public void saveIntoJson()
    {
        try
        {
            File.WriteAllText(saveFilePath, JsonUtility.ToJson(userData));
            print($"<color=green> Game saved </color>");
        }
        catch
        {
            print($"<color=red> fail when saving game data </color>");
        }
    }

    public void updateLevel(LevelModel _levelModel)
    {
        LevelModel _target = userData.listLevels.Find(val => val.level == _levelModel.level);
        _target = _levelModel;
        if (_levelModel.isWin)
        {
            LevelModel _next = userData.listLevels.Find(val => val.level == _levelModel.level);
            _next.isOpen = true;
        }
        saveIntoJson();
    }

    public void createTemp()
    {
        userData = new UserData
        {
            userName = "Temp",
            listLevels = new List<LevelModel>()
            {
                new LevelModel
                {
                    isOpen = true,
                    level = 1,
                },
            }
        };
    }


    /// <summary>
    /// Default data
    /// </summary>
    /// <returns></returns>
    public UserData createDefaultData()
    {
        return new UserData
        {
            userName = "Default",
            listLevels = new List<LevelModel>()
            {
                new LevelModel {
                    isOpen = true,
                    level = 1,
                },
                new LevelModel {
                    isOpen = true,
                    level = 2
                }
            }
        };
    }
}
