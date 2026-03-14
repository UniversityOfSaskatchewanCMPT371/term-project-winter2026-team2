
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;
using System;
using System.Runtime.CompilerServices;
using System.Reflection;

public class SceneChangerController_SceneManager
{

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator Init()
    {
        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        //SceneManagerWrapper sceneMW = new SceneManagerWrapper();
        //sceneC.SceneManagerWrapper = sceneMW;
        // Use yield to skip a frame.
        yield return null;
        // if no assertions triggered, passed
        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DebounceCheck()
    {
        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        //SceneManagerWrapper sceneMW = new SceneManagerWrapper();
        //sceneC.SceneManagerWrapper = sceneMW;
        // Use yield to skip a frame.
        yield return null;

        // loadDebounce should be false, allows scene to be loaded
        Assert.IsFalse(sceneC.LoadDebounce);

        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Check_SceneManagerWrapper()
    {
        // Use the Assert class to test conditions
        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();

        Assert.IsNotNull(sceneC.SceneManagerWrapper);

        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
        yield return null;
    }


    [UnityTest]
    public IEnumerator SingletonTest()
    {

        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        SceneManagerWrapper sceneMW = new SceneManagerWrapper();
        sceneC.SceneManagerWrapper = sceneMW;
        // Use yield to skip a frame.
        yield return null;

        SceneChangerController sceneC2 = go.AddComponent<SceneChangerController>();
        sceneC2.SceneManagerWrapper = sceneMW;
        // attempting to create another instance should fail

        // tell unity to ignore error log so test can pass
        LogAssert.Expect(LogType.Error, new Regex(".*"));
        try
        {
            sceneC2.Init();
            Assert.IsTrue(1 == 2);
        }
        catch
        {
        }
        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
        yield return null;
    }


    [UnityTest]
    public IEnumerator LoadScene()
    {

        LogAssert.Expect(LogType.Log, "SceneChangerController.LoadScene(): Valid start");
        LogAssert.Expect(LogType.Log, "SceneChangerController.LoadScene() success");
        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        // Use yield to skip a frame.
        yield return null;


        IAsyncOperationWrapper op = sceneC.LoadScene(7); //testscene


        // let async operation finish
        while (sceneC.LoadDebounce)
        {
            yield return null;
        }
        

        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
    }

    [UnityTest]
    public IEnumerator NonExistent_LoadScene()
    {
        GameObject go = new GameObject();
        SceneChangerController sceneC = go.AddComponent<SceneChangerController>();
        // Use yield to skip a frame.
        yield return null;

        LogAssert.Expect(LogType.Error, "Invalid sceneKey passed to LoadScene. Not in enum");
        try
        {
            // won't have negative scene ids ever
            sceneC.LoadScene(-1);
            Assert.Fail("Loading invalid sceneId should've triggered assertion");
        }
        catch { }

        sceneC.ResetInstance();
        UnityEngine.Object.DestroyImmediate(go);
        yield return null;
    }
}
