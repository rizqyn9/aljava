using System;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneValid
{
    public const string MAIN_MENU = "MainMenu";
    public const string LEVEL_STAGE = "LevelStage";
    public const string GAME = "Game";
}

public class GameManager : Singleton<GameManager>
{
    [Header("Properties")]
    public SaveData saveData;
    public GameObject resourceManagerPrefab;

    [Header("Debug")]
    public UserData userData;

    private void Start()
    {
        saveData.init();
        userData = saveData.userData;

        if (!FindObjectOfType<ResourceManager>())
        {
            Instantiate(resourceManagerPrefab);
        }
    }

    #region Scene Handler
    private void OnEnable() => SceneManager.sceneLoaded += handleSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= handleSceneLoaded;

    [SerializeField] string sceneNow;
    [SerializeField] bool isInit;
    public void handleSceneLoaded(Scene _scene, LoadSceneMode arg1)
    {
        sceneNow = _scene.name;
        if(sceneNow == SceneValid.GAME)
        {
            GameController.Instance.init();
        } else if (sceneNow == SceneValid.MAIN_MENU)
        {
            MainMenuController.Instance.init();
        } else if(sceneNow == SceneValid.LEVEL_STAGE)
        {
            LevelStageController.Instance.init();
        }
    }

    public static void LoadScene(string _target, LoadSceneMode _loadSceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(_target, _loadSceneMode);
    }

    public static void UnLoadScene(string _target)
    {
        SceneManager.UnloadSceneAsync(_target);
    }
    #endregion
}
