using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIPatience : MonoBehaviour
    {
        [Header("Properties")]
        public Image image;

        [Header("Debug")]
        public CustomerHandler customerHandler;
        public float duration;
        public int leanTweenID;

        public void init(CustomerHandler _customerHandler)
        {
            customerHandler = _customerHandler;
            customerHandler.patience = this;

            image.fillAmount = 0;
        }

        public void run()
        {
            leanTweenID = LeanTween.value(0, 100, 10f).setOnUpdate(val =>
            {
                image.fillAmount = 1 - (val / 100);
                if (val <= 50) image.color = Color.green;
                if (val >= 50) image.color = Color.red;
            }).setOnComplete(patienceRunOut).id;
        }

        void patienceRunOut()
        {
            customerHandler.OnPatienceRunOut();
            UIGameManager.HealthManager.decrement();
        }

        private void OnDestroy()
        {
            LeanTween.cancel(leanTweenID);
        }
    }
}
