using NUnit.Framework;
using NSubstitute;
using ObjectMatchGame;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;

/// <summary>
/// Play Mode unit tests for ObjectMatchGameController component using Reflection for dependency injection.
/// </summary>
public class ObjectMatchGameControllerPlayModeTests
{
    /// <summary>
    /// Helper to inject mocks into the protected/private fields of the base Controller.
    /// </summary>
    private void SetPrivateField(object target, string fieldName, object value)
    {
        System.Type type = target.GetType();
        FieldInfo field = null;

        while (type != null && field == null)
        {
            field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            type = type.BaseType;
        }

        if (field != null) field.SetValue(target, value);
    }

    [UnityTest]
    public IEnumerator Instantiation()
    {
        GameObject go = new GameObject();
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();

        IObjectMatchGameModel model = Substitute.For<IObjectMatchGameModel>();
        IObjectMatchGameView view = Substitute.For<IObjectMatchGameView>();

        SetPrivateField(controller, "modelInstance", model);
        SetPrivateField(controller, "viewInstance", view);

        Assert.NotNull(controller);
        yield return null;
        Object.Destroy(go);
    }

    [UnityTest]
    public IEnumerator PotentialGuess()
    {
        GameObject go = new GameObject();
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();

        IObjectMatchGameModel model = Substitute.For<IObjectMatchGameModel>();
        IObjectMatchGameView view = Substitute.For<IObjectMatchGameView>();

        SetPrivateField(controller, "modelInstance", model);
        SetPrivateField(controller, "viewInstance", view);
        controller.Init();

        string testGuessID = "TestObject";
        controller.PotentialGuess(testGuessID);

        model.Received(1).PotentialGuess(testGuessID);

        yield return null;
        Object.Destroy(go);
    }

    [UnityTest]
    public IEnumerator InitializeLevel()
    {
        GameObject go = new GameObject();
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();

        IObjectMatchGameModel model = Substitute.For<IObjectMatchGameModel>();
        IObjectMatchGameView view = Substitute.For<IObjectMatchGameView>();

        string[] testObjectIDs = { "Object1", "Object2" };
        model.GetActiveObjectIDs().Returns(testObjectIDs);
        controller.model = model;
        controller.view = view;
        
        controller.Init();

        controller.InitializeLevel();

        model.Received(1).InitializeLevel();
        view.Received(1).ShowObjects(testObjectIDs);

        yield return null;
        Object.Destroy(go);
    }

    
    [UnityTest]
    public IEnumerator RestartGame_ThrowsNotImplementedException()
    {
        GameObject go = new GameObject();
        ObjectMatchGameController controller = go.AddComponent<ObjectMatchGameController>();

        // Minimal setup for a throw test
        SetPrivateField(controller, "modelInstance", Substitute.For<IObjectMatchGameModel>());
        SetPrivateField(controller, "viewInstance", Substitute.For<IObjectMatchGameView>());
        controller.Init();

        Assert.Throws<System.NotImplementedException>(() => controller.RestartGame());

        yield return null;
        Object.Destroy(go);
    }
}