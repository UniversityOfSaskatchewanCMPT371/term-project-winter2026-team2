using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;
using System.Reflection;
using System.IO;

public class ViewTemplatePlayModeTest
{
    GameObject preloadPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/ScriptTemplates/MVCPrefab.prefab");

    [UnityTest]
    public IEnumerator Test_Initialize()
    {
        // Create GameObject and attach MVC components
        GameObject go = Object.Instantiate(preloadPrefab);

        LogAssert.Expect(LogType.Assert, "Field ModelRef cannot be null");

        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_OnExampleUpdate()
    {
        // Create GameObject and attach MVC components
        GameObject go = Object.Instantiate(preloadPrefab);
        ViewTemplate view = go.GetComponent<ViewTemplate>();

        // We don't need model layer to test this

        LogAssert.Expect(LogType.Assert, "Field ModelRef cannot be null");
        LogAssert.Expect(LogType.Warning, "Reference to the Model layer is missing");

        // Add an event listener to verify event fires
        bool EventInvoked = false;
        view.OnExampleEvent.AddListener((int amount) =>
        {
            EventInvoked = true;
        });

        // Skip one frame to allow Awake() and Start() to fire

        yield return null;

        Assert.IsTrue(EventInvoked, "Expected event to fire, but did not");
    }
}
