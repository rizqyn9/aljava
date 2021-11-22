using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.MainMenu
{
    public class UI_MenuBook : MonoBehaviour
    {
        [Header("Propeties")]
        public GameObject baseItem;
        public Transform leftPos, rightPos;

        [Header("Debug")]
        public List<MenuBase> listMenuBases;
        public List<MenuBookItem> menuItems = new List<MenuBookItem>();

        public void init()
        {
            listMenuBases = ResourceManager.ListMenus;
            render(listMenuBases[0], listMenuBases[1]);
        }

        public List<MenuBookItem> rendered;
        void render(MenuBase _left = null, MenuBase _right = null)
        {
            if (_left) renderItem(leftPos, _left);
            if (_right) renderItem(rightPos, _right);
        }

        void renderItem(Transform pos, MenuBase _menu)
        {
            MenuBookItem item = Instantiate(baseItem, pos).GetComponent<MenuBookItem>();
            item.init(_menu);
            rendered.Add(item);
        }

        public void Btn_Left()
        {

        }

        public void Btn_Right()
        {

        }
    }
}
