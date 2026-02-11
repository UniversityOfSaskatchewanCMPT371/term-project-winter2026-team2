using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class ControllerTemplateTest
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

        // Test to see if an exception will be thrown when Count() is called without Model reference.
        Assert.Throws<MissingReferenceException>(() => controller.Count(), "Expected exception to be thrown, but none was thrown on missing Model reference.");
        
        SerializedObject so = new SerializedObject(controller);
        so.FindProperty("ModelRef").objectReferenceValue = model;
        so.ApplyModifiedProperties();

        // Test to see if no exception is thrown when Count() is called with Model reference.
        Assert.DoesNotThrow(() => controller.Count(), "Expected no exception to be thrown, but one was thrown on valid Model reference.");
    }
}
