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

        public int statePage, count;

        int setStatePage(bool isIncrement)
        {
            if (isIncrement) statePage += 1;
            if (!isIncrement) statePage -= 1;

            if (statePage >= count) statePage = 0;
            if (statePage < 0) statePage = count-1;

            return statePage;
        }

        public void init()
        {
            rendered.Clear();
            listMenuBases = ResourceManager.ListMenus;
            count = listMenuBases.Count;
            statePage = 0;
            render(listMenuBases[statePage], listMenuBases[setStatePage(true)]);
        }

        public Queue<MenuBookItem> rendered = new Queue<MenuBookItem>();
        void render(MenuBase _left = null, MenuBase _right = null)
        {
            foreach (MenuBookItem item in rendered) Destroy(item.gameObject);
            rendered.Clear();
            if (_left) renderItem(leftPos, _left);
            if (_right) renderItem(rightPos, _right);
        }

        void renderItem(Transform pos, MenuBase _menu)
        {
            MenuBookItem item = Instantiate(baseItem, pos).GetComponent<MenuBookItem>();
            item.init(_menu);
            rendered.Enqueue(item);
        }

        public void Btn_Left()
        {
            render(listMenuBases[setStatePage(false)], listMenuBases[setStatePage(false)]);
        }

        public void Btn_Right()
        {
            render(listMenuBases[setStatePage(true)], listMenuBases[setStatePage(true)]);

        }
    }
}
