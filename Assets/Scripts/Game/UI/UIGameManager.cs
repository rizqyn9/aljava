using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Game
{
    public class UIGameManager : Singleton<UIGameManager>, IGameState
    {
        [Header("Properties")]
        [SerializeField] UIMachineManager machineManager;
        [SerializeField] UIBubblesManager bubblesManager;
        [SerializeField] GameObject TopBar;
        [SerializeField] TMP_Text count;
        [SerializeField] Button pauseBtn;

        public static UIMachineManager MachineManager => Instance.machineManager;
        public static UIBubblesManager BubblesManager => Instance.bubblesManager;

        private void Start()
        {
        }

        #region Game State Handler
        void OnEnable() => GameController.OnGameStateChanged += GameStateHandler;
        void OnDisable() => GameController.OnGameStateChanged -= GameStateHandler;

        public GameState gameState => GameController.GameState;
        public void GameStateHandler() => GameStateController.UpdateGameState(this);
        public GameObject GetGameObject() => gameObject;

        public void OnGameInit()
        {
            setUpComponent();
        }

        public void OnGameBeforeStart() { }

        public void OnGameStart()
        {
            LeanTween.moveY(TopBar.GetComponent<RectTransform>(), 0, .5f).setEaseInBounce().setOnComplete(() => StartCoroutine(ICountDown()));
            LeanTween.moveX(pauseBtn.GetComponent<RectTransform>(), -20, .5f).setEaseInBounce();
        }
        public void OnGameClearance() { }
        public void OnGameFinish() { }
        public void OnGameIddle() { }
        public void OnGamePause() { }
        #endregion

        private void setUpComponent()
        {
            countDown = GameController.LevelBase.gameDuration;
        }

        #region COUNTDOWN CONTROLLER
        public bool timerIsRunning = false;
        [SerializeField] int _countDown = 0;
        int countDown
        {
            get => _countDown;
            set
            {
                _countDown = value;
                count.text = _countDown.ToString();
            }
        }

        IEnumerator ICountDown()
        {
            while (countDown > 0)
            {
                timerIsRunning = true;
                yield return new WaitForSeconds(1);
                countDown -= 1;
            }
            timerIsRunning = false;
            if (countDown <= 0) GameController.Instance.handleGameTimeOut();
            yield break;
        }

        #endregion

        public void init()
        {

        }
    }
}
