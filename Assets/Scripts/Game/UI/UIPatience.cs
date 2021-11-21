using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aljava.Game
{
    public class UIPatience : MonoBehaviour
    {
        [Header("Properties")]
        public Image image;

        [Header("Debug")]
        public CustomerHandler customerHandler;
        public float duration;
        public int leanTweenID;

        public string BUYER_TALK = "BUYER_TALK",
            BUYER_IDDLE = "BUYER_IDDLE",
            BUYER_ANGRY = "BUYER_ANGRY",
            BUYER_HAPPY = "BUYER_HAPPY";

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
                handlePatience(val);
            }).setOnComplete(patienceRunOut).id;
        }

        private void handlePatience(float val)
        {

            if(val < 10)
                customerHandler.changeAnimation(BUYER_TALK);
            else if(val < 20)
                customerHandler.changeAnimation(BUYER_IDDLE);
            else if(val > 80)
                customerHandler.changeAnimation(BUYER_ANGRY);
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
