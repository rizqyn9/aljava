using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnvManager : Singleton<EnvManager>, IGameState
    {
        [Header("Properties")]
        public GameObject animCharBackground;

        [Header("Debug")]
        public List<MachineBase> listMachines = new List<MachineBase>(); 
        public List<MenuBase> listMenus = new List<MenuBase>(); 
        public List<BuyerBase> listBuyers = new List<BuyerBase>(); 

        #region Game State Handler
        void OnEnable() => GameController.OnGameStateChanged += GameStateHandler;
        void OnDisable() => GameController.OnGameStateChanged -= GameStateHandler;

        public GameState gameState => GameController.GameState;
        public void GameStateHandler() => GameStateController.UpdateGameState(this);
        public GameObject GetGameObject() => gameObject;

        public void OnGameBeforeStart()
        {
            getAllResources();
        }


        public void OnGameStart() { }
        public void OnGameClearance() { }
        public void OnGameFinish() { }
        public void OnGameIddle() { }
        public void OnGameInit() { }
        public void OnGamePause() { }
        #endregion

        private void getAllResources()
        {
            listMenus = ResourceManager.Instance.ListMenus.FindAll(val => GameController.Instance.levelBase.MenuTypeUnlock.Contains(val.menuListName));
            listBuyers = ResourceManager.Instance.ListBuyers.FindAll(val => GameController.Instance.levelBase.BuyerTypeUnlock.Contains(val.enumBuyerType));
        }
    }
}
