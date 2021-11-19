using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
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
        }
    }
}
