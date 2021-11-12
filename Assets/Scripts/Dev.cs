using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dev : Singleton<Dev>
{
    [Header("Properties")]
    public bool devMode = true;
    public GameObject gameManagerPrefab;
    public GameObject resourcePrefab;


    private void Start()
    {
        if (!FindObjectOfType<GameManager>())
        {
            Instantiate(gameManagerPrefab);
        }
        if (!FindObjectOfType<ResourceController>())
        {
            Instantiate(resourcePrefab);
        }
    }
}
