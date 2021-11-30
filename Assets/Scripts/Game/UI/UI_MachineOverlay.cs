using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public class UI_MachineOverlay : MonoBehaviour
    {
        [Header("Properties")]
        public float offsetY = -400;

        [Header("Debug")]
        public Machine machine;
        [SerializeField] GameObject container;
        public bool isInteractable = false;
        public bool isOverlayResolve = false;
        public bool isMiniGameSuccess = false;

        public void init(Machine _machine, GameObject _container)
        {
            machine = _machine;
            container = _container;
        }

        public void spawn()
        {
            isMiniGameSuccess = false;
            LeanTween.moveY(container.GetComponent<RectTransform>(), 0, .5f)
                .setOnStart(() => gameObject.SetActive(true))
                .setOnComplete(() =>
                {
                    UIGameManager.Instance.noClickSetActive(true);
                    isInteractable = true;
                })
                .setIgnoreTimeScale(true);

            UIGameManager.MachineManager.activeMachineOverlay = this;
        }

        public void hide()
        {
            LeanTween.moveY(container.GetComponent<RectTransform>(), offsetY, .5f)
                .setOnStart(() =>
                {
                    UIGameManager.Instance.noClickSetActive(false);
                    isInteractable = false;
                })
                .setOnComplete(() => gameObject.SetActive(false))
                .setIgnoreTimeScale(true);

            UIGameManager.MachineManager.activeMachineOverlay = null;
        }

        public void Btn_True()
        {
            isOverlayResolve = true;
        }

        public void Btn_False()
        {
            isOverlayResolve = false;
        }

        public void Btn_Resolve()
        {
            isMiniGameSuccess = true;
            hide();
        }
    }
}
