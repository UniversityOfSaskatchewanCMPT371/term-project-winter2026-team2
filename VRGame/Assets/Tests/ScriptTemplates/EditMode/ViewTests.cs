using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BaseViewWithoutViewClass : MonoBehaviour
{
    
}

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

    // setup the view component
    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        view = go.AddComponent<BaseView>();
    }

    // clean up object
    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(go);
    }

    // mock the controller
    public void MockControllerComponent()
    {
        controller = Substitute.For<IController>();
        view.ControllerMock = controller;
    }

    // Test with all valid fields
    [Test]
    public void Instantiation()
    {
        Assert.NotNull(view, "'viewInstance' field cannot be null");

        MockControllerComponent();

        view.CheckControllerRef();
    }

    // Test where the controller field is missing
    [Test]
    public void InstantiationWithMissingControllerRefs()
    {
        Assert.NotNull(view, "'viewInstance' field cannot be null");

        LogAssert.Expect(LogType.Error, "'controllerInstance' field is null.");
        Assert.Throws<AssertionException>(() => view.CheckControllerRef(), "'controllerInstance' field cannot be null.");
    }

    // test setting the value of 'controllerInstance' to null
    [Test]
    public void SetControllerFieldToNull()
    {
        Assert.NotNull(view, "'viewInstance' field cannot be null");

        LogAssert.Expect(LogType.Error, "'value' passed to set 'controllerInstance' is null.");

        Assert.Throws<AssertionException>(() => view.ControllerMock = null, "Expected exception to be thrown when setting field 'controllerInstance' to null.");
    }

    // test setting the value of 'controllerInstance' automatically
    [Test]
    public void AutomaticallySetControllerField()
    {
        Assert.NotNull(view, "'viewInstance' field cannot be null");

        LogAssert.Expect(LogType.Warning, "'inspectorWindowController' value was not set in inspector.");
        go.AddComponent<BaseController>();

        Assert.DoesNotThrow(() => view.CheckControllerRef(), "Expected exception to be thrown when 'controllerInstance' field is null.");
    }
}