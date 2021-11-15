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

        public bool isMenuInQueue(MenuBase _menu, out BuyerPrototype _buyerPrototype)
        {
            bool res = false;
            _buyerPrototype = new BuyerPrototype();
            foreach(BuyerPrototype queue in listOrderQueue)
                if(res = queue.menuListNames.Find(val => val.menuListName == _menu.menuListName))
                {
                    _buyerPrototype = queue;
                    _buyerPrototype.menuListNames.Remove(_menu);        // remove from cache
                    break;
                }
            return res;
        }
    }
}
