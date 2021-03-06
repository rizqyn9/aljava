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

        GraphicRaycaster raycast;
        private void OnEnable()
        {
            raycast = gameObject.GetComponent<GraphicRaycaster>();
            raycast.enabled = false;
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
        public bool usePaused;
        public void showDialog
            (
                string _text,
                bool _isLastDialog = true,
                bool _usePaused = true,
                Action _bindNextButton = null,
                bool _showNextBtn = true,
                Action _cbOnDone = null,
                Action _cbOnStart = null
            )
        {
            isLastDialog = _isLastDialog;

            if (_usePaused) Time.timeScale = 0;
            usePaused = _usePaused;

            if (_bindNextButton != null) bindNextButton = _bindNextButton;

            isNextClicked = false;

            dialogIsActive = true;
            Time.timeScale = 0;
            nextBtn.gameObject.SetActive(false);

            _cbOnStart?.Invoke();

            animateChar(true)
                .setOnStart(() =>
                {
                    character.SetActive(true);
                    dialogContainer.SetActive(true);
                })
                .setOnComplete(() =>
                        animateDialog(true)
                            .setOnStart(() => StartCoroutine(ITextRender(_text, _showNextBtn, _cbOnDone)))
                    );
        }

        public void closeDialog()
        {
            animateDialog(false)
                .setOnComplete(() => dialogContainer.SetActive(false));
            animateChar(false)
                .setDelay(.3f)
                .setOnComplete(() =>
                {
                    if (usePaused) Time.timeScale = 0;
                    character.SetActive(false);
                });
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

        IEnumerator ITextRender(string _text, bool _showNextBtn, Action _cb)
        {
            text.text = "";
            //foreach (char letter in _text.ToCharArray())
            //{
            //    text.text += letter;
            //    yield return null;
            //}
            text.text = _text;
            yield return null;

            _cb?.Invoke();
            raycast.enabled = _showNextBtn;
            nextBtn.gameObject.SetActive(_showNextBtn);
        }
    }
}
