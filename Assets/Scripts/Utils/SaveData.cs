using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Aljava.Game;
using Aljava;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct LevelModel
{
    public LevelState levelState;
    public int level;
    public int stars;
    public int playerInstance;
    public int point;
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

public enum LevelState
{
    COMMON,
    LOCK,
    OPEN,
    LOSE,
    WIN
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
        print("saving data");
        int _target = userData.listLevels.FindIndex(val => val.level == _levelModel.level);
        userData.listLevels[_target] = _levelModel;  // Updating current level
        if (_levelModel.levelState == LevelState.WIN)
        {
            // Set open on next level
            openNextLevel(_levelModel.level + 1);
        }
        saveIntoJson();
    }

    private void openNextLevel(int _targetLevel)
    {
        int index = userData.listLevels.FindIndex(val => val.level == _targetLevel);
        if(index >= 0)
        {
            print("Update");
            LevelModel next = userData.listLevels[index];
            if(next.levelState == LevelState.LOCK)
            {
                next.levelState = LevelState.OPEN;
                userData.listLevels[index] = next;
            }
        }
        else
        {
            print("Create new temp");
            userData.listLevels.Add(new LevelModel
            {
                level = _targetLevel,
                levelState = LevelState.OPEN
            });
        }
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
                    levelState = LevelState.OPEN,
                    level = 1,
                },
            }
        };
    }

    #if UNITY_EDITOR
    [ContextMenu("Reset Persistant")]
    [MenuItem("Aljava/reset persistant")]
    public static void resetPersistant()
    {
        string saveFilePath = Application.persistentDataPath + "/aljava.json";
        if (File.Exists(saveFilePath))
        {
            Debug.Log("Founded");
            File.Delete(saveFilePath);
            Debug.Log("Deleted");
        } else
            Debug.Log("Empty");
    }
    #endif


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
                    levelState = LevelState.OPEN,
                    level = 1,
                }
            }
        };
    }
}
