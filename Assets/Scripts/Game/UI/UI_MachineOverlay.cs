using System.Collections;
using UnityEngine;

namespace Aljava.Game
{
    public class UI_MachineOverlay : MonoBehaviour
    {
        [Header("Properties")]
        public float offsetY = -400;
        public Animator animator;

        [Header("Debug")]
        public Machine machine;
        [SerializeField] GameObject container;
        public bool isInteractable = false;
        public bool isOverlayResolve = false;
        public bool isMiniGameSuccess = false;

        private void OnEnable()
        {
            if(animator) animator.enabled = false;
        }

        public void init(Machine _machine, GameObject _container)
        {
            machine = _machine;
            container = _container;
            gameObject.SetActive(false);
        }

        public void spawn()
        {
            gameObject.SetActive(true);
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
            gameObject.SetActive(false);
        }

        public void Btn_True()
        {
            isOverlayResolve = true;
            if(machine.machineType == MachineIgrendient.BEANS_ROBUSTA)
            {
                animator.enabled = true;
                animator.Play("run");
                StartCoroutine(IListen());
            }
        }

        IEnumerator IListen()
        {
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("done"));
            print("aniim done");
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
