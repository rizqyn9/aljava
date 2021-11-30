using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Aljava.Game
{
    public class UIBaseRecipe : MonoBehaviour
    {
        [Header("Properties")]
        public TMP_Text menuTitle;
        public List<Image> steps;
        public Image menuImage;

        [Header("Debug")]
        [SerializeField] MenuBase menuBase;
        public void init(MenuBase _menuBase)
        {
            menuBase = _menuBase;

            menuImage.sprite = _menuBase.menuSprite;
            menuTitle.text = menuBase.menuName;
            for(int i = 0; i < steps.Count; i++)
            {
                
                if (i >= _menuBase.stepRecipes.Count)
                {
                    steps[i].enabled = false;
                } else
                    steps[i].sprite = menuBase.stepRecipes[i];
            }
        }
    }
}
