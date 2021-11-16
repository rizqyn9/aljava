using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RulesController : MonoBehaviour
    {
        [Header("Debug")]
        public int buyerInstance = 0;
        public int buyerSuccessTotal = 0;
        public int buyerFailTotal = 0;

        public int menuInstanceTotal = 0;
        public int menuSuccessTotal = 0;
        public int menuFailTotal = 0;

        public int earnMoneyTotal = 0;
        public int earnPointTotal = 0;

        public void handleBuyerDone(int _menuSuccess)
        {
            buyerSuccessTotal += 1;
            menuSuccessTotal += _menuSuccess;
        }

        public void handleBuyerFail(int _menuFail)
        {
            buyerFailTotal += 1;
            menuFailTotal += _menuFail;
        }
    }
}
