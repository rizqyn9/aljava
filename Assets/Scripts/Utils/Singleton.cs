using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// Temporary turn off
    /// </summary>
    //public abstract bool isDestroyed();

    [Header("Singleton Properties")]
    public bool isDDOL = false;

    private static T _instance;
    public static T Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
        }

        if (isDDOL)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
