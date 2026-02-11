using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;

public class PMViewTemplateTest
{
    GameObject preloadPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/ScriptTemplates/MVCPrefab.prefab");

    [UnityTest]
    public IEnumerator Test01_Initialize()
    {
        // Create GameObject and attach MVC components
        GameObject go = Object.Instantiate(preloadPrefab);

        // Destroy components we don't need
        ModelTemplate model = go.GetComponent<ModelTemplate>();
        Object.DestroyImmediate(model);

        // Destroy components we don't need
        ControllerTemplate controller = go.GetComponent<ControllerTemplate>();
        Object.DestroyImmediate(controller);

        LogAssert.Expect(LogType.Assert, "Field ControllerRef cannot be null");

        yield return null;
    }

    [UnityTest]
    public IEnumerator Test02_OnExampleUpdate()
    {
        // Create GameObject and attach MVC components
        GameObject go = Object.Instantiate(preloadPrefab);
        ViewTemplate view = go.GetComponent<ViewTemplate>();

        // Destroy components we don't need
        ModelTemplate model = go.GetComponent<ModelTemplate>();
        Object.DestroyImmediate(model);

        // Expect assertions
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

        // Test to see if ExampleEvent will fire when invoked by the controller layer.
        Assert.IsTrue(EventInvoked, "Expected event to fire, but did not");
    }
}
