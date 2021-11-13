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
        public List<Machine> machines = new List<Machine>();

        #region Game State Handler
        void OnEnable() => GameController.OnGameStateChanged += GameStateHandler;
        void OnDisable() => GameController.OnGameStateChanged -= GameStateHandler;

        public GameState gameState => GameController.GameState;
        public void GameStateHandler() => GameStateController.UpdateGameState(this);
        public GameObject GetGameObject() => gameObject;

        public void OnGameBeforeStart()
        {
            print("Env before start");
        }

        public void OnGameStart() { }
        public void OnGameClearance() { }
        public void OnGameFinish() { }
        public void OnGameIddle() { }
        public void OnGameInit() { }
        public void OnGamePause() { }
        #endregion
    }
}
