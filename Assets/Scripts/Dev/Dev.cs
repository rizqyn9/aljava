using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Aljava;
using Aljava.Game;

public class Dev : Singleton<Dev>
{
    [Header("Properties")]
    public bool devMode = true;
    public GameObject gameManagerPrefab;
    public GameObject resourcePrefab;
    public bool useLevelTest = true;
    public LevelBase levelTest;
    public bool useCustomUserData = true;
    public UserData customUserData;

    private void Start()
    {
        if (!FindObjectOfType<GameManager>()) Instantiate(gameManagerPrefab);
        if (SceneManager.GetActiveScene().name == SceneValid.GAME)
        {
            StartCoroutine(IStartGame());
        }
    }

    IEnumerator IStartGame()
    {
        yield return new WaitUntil(() => GameManager.Instance.isResourceManagerReady);
        if(levelTest.isTutorialLevel)
            GameController.Instance.initTutorial(levelTest, levelTest.tutorialScript);
        else
            GameController.Instance.init(levelTest);
        yield break;
    }
}
