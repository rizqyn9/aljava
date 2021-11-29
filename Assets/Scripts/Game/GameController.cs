using System;
using System.Collections;
using UnityEngine;

namespace Aljava.Game
{
    public class GameController : Singleton<GameController>
    {
        [Header("Properties")]
        [SerializeField] GameProperties gameProperties;
        [SerializeField] OrderController orderController;
        [SerializeField] RulesController rulesController;

        [Header("Debug")]
        public LevelBase levelBase;
        public bool isTutorial = false;
        public Tutorial tutorial;

        // Accessor
        public static bool IsTutorial => Instance.isTutorial;
        public static LevelBase LevelBase => Instance.levelBase;
        public static GameProperties GameProperties => Instance.gameProperties;
        public static OrderController OrderController => Instance.orderController;
        public static RulesController RulesController => Instance.rulesController;

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

        public void initTutorial(LevelBase _levelBase, GameObject _tutorial)
        {
            levelBase = _levelBase;
            tutorial = Instantiate(_tutorial, transform).GetComponent<Tutorial>();

            tutorial.init();
        }

        public void init(LevelBase _levelbase)
        {
            levelBase = _levelbase;

            StartCoroutine(IStartGame());
        }

        IEnumerator IStartGame()
        {
            GameState = GameState.INIT;
            yield return new WaitForSeconds(gameProperties.delayStart/2);

            GameState = GameState.BEFORE_START;
            yield return new WaitForSeconds(gameProperties.delayStart);

            GameState = GameState.START;
        }

        public void handleGameEnd()
        {
            GameState = GameState.FINISH;
        }


        public void Btn_Home()
        {
            GameManager.LoadScene(SceneValid.MAIN_MENU);
        }
    }
}
