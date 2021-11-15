using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class OrderController : MonoBehaviour
    {
        [Header("Debug")]
        public List<BuyerPrototype> listOrderQueue = new List<BuyerPrototype>();

        public void init()
        {

        }

        public void reqOrder(BuyerPrototype _buyerPrototype)
        {
            listOrderQueue.Add(_buyerPrototype);
        }

        public bool isMenuInQueue(MenuBase _menu, out CustomerHandler _customerHandler)
        {
            bool res = false;
            _customerHandler = null;
            foreach(BuyerPrototype queue in listOrderQueue)
                if(res = queue.menuListNames.Find(val => val.menuListName == _menu.menuListName))
                {
                    _customerHandler = queue.customerHandler;
                    print("Found Menu");
                    break;
                }
            return res;
        }
    }
}
