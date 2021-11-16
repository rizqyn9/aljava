using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class UI_Win : MonoBehaviour
    {
        [Header("Properties")]
        public TMP_Text text;
        public List<Image> stars;

        private void Start()
        {
            text.text = "";
            stars.ForEach(val =>
            {
                val.gameObject.transform.localScale = Vector2.zero;
            });
        }

        [ContextMenu("test")]
        public void test()
        {
            spawn(3);
        }

        public void spawn(int _total)
        {
            LeanTween.moveY(gameObject.GetComponent<RectTransform>(), 0,2f).setEaseInBounce().setOnComplete(() =>
            {
                for (int i = 0; i < _total; i++)
                    LeanTween.scale(stars[i].gameObject, new Vector2(1f, 1f), .7f).setDelay(.2f * i).setEaseInBounce();
            });
        }

        public void Btn_Restart()
        {

        }

        public void Btn_Home()
        {

        }

        public void Btn_Next()
        {

        }
    }
}
