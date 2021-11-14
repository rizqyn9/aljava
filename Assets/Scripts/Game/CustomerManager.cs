using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CustomerManager : Singleton<CustomerManager>, IGameState
    {
        [Header("Properties")]
        public GameObject seat;

        [Header("Debug")]
        public List<TransformSeatData> transformSeatDatas;


        void OnValidate()
        {
            foreach (Transform to in seat.GetComponentsInChildren<Transform>())
                if (to.CompareTag("seat")) transformSeatDatas.Add(new TransformSeatData() { isSeatAvaible = false, transform = to });
        }


        #region Game State Handler
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
