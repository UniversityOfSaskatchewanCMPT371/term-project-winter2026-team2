using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// The enum values map directly to build index numbers.
public enum Scenes
{
    Hub = 0,
    Room1 = 1,
}

/// <summary>
/// A persistent singleton responsible for changing scenes safely.
/// </summary>
public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance { get; set; }          // Singleton instance of the SceneChanger
    private static bool loadDebounce = false;                   // Prevents multiple scene loads from being triggered at once

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);
    }

    private void resetDebounce()
    {
        loadDebounce = false;
    }

    /// <summary>
    /// Loads a scene by enum value.
    /// </summary>
    /// <returns>The AsyncOperation or null if a load request is already in progress.</returns>
    /// <param name="sceneIdx">The scene to load based on the Scenes enum.</param>
    public AsyncOperation LoadScene(Scenes sceneIdx)
    {
        if (loadDebounce) return null;
        loadDebounce = true;

        // Load the scene using the sceneIdx enum

        AsyncOperation loadingScene = SceneManager.LoadSceneAsync((int)sceneIdx);

        // Reset the debounce once the scene finishes loading

        loadingScene.completed += (o) => resetDebounce();

        return loadingScene;
    }
}