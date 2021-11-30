using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct BarMenu
{
    [Tooltip("1: Bitterness 2: Sweetness")]
    public int id;
    public int total;
}
namespace Aljava.MainMenu
{
    public class MenuBookItem : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject baseBarItem;
        public Transform barContainer1, barContainer2;
        public Image menuImage;
        public TMPro.TMP_Text menuName;
        public float yOffset;

        [Header("Debug")]
        public List<MenuBookBar> listBar;
        public MenuBase menuBase;

        public void init(MenuBase _menuBase)
        {
            menuBase = _menuBase;
            menuImage.sprite = _menuBase.menuSprite;
            menuName.text = menuBase.menuName;

            for(int i = 0; i< menuBase.barMenus.Count; i++)
            {
                BarMenu barData = menuBase.barMenus[i];
                MenuBookBar bar = Instantiate(baseBarItem, i == 0 ? barContainer1 : barContainer2).GetComponent<MenuBookBar>();
                bar.init(barData.id, barData.total);
                listBar.Add(bar);
            }
        }

        public void Btn_Cart()
        {
            MainMenuController.Instance.GrabFoodLink();
        }
    }
}
