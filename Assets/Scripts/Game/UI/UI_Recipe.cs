using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UI_Recipe : MonoBehaviour
    {
        [Header("Properties")]
        public float offsetX;
        public float startX;
        public Button btnRecipe;
        public AnimationCurve animationCurve; 

        [SerializeField] bool isActive;
        public void Btn_Recipe()
        {
            LeanTween.moveX(gameObject.GetComponent<RectTransform>(), isActive ? startX : offsetX, .7f)
                .setOnStart(() => {
                    btnRecipe.interactable = false;
                    UIGameManager.Instance.noClickSetActive(isActive);
                    })
                .setOnComplete(() => btnRecipe.interactable = true)
                .setEase(animationCurve);
            isActive = !isActive;
        }
    }
}
