using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

namespace Aljava.Game
{
    public class UIGameManager : Singleton<UIGameManager>, IGameState
    {
        [Header("Properties")]
        [SerializeField] UIMachineManager machineManager;
        [SerializeField] UIBubblesManager bubblesManager;
        [SerializeField] HealthManager healtManager;
        [SerializeField] UI_Recipe recipe;
        [SerializeField] UI_Win win;
        [SerializeField] UI_Lose lose;
        [SerializeField] TMP_Text count, topNavText;
        [SerializeField] Button pauseBtn;
        [SerializeField] GameObject noClickArea, pauseContainer, TopBar;
        [SerializeField] List<CanvasGroup> canvasGroups;
        [SerializeField] Converse converse;

        public static UIMachineManager MachineManager => Instance.machineManager;
        public static UIBubblesManager BubblesManager => Instance.bubblesManager;
        public static UI_Recipe Recipe => Instance.recipe;
        public static HealthManager HealthManager => Instance.healtManager;
        public static UI_Lose Lose => Instance.lose;
        public static UI_Win Win => Instance.win;
        public static Converse Converse => Instance.converse;
        public static bool IsActiveUI => Instance.isActiveUI;

        [Header("Debug")]
        public bool isActiveUI = false;
        GameMode gameMode => GameController.LevelBase.gameMode;

        private void Start()
        {
            noClickSetActive(false);
        }

        #region Top Nav Controller

        void OnStatsChanged() => setNav();

        void setNav()
        {
            if (gameMode == GameMode.ORDER) topNavText.text = $"Customer total {GameController.RulesController.buyerSuccessTotal.ToString()}";
        }
        #endregion

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
            GameController.RulesController.OnstatisticsChanged += OnStatsChanged;
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
            setNav();
        }

        private void OnDestroy() => GameController.RulesController.OnstatisticsChanged -= OnStatsChanged;

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

        #region Handle NoClickArea
        public void noClickSetActive(bool active)
        {
            isActiveUI = active;
            canvasGroups.ForEach(val => val.alpha = active ? 0 : 1);
            noClickArea.SetActive(active);
        }
        #endregion

        #region Button
        [SerializeField] bool isPaused = false;
        [SerializeField] float offsetY;
        public void Btn_Pause()
        {
            isPaused = !isPaused;
            noClickSetActive(isPaused);

            LeanTween.moveY(pauseContainer.GetComponent<RectTransform>(), isPaused ? 0 : offsetY, .3f)
                .setOnStart(() =>
                {
                    if (isPaused)
                    {
                        pauseContainer.SetActive(isPaused);
                        noClickSetActive(isPaused);
                    }
                })
                .setOnComplete(()=>
                {
                    if(!isPaused)
                    {
                        pauseContainer.SetActive(isPaused);
                        noClickSetActive(isPaused);
                    }
                }).setIgnoreTimeScale(true).setEaseInBack();

            Time.timeScale = isPaused ? 0 : 1;
        }

        public void Btn_MainMenu()
        {
            Time.timeScale = 1;
            GameManager.LoadMainMenu();
        }

        public void Btn_Restart()
        {
            Time.timeScale = 1;
            GameManager.LoadLevel(GameController.LevelBase);
        }
        #endregion
    }
}
