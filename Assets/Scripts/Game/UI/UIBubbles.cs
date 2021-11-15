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
        public List<GameObject> itemGO;
        public CustomerHandler customerHandler; // TO comunicate 

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
    }
}
