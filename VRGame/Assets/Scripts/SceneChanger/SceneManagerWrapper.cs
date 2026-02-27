
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// Wrapper class for unity's SceneManager. Made so that calls to sceneManager can be mocked
/// out for unit testign
/// </summary>
public class SceneManagerWrapper
{

    /// <summary>
    /// Calls loadSceneAsync from unity's SceneManager with the specified scene-Id
    /// </summary>
    /// <param name="sceneKey">A scene Id from SceneEnum</param>
    /// <returns>An asyncOperationWrapper, holds the AsyncOperation of loading the new scene </returns>
    /// <remarks>
    /// Preconditions
    /// - sceneKey must exist in SceneEnum
    /// Posconditions
    /// -  All effects caused by calling LoadSceneAsync with the specified key
    public IAsyncOperationWrapper LoadSceneAsync(int sceneKey)
    {
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync(sceneKey);
        return  new AsyncOperationWrapper(loadingScene);
    }

    
}