using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aljava.Game
{
    public class UI_Recipe : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject baseItemRecipe;
        public Transform itemPlace;
        public float offsetX;
        public float startX;
        public Button btnRecipe;
        public AnimationCurve animationCurve;

        [Header("Debug")]
        public List<UIBaseRecipe> recipesItem;

        [SerializeField] bool isActive;
        public void Btn_Recipe()
        {
            LeanTween.moveX(gameObject.GetComponent<RectTransform>(), isActive ? startX : offsetX, .7f)
                .setOnStart(() => {
                    btnRecipe.interactable = false;
                    if(isActive) UIGameManager.Instance.noClickSetActive(isActive);
                    })
                .setOnComplete(() => {
                    btnRecipe.interactable = true;
                    if(!isActive) UIGameManager.Instance.noClickSetActive(isActive);
                })
                .setEase(animationCurve);
            isActive = !isActive;
        }

        /// <summary>
        /// Render Menu list
        /// Do foreach 
        /// </summary>
        public void init(List<MenuBase> _menuBases)
        {
            _menuBases.ForEach(val =>
            {
                UIBaseRecipe go = Instantiate(baseItemRecipe, itemPlace).GetComponent<UIBaseRecipe>();
                recipesItem.Add(go);
                go.init(val);
            });
        }
    }
}
