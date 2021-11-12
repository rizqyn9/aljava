using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : Singleton<MainMenuController>
{
    [Header("Properties")]
    public GameObject playBtn;

    //private void OnEnable() => SceneManager.sceneLoaded += GameManager.Instance.handleSceneLoaded;
    //private void OnDisable() => SceneManager.sceneLoaded -= GameManager.Instance.handleSceneLoaded;

    public void init()
    {
        print("MainMenu Initialize");
    }

    public void Btn_Play()
    {
        GameManager.LoadScene(SceneValid.GAME);
    }
}
