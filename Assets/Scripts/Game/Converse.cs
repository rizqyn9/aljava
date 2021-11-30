using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aljava.Game
{
    public class Converse : MonoBehaviour
    {
        [Header("Properties")]
        public TMPro.TMP_Text text;
        public Button nextBtn;
        public float offsetChar, offsetDialog;
        public GameObject dialogContainer, character;

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


        public bool dialogIsActive = false;
        public bool isLastDialog = true;
        public void showDialog(string _text, bool _isLastDialog = true, Action _bindNextButton = null)
        {
            isLastDialog = _isLastDialog;
            if (_bindNextButton != null) bindNextButton = _bindNextButton;

            isNextClicked = true;

            dialogIsActive = true;
            Time.timeScale = 0;
            nextBtn.gameObject.SetActive(false);
            
            animateChar(true)
                .setOnStart(() => dialogContainer.SetActive(true))
                .setOnComplete(() =>
                        animateDialog(true)
                            .setOnStart(() => StartCoroutine(ITextRender(_text)))
                    );
        }

        public void closeDialog()
        {
            animateDialog(false)
                .setOnComplete(() => dialogContainer.SetActive(false));
            animateChar(false)
                .setDelay(.3f)
                .setOnComplete(() => character.SetActive(false));
        }

        public void updateDialog()
        {

        }

        public bool isNext = false;
        Action bindNextButton;

        public bool isNextClicked = false;
        public void Btn_Next()
        {
            if (isLastDialog) closeDialog();
            else
            {
                text.text = "";
            }
            //GameController.Tutorial.tutorialState = TutorialState.RESOLVE;
            isNextClicked = true;
            nextBtn.gameObject.SetActive(false);
        }

        IEnumerator ITextRender(string _text)
        {
            text.text = "";
            foreach(char letter in _text.ToCharArray())
            {
                text.text += letter;
                yield return null;
            }
            nextBtn.gameObject.SetActive(true);
        }
    }
}
