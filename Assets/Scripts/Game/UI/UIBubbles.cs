using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [System.Serializable]
    public struct ItemMenu
    {
        public GameObject go;
        public MenuListName menuListName;
    }

    public class UIBubbles : MonoBehaviour
    {
        [Header("Properties")]
        public Transform itemsPos;
        public GameObject baseBubbleItem;

        [Header("Debug")]
        public BuyerPrototype buyerPrototype;
        public List<ItemMenu> listItem;
        public RectTransform rectTransform;


        private void Awake()
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
        }

        private void Start()
        {
            LeanTween.alpha(rectTransform, 0, 0);
        }

        /**
         * animate on first init
         * regist menu list
         * create animate trigger
         * create handle menu onDone
         * create handle menu onFailedTo Serve
         */
        public void init(BuyerPrototype _buyerPrototype)
        {
            buyerPrototype = _buyerPrototype;
            print($"req init from {_buyerPrototype.customerCode}");

            setUpMenu();
        }

        private void setUpMenu() =>
            buyerPrototype.menuListNames.ForEach(val =>
            {
                GameObject go = null;
                if (val.menuSprite)
                {
                    go = Instantiate(baseBubbleItem, itemsPos);
                    go.GetComponent<Image>().sprite = val.menuSprite;
                    go.transform.localScale = Vector2.zero;
                }
                listItem.Add(new ItemMenu() { go = go, menuListName = val.menuListName });
            });

        public void show()
        {
            transform.position = Camera.main.WorldToScreenPoint(buyerPrototype.customerHandler.bublePos.position);
            //LeanTween.alpha(rectTransform.GetComponentInChildren<RectTransform>(), 1, 1f).setOnComplete(animateMenu);
            //LeanTween.scale(rectTransform, new Vector2(1.2f, 1.2f), .2f).setLoopPingPong(2);
        }

        void animateMenu() =>
            listItem.ForEach(val =>
                LeanTween.scale(val.go, new Vector2(1, 1), .5f));

        public void OnMenuServe(MenuListName _menu, bool isAllServe = false)
        {
            listItem.Find(val => val.menuListName == _menu);
        }

        public void OnMenusDone()
        {

        }

        public void OnMenusFailed()
        {

        }

        void closeItem(GameObject go) => LeanTween.scale(go, Vector2.zero, .5f);

        void closeBubble() =>
            LeanTween.scale(gameObject, Vector2.zero, .3f);
    }
}
