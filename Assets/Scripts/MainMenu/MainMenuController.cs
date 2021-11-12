using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : Singleton<MainMenuController>
{
    [Header("Properties")]
    public GameObject playBtn;

    public void init()
    {
        print("MainMenu Initialize");
    }

    public void Btn_Play()
    {
        GameManager.LoadScene(SceneValid.LEVEL_STAGE, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
