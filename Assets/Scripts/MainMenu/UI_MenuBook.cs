using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.MainMenu
{
    public class UI_MenuBook : MonoBehaviour
    {
        [Header("Propeties")]
        public GameObject baseItem;

        [Header("Debug")]
        public List<MenuBase> listMenuBases;
        public List<MenuBookItem> menuItems = new List<MenuBookItem>();

        public void init()
        {
            listMenuBases = ResourceManager.ListMenus;
        }

    }
}
