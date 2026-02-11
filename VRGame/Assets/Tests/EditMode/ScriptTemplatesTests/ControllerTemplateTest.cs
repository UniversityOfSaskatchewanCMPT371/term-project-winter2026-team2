using NUnit.Framework;
using UnityEngine;
using System.Reflection;

public class EMControllerTemplateTest
{
    [Test]
    public void Test01_Instantiate()
    {
        GameObject go = new GameObject();
        ControllerTemplate controller = go.AddComponent<ControllerTemplate>();
    }

    [Test]
    public void Test02_Count()
    {
        GameObject go = new GameObject();
        ControllerTemplate controller = go.AddComponent<ControllerTemplate>();
        ModelTemplate model = go.AddComponent<ModelTemplate>();
        
        typeof(ControllerTemplate)
            .GetField("ModelRef", BindingFlags.NonPublic | BindingFlags.Instance)
            .SetValue(controller, model);

        // Test to see if no exception is thrown when Count() is called with Model reference
        Assert.DoesNotThrow(() => controller.Count(), "Expected no exception to be thrown, but one was thrown on valid Model reference");
    }
}
