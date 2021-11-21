using UnityEngine;

namespace Aljava.Game
{
    public class RulesController : MonoBehaviour
    {
        [Header("Debug")]
        public int buyerInstance = 0;
        [SerializeField] int buyerSuccessTotal = 0;
        public int buyerFailTotal = 0;

        public int menuInstanceTotal = 0;
        public int menuSuccessTotal = 0;
        public int menuFailTotal = 0;

        public int earnMoneyTotal = 0;
        public int earnPointTotal = 0;

        public void handleBuyerDone(int _menuSuccess)
        {
            buyerSuccessTotal += 1;
            print("Trigger");
            menuSuccessTotal += _menuSuccess;
        }

        public void handleBuyerFail(int _menuFail)
        {
            buyerFailTotal += 1;
            menuFailTotal += _menuFail;
        }

        public bool isHealthRunOut = false;
        public void handleHealthRunOut()
        {
            isHealthRunOut = true;
            initializeLose();
        }

        public void handleGameTimeOut()
        {
            if (isHealthRunOut) return;
            GameController.GameState = GameState.CLEARANCE;

            CustomerManager.Instance.gameTimeOut = true;
            if(CustomerManager.Instance.findAvaibleSeat().Count == 3)
            {
                handleCustomerGameManagerTimeOut();
            }

        }

        public void handleCustomerGameManagerTimeOut()
        {
            if (isHealthRunOut) return;
            initializeWin();
        }


        public void initializeWin()
        {
            //int star = 0;
            //if (buyerInstance == buyerSuccessTotal) star = 3;
            //else if (buyerSuccessTotal / buyerInstance * 10 >= 5) star = 2;
            //else if (buyerSuccessTotal / buyerInstance * 10 <= 5) star = 1;
            GameController.Instance.handleGameEnd();
            UIGameManager.Win.init(UIGameManager.HealthManager.instance);
        }

        public void initializeLose()
        {
            GameController.Instance.handleGameEnd();
            UIGameManager.Lose.init();
        }
    }
}
