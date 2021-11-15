using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CustomerHandler : MonoBehaviour
    {
        [Header("Properties")]
        public Transform charPos;
        public Transform bublePos;

        [Header("Debug")]
        public BuyerPrototype buyerPrototype;
        public enumBuyerType buyerType;
        public GameObject characterGO;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        public UIBubbles bubbles;

        public void init(BuyerPrototype _buyerPrototype)
        {
            buyerPrototype = _buyerPrototype;
            gameObject.name = _buyerPrototype.customerCode;
            buyerType = _buyerPrototype.buyerBase.buyerType;

            characterGO = Instantiate(_buyerPrototype.buyerBase.buyerPrefab, charPos);
            animator = characterGO.GetComponentInChildren<Animator>();
            spriteRenderer = characterGO.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sortingOrder = 0;

            buyerPrototype.customerHandler = this;

            UIGameManager.BubblesManager.reqInstance(buyerPrototype, out bubbles);

            StartCoroutine(IWalkSeat());
        }

        public void OnMenuServe(MenuBase _menu)
        {
            buyerPrototype.menuListNames.Remove(_menu);

            bubbles.OnMenuServe(_menu.menuListName);
            if (buyerPrototype.menuListNames.Count < 1)
                allMenusDone();
        }

        void allMenusDone()
        {
            StartCoroutine(IWalkOut());
        }

        IEnumerator IWalkOut()
        {
            yield return new WaitForSeconds(.4f);
            LeanTween.moveX(gameObject, buyerPrototype.spawnPos.x, duration)
                .setOnComplete(handleOut);
        }

        void handleOut()
        {
            Destroy(gameObject);
        }

        #region handle seat
        [SerializeField] float direction;
        [SerializeField] float duration;
        IEnumerator IWalkSeat()
        {
            direction = buyerPrototype.spawnPos.x - buyerPrototype.seatPos.x;
            direction = direction < 0 ? direction * -1 : direction;
            duration = direction / 5;

            LeanTween.moveX(gameObject, buyerPrototype.seatPos.x, duration)
                .setOnComplete(() => StartCoroutine(IHandleOnSeat()));

            yield return 1;
        }

        IEnumerator IHandleOnSeat()
        {
            spriteRenderer.sortingOrder = 1;
            bubbles.show();

            yield return new WaitForSeconds(1);     // TODO code: 2 value on seat must sync wih tweening menu item
            GameController.OrderController.reqOrder(buyerPrototype);
        }
        #endregion

    }
}
