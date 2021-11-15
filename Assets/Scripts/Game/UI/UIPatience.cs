using System.Collections;
using System.Collections.Generic;
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

        public void init(CustomerHandler _customerHandler)
        {
            customerHandler = _customerHandler;
            customerHandler.patience = this;

            image.fillAmount = 0;
        }

        public void run()
        {
            LeanTween.value(0, 100, 10f).setOnUpdate(val =>
            {
                image.fillAmount = 1 - (val / 100);
            }).setOnComplete(patienceRunOut);
        }

        void patienceRunOut()
        {
            customerHandler.OnPatienceRunOut();
        }
    }
}
