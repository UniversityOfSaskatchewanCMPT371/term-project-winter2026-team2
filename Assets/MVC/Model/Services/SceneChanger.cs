using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes {
    Hub = 0,
    Room1 = 1,
}

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance { get; set; }
    private static bool loadDebounce = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);
    }

    public void LoadScene(Scenes sceneIdx)
    {
        if (loadDebounce) return;
        loadDebounce = true;

        SceneManager.LoadScene((int)sceneIdx);

        loadDebounce = false;
    }
}
