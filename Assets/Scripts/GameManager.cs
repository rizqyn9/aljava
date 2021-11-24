using UnityEngine;
using UnityEngine.SceneManagement;
using Aljava.MainMenu;
using Aljava.Level;
using Aljava.Game;
using System.Collections;

namespace Aljava
{
    public static class SceneValid
    {
        public const string MAIN_MENU = "MainMenu";
        public const string LEVEL_STAGE = "LevelStage";
        public const string GAME = "Game";
    }

    public class GameManager : Singleton<GameManager>
    {
        [Header("Properties")]
        public SaveData saveData;
        public GameObject resourceManagerPrefab;

        [Header("Debug")]
        public UserData userData;
        public bool isResourceManagerReady = false;

        private void Start()
        {
            saveData.init();
            userData = saveData.userData;

            // Ensure that resource manager instance on hierarchy
            if (!FindObjectOfType<ResourceManager>())
            {
                Instantiate(resourceManagerPrefab);
            }
        }

        delegate void SuccessLoadedResources();
        IEnumerator ICheckResource(SuccessLoadedResources callback)
        {
            yield return new WaitUntil(() => Instance.isResourceManagerReady);
            callback();
            yield break;
        }

        #region Scene Handler
        private void OnEnable() => SceneManager.sceneLoaded += handleSceneLoaded;
        private void OnDisable() => SceneManager.sceneLoaded -= handleSceneLoaded;

        [SerializeField] string sceneNow;
        [SerializeField] bool isInit;
        public void handleSceneLoaded(Scene _scene, LoadSceneMode arg1)
        {
            sceneNow = _scene.name;
            if (sceneNow == SceneValid.GAME)
            {
                StartCoroutine(
                    ICheckResource(() =>
                    {
                        if(levelBase.isTutorialLevel)
                            GameController.Instance.initTutorial(levelBase, levelBase.tutorialScript);
                        else 
                            GameController.Instance.init(levelBase);
                    })
                );
            } else if (sceneNow == SceneValid.MAIN_MENU)
            {
                StartCoroutine(ICheckResource(MainMenuController.Instance.init));
            } else if(sceneNow == SceneValid.LEVEL_STAGE)
            {
                StartCoroutine(ICheckResource(LevelStageController.Instance.init));
            }
        }

        public static void LoadScene(string _target, LoadSceneMode _loadSceneMode = LoadSceneMode.Single)
        {
            SceneManager.LoadScene(_target, _loadSceneMode);
        }

        [SerializeField] LevelBase levelBase;
        public static void LoadLevel(LevelBase _levelBase)
        {
            Instance.levelBase = _levelBase;
            LoadScene(SceneValid.GAME);
        }

        public static void UnLoadScene(string _target)
        {
            SceneManager.UnloadSceneAsync(_target);
        }

        public static void LoadNextLevel(LevelBase levelNow)
        {
            LoadLevel(ResourceManager.ListLevels.Find(val => val.level == (levelNow.level + 1)));
        }

        public static void LoadMainMenu() => LoadScene(SceneValid.MAIN_MENU);
        #endregion
    }
}
