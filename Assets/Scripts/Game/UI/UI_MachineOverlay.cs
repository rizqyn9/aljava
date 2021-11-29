using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public class UI_MachineOverlay : MonoBehaviour
    {
        [Header("Properties")]
        public float offsetY = -250f;

        [Header("Debug")]
        [SerializeField] GameObject container;
        [SerializeField] Machine machine;
        [SerializeField] bool isInteractable = false;
        public bool isOverlayResolve = false;

        public void init(Machine _machine, GameObject _container)
        {
            machine = _machine;
            container = _container;
        }

        public void spawn()
        {
            print("Spawm");
            LeanTween.moveY(container.GetComponent<RectTransform>(), 0, .5f)
                .setOnStart(() => gameObject.SetActive(true))
                .setOnComplete(() =>
                {
                    UIGameManager.Instance.noClickSetActive(true);
                    isInteractable = true;
                });

        }

        public void hide() =>
            LeanTween.moveY(container.GetComponent<RectTransform>(), offsetY, .5f)
                .setOnStart(() =>
                {
                    UIGameManager.Instance.noClickSetActive(false);
                    isInteractable = false;
                })
                .setOnComplete(() => gameObject.SetActive(false));

        public void Btn_True()
        {
            hide();
        }

        public void Btn_False()
        {
            Debug.LogWarning("False");
        }
    }
}
