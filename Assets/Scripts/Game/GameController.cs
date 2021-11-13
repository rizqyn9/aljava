using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : Singleton<GameController>
    {
        [Header("Debug")]
        public LevelBase levelBase;

        #region Game State
        public static event Action OnGameStateChanged;
        [SerializeField] GameState _gameState = GameState.NULL;    // On first init gameState have null value which to prevent crash on scene level changes
        public static GameState GameState
        {
            get => Instance._gameState;
            set
            {
                print($"<color=green> Game State Changed {value} </color>");
                OnGameStateChanged?.Invoke();
                Instance._gameState = value;
            }
        }
        #endregion

        public void init(LevelBase _levelbase)
        {
            print("Game Initialize");
            levelBase = _levelbase;
            
        }

        public void Btn_Home()
        {
            print("TOuched");
            GameManager.LoadScene(SceneValid.MAIN_MENU);
        }
    }
}
