using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CustomerManager : Singleton<CustomerManager>, IGameState
    {
        #region Game State Handler
        public GameState gameState => GameController.GameState;
        void OnEnable() => GameController.OnGameStateChanged += GameStateHandler;
        void OnDisable() => GameController.OnGameStateChanged -= GameStateHandler;
        public void GameStateHandler() => GameStateController.UpdateGameState(this);
        public GameObject GetGameObject() => gameObject;

        public void OnGameBeforeStart()
        {

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
