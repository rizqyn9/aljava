using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIGameManager : Singleton<UIGameManager>, IGameState
    {
        [Header("Properties")]
        [SerializeField] UIMachineManager machineManager;

        public static UIMachineManager MachineManager => Instance.machineManager;

        #region Game State Handler
        void OnEnable() => GameController.OnGameStateChanged += GameStateHandler;
        void OnDisable() => GameController.OnGameStateChanged -= GameStateHandler;

        public GameState gameState => GameController.GameState;
        public void GameStateHandler() => GameStateController.UpdateGameState(this);
        public GameObject GetGameObject() => gameObject;

        public void OnGameInit()
        {
            
        }

        public void OnGameBeforeStart()
        {

        }

        public void OnGameStart() { }
        public void OnGameClearance() { }
        public void OnGameFinish() { }
        public void OnGameIddle() { }
        public void OnGamePause() { }
        #endregion

        public void init()
        {

        }
    }
}
