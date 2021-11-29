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
        public TMPro.TMP_Text text;
        public GameObject dialogContainer, character;

        public List<DialogModel> dialogModels = new List<DialogModel>();

        public void init()
        {
        }

        LTDescr animateChar(bool setActive) =>
            LeanTween.moveX(character.GetComponent<RectTransform>(), setActive ? 0 : offsetChar, .3f)
            .setIgnoreTimeScale(true);

        LTDescr animateDialog(bool setActive) =>
            LeanTween.moveY(dialogContainer.GetComponent<RectTransform>(), setActive ? 0 : offsetDialog, setActive ? .3f : .5f)
            .setIgnoreTimeScale(true);

        void renderText(string _text) =>
            text.text = _text;

        void showDialog()
        {
            LeanTween.scale(dialogContainer.GetComponent<RectTransform>(), new Vector2(1, 1), .5f)
                .setFrom(Vector2.zero);
        }

        public void setDialog(string text)
        {
            Time.timeScale = 0;
            animateChar(true)
                .setOnStart(() => dialogContainer.SetActive(true))
                .setOnComplete(() =>
                        animateDialog(true)
                            .setOnStart(() => renderText(text))
                    );
        }

        public bool isNext = false;
        public void Btn_Next()
        {
            Debug.Log("Next chat");
        }

        public LTSeq seq;
        [ContextMenu("Test INIt")]
        public void test()
        {
            animateChar(true)
                .setOnStart(() => dialogContainer.SetActive(true))
                .setOnComplete(() =>
                        animateDialog(true)
                            .setOnStart(() => renderText("Test"))
                    );
        }

        [ContextMenu("Close")]
        public void closed()
        {
            animateDialog(false)
                .setOnComplete(() => animateChar(false))
                .setOnComplete(() => dialogContainer.SetActive(false));
        }
    }
}
