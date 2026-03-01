using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BaseView : View<IController>
{
    public override void Init()
    {
        
    }
}

public class ViewTests
{
    private GameObject go;
    private BaseView view;
    private IController controller;

    [SetUp]
    public void Setup()
    {
        // setup the component
        go = new GameObject();
        view = go.AddComponent<BaseView>();
    }

    [TearDown]
    public void TearDown()
    {
        // clean up objects
        UnityEngine.Object.DestroyImmediate(go);
    }

    public void MockControllerComponent()
    {
        // mock the controller
        controller = Substitute.For<IController>();
        view.Controller = controller;
    }

    // Test with all valid fields
    [Test]
    public void Instantiation()
    {
        Assert.NotNull(view, "'view' field cannot be null");

        MockControllerComponent();

        view.Start();
    }

    // Test where the controller field is missing
    [Test]
    public void InstantiationWithMissingControllerRefs()
    {
        Assert.NotNull(controller, "'controller' field cannot be null");

        LogAssert.Expect(LogType.Error, "'controller' field is null.");
        LogAssert.Expect(LogType.Assert, "'controller' field cannot be null.");

        view.Start();
    }
}