using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aljava.Game
{
    public struct DialogModel
    {
        public Image characterImage;
        public List<TextDialogModel> texts;
    }

    public struct TextDialogModel
    {
        public bool autoSkip;
        public string text;
    }
    public class Converse : MonoBehaviour
    {
        public float offsetChar, offsetDialog;
        public Image character;
        public TMPro.TMP_Text text;
        public GameObject dialogContainer;

        public List<DialogModel> dialogModels = new List<DialogModel>();

        public void init()
        {
            animateChar(true, () => animateDialog(true));
        }

        void animateChar(bool setActive, Action setComplete) =>
            LeanTween.moveX(character.GetComponent<RectTransform>(), setActive ? 0 : offsetChar, .3f)
            .setOnComplete(setComplete);

        void animateDialog(bool setActive) =>
            LeanTween.moveY(dialogContainer.GetComponent<RectTransform>(), setActive ? 0 : offsetDialog, .3f);

        void showDialog()
        {
            LeanTween.scale(dialogContainer.GetComponent<RectTransform>(), new Vector2(1, 1), .5f)
                .setFrom(Vector2.zero)
                .setOnComplete(showText);
        }

        void showText()
        {
            text.text = "asdgjad test init";
            LeanTween.alpha(text.GetComponent<RectTransform>(), 1, .5f)
                .setFrom(0);
        }

        public bool isNext = false;
        public void Btn_Next()
        {

        }

        [ContextMenu("Test INIt")]
        public void test()
        {
            init();
        }
    }
}
