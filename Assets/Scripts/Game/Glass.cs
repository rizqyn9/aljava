using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Glass : MonoBehaviour
    {
        [Header("Properties")]
        public SpriteRenderer igrendientSprite;     // On start igrendient will render image is front of glass

        [Header("Debug")]
        public int glassCode;
        public GlassState glassState = GlassState.PROCESS;          // ensure glass cannot filled on first instance
        public List<MachineIgrendient> igrendients = new List<MachineIgrendient>();
        public MenuBase menuResult;
        public bool isValidMenu = false;
        public GlassRegistered glassRegistered;
        [SerializeField] BoxCollider2D boxCollider2D;
        [SerializeField] BuyerPrototype buyerPrototype;
        [SerializeField] MachineIgrendient _lastIgrendients = MachineIgrendient.NULL;
        public MachineIgrendient lastIgrendients
        {
            get => _lastIgrendients;
            set
            {
                checkMenu();
                _lastIgrendients = value;
            }
        }

        private void Awake() => boxCollider2D = GetComponent<BoxCollider2D>();

        public void init(GlassRegistered _glassRegistered)
        {
            glassRegistered = _glassRegistered;
        }

        private void OnDoubleClick()
        {
            if (lastIgrendients == MachineIgrendient.NULL) return;
            EnvManager.Trash.throwTrash();
            glassDestroyed();
        }

        void OnSingleClick()
        {
            boxCollider2D.enabled = false;
            if (isValidMenu)
                if (GameController.OrderController.isMenuInQueue(menuResult, out buyerPrototype))
                {
                    buyerPrototype.customerHandler.OnMenuServe(menuResult);
                    //LeanTween.move(gameObject, new LTSpline(new Vector3[] { Vector3.zero, new Vector3(100, 100) }), 2f);
                    LeanTween.scale(gameObject, Vector3.zero, 1f).setOnComplete(handleOnServe);
                } else
                    boxCollider2D.enabled = true;
            else
                boxCollider2D.enabled = true;
        }

        public void handleOnServe()
        {
            glassDestroyed();
        }

        public void glassDestroyed()
        {
            EnvManager.GlassManager.reqGlassSpawn(glassRegistered.seatIndex);
            Destroy(gameObject);
        }

        public void addIgrendients(MachineIgrendient _igrendient, Sprite _sprite)
        {
            igrendients.Add(_igrendient);
            igrendientSprite.sprite = _sprite;
            onFillIgrendients();
            lastIgrendients = _igrendient;
        }

        void checkMenu() => isValidMenu = GlassManager.MenuChecker(igrendients, out menuResult);

        public void addIgrendients(List<MachineIgrendient> _igrendients, Sprite _sprite)
        {
            igrendients.AddRange(_igrendients);
            if(_sprite != null)
                igrendientSprite.sprite = _sprite;
            onFillIgrendients();
            lastIgrendients = _igrendients[_igrendients.Count - 1];
        }

        void onFillIgrendients()
        {
            glassState = GlassState.PROCESS;
            LeanTween.scale(gameObject, new Vector2(.3f, .3f), .2f).setLoopPingPong(2).setOnComplete(() => glassState = GlassState.FILLED);
        }

        #region Handle Tap
        [SerializeField] int tap = 0;
        [SerializeField] float interval = .3f;
        private void OnMouseDown()
        {
            tap++;
            if (tap == 1)
            {
                StartCoroutine(IDoubleClick());
            }
            else if (tap > 1)
            {
                tap = 0;    // reset
                OnDoubleClick();
            }
        }

        IEnumerator IDoubleClick()
        {
            yield return new WaitForSeconds(interval);
            if (tap == 1)
            {
                OnSingleClick();
            }
            tap = 0;
        }
        #endregion
    }
}
