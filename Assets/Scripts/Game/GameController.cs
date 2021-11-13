using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : Singleton<GameController>
    {
        [Header("Properties")]
        [SerializeField] GameProperties gameProperties;

        [Header("Debug")]
        public LevelBase levelBase;

        public static GameProperties GameProperties => Instance.gameProperties;

        #region Game State
        public static event Action OnGameStateChanged;
        [SerializeField] GameState _gameState = GameState.NULL;    // On first init gameState have null value which to prevent crash on scene level changes
        public static GameState GameState
        {
            get => Instance._gameState;
            set
            {
                print($"<color=green> Game State Changed {value} </color>");
                Instance._gameState = value;
                OnGameStateChanged?.Invoke();
            }
        }
        #endregion

        public void init(LevelBase _levelbase)
        {
            levelBase = _levelbase;

            StartCoroutine(IStartGame());
            
        }

        IEnumerator IStartGame()
        {
            GameState = GameState.INIT;
            yield return new WaitForSeconds(gameProperties.delayStart);

            GameState = GameState.BEFORE_START;
            yield return 1;
        }

        public void Btn_Home()
        {
            print("TOuched");
            GameManager.LoadScene(SceneValid.MAIN_MENU);
        }
    }
}
