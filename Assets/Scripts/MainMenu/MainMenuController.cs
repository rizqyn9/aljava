using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuController : Singleton<MainMenuController>
{
    [Header("Properties")]
    public GameObject playBtn;
    [SerializeField] GameObject noClickArea;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GameObject settingContainer;


    //[Header("Debug")]


    private void Start()
    {
        noClickArea.SetActive(false);    
    }

    public void init()
    {
        print("MainMenu Initialize");
    }

    public void handleOpenLevel(bool levelIsOpen)
    {
        LeanTween.alphaCanvas(canvasGroup, levelIsOpen ? 0 : 1, .5f);
    }

    [SerializeField] bool isLevel = false;
    public void Btn_Level()
    {
        isLevel = !isLevel;
        handleOpenLevel(isLevel);
        noClickArea.SetActive(isLevel);
        if(isLevel)
            GameManager.LoadScene(SceneValid.LEVEL_STAGE, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        else
            GameManager.UnLoadScene(SceneValid.LEVEL_STAGE);
    }

    [SerializeField] bool isSetting = false;
    [SerializeField] float settingOffsetY;
    public void Btn_Setting()
    {
        isSetting = !isSetting;
        noClickArea.SetActive(isSetting);
        LeanTween.moveY(settingContainer.GetComponent<RectTransform>(), isSetting ? 0 : settingOffsetY, 1f).setEaseInBack();
    }

    public void Btn_PreventClick()
    {
        if (isSetting) Btn_Setting();
        if (isLevel) Btn_Level();
    }
}
