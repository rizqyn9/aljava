using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aljava.MainMenu
{
    public class MainMenuController : Singleton<MainMenuController>
    {
        [Header("Properties")]
        public GameObject playBtn;
        public UI_Upgrade UI_Upgrade;
        public UI_MenuBook UI_MenuBook;
        [SerializeField] GameObject noClickArea;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] GameObject settingContainer;
        [SerializeField] GameObject aboutContainer;
        [SerializeField] GameObject upgradeContainer;
        [SerializeField] GameObject menuListContainer;

        private void Start()
        {
            noClickArea.SetActive(false);    
        }

        public void init()
        {
            print("MainMenu Initialize");
            UI_Upgrade.init();
            UI_MenuBook.init();
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
            defaultCommand(isSetting, settingContainer);
        }

        [SerializeField] bool isAbout = false;
        public void Btn_About()
        {
            isAbout = !isAbout;
            defaultCommand(isAbout, aboutContainer);
        }

        [SerializeField] bool isUpgrade = false;
        public void Btn_Upgrade()
        {
            isUpgrade = !isUpgrade;
            defaultCommand(isUpgrade, upgradeContainer);
        }

        [SerializeField] bool isMenuList = false;
        public void Btn_MenuList()
        {
            isMenuList = !isMenuList;
            defaultCommand(isMenuList, menuListContainer);
        }

        public void defaultCommand(bool _isActive, GameObject _go)
        {
            LeanTween.moveY(_go.GetComponent<RectTransform>(), _isActive ? 0 : settingOffsetY, 1f)
                .setOnComplete(() => {
                    if (!_isActive)
                    {
                        noClickArea.SetActive(false);
                        canvasGroup.alpha = 1;
                    }
                })
                .setOnStart(() => {
                    if (_isActive)
                    {
                        noClickArea.SetActive(true);
                        canvasGroup.alpha = 0;
                    }
                })
                .setEaseInBack();
        }

        public void Btn_PreventClick()
        {
            if (isSetting) Btn_Setting();
            if (isLevel) Btn_Level();
            if (isMenuList) Btn_MenuList();
            if (isUpgrade) Btn_Upgrade();
            if (isAbout) Btn_About();
        }
    }
}
