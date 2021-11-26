using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aljava.MainMenu
{
    public class MenuBookItem : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject baseBarItem;
        public Transform barContainer;
        public Image menuImage;
        public TMPro.TMP_Text menuName;

        [Header("Debug")]
        public List<MenuBookBar> listBar;
        public MenuBase menuBase;

        public void init(MenuBase _menuBase)
        {
            menuBase = _menuBase;
            menuImage.sprite = _menuBase.menuSprite;
            menuName.text = menuBase.menuName;
        }
    }
}
