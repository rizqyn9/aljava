using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneValid
{
    public const string MAIN_MENU = "MainMenu";
    public const string GAME = "Game";
}

public class GameManager : Singleton<GameManager>
{
    //[Header("Scene")]

    //[Header("Debug")]

    private void OnEnable()
    {
        isDDOL = true;
        SceneManager.sceneLoaded += handleSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= handleSceneLoaded;
    }

    [SerializeField] string sceneNow;
    [SerializeField] bool isInit;
    public void handleSceneLoaded(Scene _scene, LoadSceneMode arg1)
    {

        Scene getScene = SceneManager.GetActiveScene();
        if(getScene.name == SceneValid.GAME)
        {
            GameController.Instance.init();
        } else if (getScene.name == SceneValid.MAIN_MENU)
        {
            MainMenuController.Instance.init();
        }
    }

    public static void LoadScene(string _target)
    {
        SceneManager.LoadScene(_target);
    }
}
