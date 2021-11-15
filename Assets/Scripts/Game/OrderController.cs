using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class OrderController : MonoBehaviour
    {
        [Header("Debug")]
        public List<BuyerPrototype> orderQueque = new List<BuyerPrototype>();

        public void init()
        {

        }

        public void reqOrder(BuyerPrototype _buyerPrototype)
        {
            orderQueque.Add(_buyerPrototype);
        }
    }
}
