using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
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
        [SerializeField] BuyerPrototype targetBuyer;
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

        void checkMenu() { }
        public void init(GlassRegistered _glassRegistered)
        {
            glassRegistered = _glassRegistered;

        }

        private void OnDoubleClick()
        {
            if (lastIgrendients == MachineIgrendient.NULL) return;
            //OnTrash();
        }

        void OnSingleClick()
        {

        }

        #region Handle Tap
        int tap = 0;
        float interval = .3f;
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
