using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIBubbles : MonoBehaviour
    {
        [Header("Properties")]
        public Transform itemsPos;
        public GameObject baseBubbleItem;

        [Header("Debug")]
        public BuyerPrototype buyerPrototype;
        public List<GameObject> itemGO;

        /**
         * animate on first init
         * regist menu list
         * create animate trigger
         * create handle menu onDone
         * create handle menu onFailedTo Serve
         */
        public void istanceMenuGraph()
        {

        }

        public void init(BuyerPrototype _buyerPrototype)
        {
            buyerPrototype = _buyerPrototype;
            print($"req init from {_buyerPrototype.customerCode}");
        }
    }
}
