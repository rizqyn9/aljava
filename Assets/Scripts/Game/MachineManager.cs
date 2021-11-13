using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MachineManager : MonoBehaviour, IGameState
    {
        public EnvManager envManager;

        public void GameStateHandler() => GameStateController.UpdateGameState(this);

        public GameObject GetGameObject() => gameObject;
        public void OnGameBeforeStart() { }
        public void OnGameStart() { }
        public void OnGameClearance() { }
        public void OnGameFinish() { }
        public void OnGameIddle() { }
        public void OnGameInit() { }
        public void OnGamePause() { }

        [ContextMenu("Test")]
        public void test()
        {
            Debug.Log(envManager.gameState);
        }
    }
}
