using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class RulesController : MonoBehaviour
    {
        [Header("Debug")]
        public List<MenuBase> listMenuDones = new List<MenuBase>();


        [SerializeField] MenuBase menu;
        [SerializeField] CustomerHandler customerHandler;
        public void handleMenuDone(MenuBase _menu, CustomerHandler _customerHandler)
        {
            listMenuDones.Add(_menu);
            customerHandler = _customerHandler;
        }

    }
}
