using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CustomerHandler : MonoBehaviour
    {
        [Header("Properties")]
        public BuyerPrototype buyerPrototype;

        public void init(BuyerPrototype _buyerPrototype)
        {
            buyerPrototype = _buyerPrototype;
            print(_buyerPrototype.customerCode);
        }
    }
}
