using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    public class UIGameManager : Singleton<UIGameManager>, IGameState
    {
        [Header("Properties")]
        [SerializeField] UIMachineManager machineManager;
        [SerializeField] UIBubblesManager bubblesManager;
        [SerializeField] HealthManager healtManager;
        [SerializeField] UI_Win win;
        [SerializeField] UI_Lose lose;
        [SerializeField] GameObject TopBar;
        [SerializeField] TMP_Text count;
        [SerializeField] Button pauseBtn;
        [SerializeField] GameObject noClickArea;
        [SerializeField] List<CanvasGroup> canvasGroups;

        public static UIMachineManager MachineManager => Instance.machineManager;
        public static UIBubblesManager BubblesManager => Instance.bubblesManager;
        public static HealthManager HealthManager => Instance.healtManager;
        public static UI_Lose Lose => Instance.lose;
        public static UI_Win Win => Instance.win;
        public static bool IsActiveUI => Instance.isActiveUI;

        [Header("Debug")]
        public bool isActiveUI = false;

        private void Start()
        {
            noClickSetActive(false);
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

        public void OnGameBeforeStart()
        {
            healtManager.init(3);
        }

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
            if (countDown <= 0) GameController.RulesController.handleGameTimeOut();
            yield break;
        }

        #endregion

        public void init()
        {

        }

        #region Handle NoClickArea
        public void noClickSetActive(bool active)
        {
            canvasGroups.ForEach(val => val.alpha = active ? 0 : 1);
            noClickArea.SetActive(active);
        }
        #endregion

        [SerializeField] bool isPaused = false;
        public void Btn_Pause()
        {
            isPaused = !isPaused;
            noClickSetActive(isPaused);

            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}
