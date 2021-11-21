using System.Collections.Generic;
using UnityEngine;

namespace Aljava.Game
{
    public class EnvManager : Singleton<EnvManager>, IGameState
    {
        [Header("Properties")]
        public GameObject animCharBackground;
        public MachineManager machineManager;
        public Trash trash;
        public GlassManager glassManager;

        [Header("Debug")]
        public List<MachineBase> listMachines = new List<MachineBase>(); 
        public List<MenuBase> listMenus = new List<MenuBase>(); 
        public List<BuyerBase> listBuyers = new List<BuyerBase>();

        #region Accessor
        public static Trash Trash => Instance.trash;
        public static GlassManager GlassManager => Instance.glassManager;
        public static List<MachineBase> ListMachines => Instance.listMachines;
        public static List<MenuBase> ListMenus => Instance.listMenus;
        public static List<BuyerBase> ListBuyers => Instance.listBuyers;
        #endregion

        private void Start()
        {
            LeanTween.alpha(animCharBackground, 0, 0);
        }

        #region Game State Handler
        void OnEnable() => GameController.OnGameStateChanged += GameStateHandler;
        void OnDisable() => GameController.OnGameStateChanged -= GameStateHandler;

        [SerializeField] GameState _gameState;
        public GameState gameState => GameController.GameState;
        public void GameStateHandler() => GameStateController.UpdateGameState(this);
        public GameObject GetGameObject() => gameObject;

        public void OnGameInit()
        {
            getAllResources();

            UIGameManager.Recipe.init(listMenus);

            machineManager.init(this);
        }

        public void OnGameBeforeStart()
        {
            glassManager.init();
            machineManager.OnGameBeforeStart();
        }

        public void OnGameStart()
        {
            LeanTween.alpha(animCharBackground, 1, .5f).setLoopCount(2);
            machineManager.OnGameStart();
        }
        public void OnGameClearance() { }
        public void OnGameFinish()
        {
            machineManager.OnGameFinished();
        }

        public void OnGameIddle() { }
        public void OnGamePause() { }
        #endregion

        void getAllResources()
        {
            listMenus = ResourceManager.ListMenus.FindAll(val => GameController.Instance.levelBase.MenuTypeUnlock.Contains(val.menuListName));
            listBuyers = ResourceManager.ListBuyers.FindAll(val => GameController.Instance.levelBase.BuyerTypeUnlock.Contains(val.buyerType));
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
