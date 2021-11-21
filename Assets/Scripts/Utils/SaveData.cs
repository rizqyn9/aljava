using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Aljava.Game;
using Aljava;


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
    public List<UserMachineState> userEnvDatas;
    public List<LevelModel> listLevels;
}

[System.Serializable]
public struct UserMachineState
{
    public MachineIgrendient machineType;
    public int level;
}

public class SaveData : MonoBehaviour
{
    [SerializeField] string saveFilePath;
    public UserData userData;
    public LevelModel levelModel;

    public void init()
    {
        saveFilePath = Application.persistentDataPath + "/aljava.json";
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

    [ContextMenu("Reset Persistant")]
    public void resetPersistant()
    {
        saveFilePath = Application.persistentDataPath + "/aljava.json";
        if (File.Exists(saveFilePath))
        {
            Debug.Log("Founded");
            File.Delete(saveFilePath);
            Debug.Log("Deleted");
        }
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
