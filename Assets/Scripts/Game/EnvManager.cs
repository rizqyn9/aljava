using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnvManager : MonoBehaviour, IGameState
    {
        [Header("Properties")]
        public GameObject animCharBackground;

        //[Header("Debug")]

        public GameState gameState => GameController.GameState;
        public void GameStateHandler() => GameStateController.UpdateGameState(this);

        #region Game State Handler
        public GameObject GetGameObject() => gameObject;

        /**
         * Init data machine
         * Init Buyer
         */
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
