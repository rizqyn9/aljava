using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIBubblesManager : MonoBehaviour
    {
        [Header("Properties")]
        public GameObject baseBubble;

        [Header("Debug")]
        public List<UIBubbles> UIBubblesList;
        public BuyerPrototype buyerPrototype;

        public void init()
        {

        }

        public void reqInstance(BuyerPrototype _buyerPrototype, out UIBubbles bubbles)
        {
            buyerPrototype = _buyerPrototype;

            bubbles = Instantiate(baseBubble, transform).GetComponent<UIBubbles>();
            bubbles.init(buyerPrototype);

            UIBubblesList.Add(bubbles);

        }
    }
}
