using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class PMControllerTemplateTest
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

        ViewTemplate view = go.GetComponent<ViewTemplate>();
        Object.DestroyImmediate(view);

        LogAssert.Expect(LogType.Assert, "Field ModelRef cannot be null");
        LogAssert.Expect(LogType.Assert, "Field ViewRef cannot be null");

        yield return null;
    }

    [UnityTest]
    public IEnumerator Test02_Count()
    {
        // Create GameObject and attach MVC components
        GameObject go = Object.Instantiate(preloadPrefab);
        ControllerTemplate controller = go.GetComponent<ControllerTemplate>();
        ViewTemplate view = go.GetComponent<ViewTemplate>();
        ModelTemplate model = go.GetComponent<ModelTemplate>();

        yield return null; // allow Start() to run

        bool invoked = false;
        int lastValue = 0;

        view.OnExampleEvent.AddListener((value) =>
        {
            invoked = true;
            lastValue = value;
        });

        int before = model.GetExample();

        controller.Count();

        // Test if Count() increments the Model and updates the View
        Assert.AreEqual(before + 1, model.GetExample());

        // Test if update is reflected on View layer
        Assert.IsTrue(invoked);

        // Final update from finally block
        Assert.AreEqual(-1, lastValue);

        yield return null;

    }
}
