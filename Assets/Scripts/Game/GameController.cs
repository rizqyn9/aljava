using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{

    //private void OnEnable() => SceneManager.sceneLoaded += GameManager.Instance.handleSceneLoaded;
    //private void OnDisable() => SceneManager.sceneLoaded -= GameManager.Instance.handleSceneLoaded;
    public void init()
    {
        print("Game Initialize");
    }

    public void Btn_Home()
    {
        GameManager.LoadScene(SceneValid.MAIN_MENU);
    }
}
