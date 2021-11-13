using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
