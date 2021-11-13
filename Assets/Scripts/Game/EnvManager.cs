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
        public MachineManager machineManager;

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

        public void OnGameInit()
        {
            getAllResources();

            machineManager.init(this);
        }

        public void OnGameBeforeStart()
        {
            machineManager.OnGameBeforeStart();
        }

        public void OnGameStart() { }
        public void OnGameClearance() { }
        public void OnGameFinish() { }
        public void OnGameIddle() { }
        public void OnGamePause() { }
        #endregion

        void getAllResources()
        {
            listMenus = ResourceManager.ListMenus.FindAll(val => GameController.Instance.levelBase.MenuTypeUnlock.Contains(val.menuListName));
            listBuyers = ResourceManager.ListBuyers.FindAll(val => GameController.Instance.levelBase.BuyerTypeUnlock.Contains(val.enumBuyerType));
            listMachines = validateMachineWillInstance();
        }

        [SerializeField] List<MachineIgrendient> _machineTypes;
        List<MachineBase> validateMachineWillInstance()
        {
            _machineTypes = new List<MachineIgrendient>();
            List<MachineBase> _listMachine = new List<MachineBase>();

            foreach (MenuBase _menu in listMenus)
            {
                foreach (MachineIgrendient _machineType in _menu.Igrendients)
                {
                    if (!_machineTypes.Contains(_machineType))
                    {
                        _machineTypes.Add(_machineType);
                        _listMachine.Add(ResourceManager.ListMachines.Find(val => val.machineType == _machineType));
                    }
                }
            }
            return _listMachine;
        }
    }
}
