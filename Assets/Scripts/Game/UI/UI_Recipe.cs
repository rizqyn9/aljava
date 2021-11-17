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
        public Button btnRecipe;
        public AnimationCurve animationCurve; 

        [SerializeField] bool isActive;
        public void Btn_Recipe()
        {
            LeanTween.moveX(gameObject.GetComponent<RectTransform>(), isActive ? 0 : offsetX, .7f)
                .setOnStart(() => btnRecipe.interactable = false)
                .setOnComplete(() => btnRecipe.interactable = true)
                .setEase(animationCurve);
            isActive = !isActive;
        }
    }
}
