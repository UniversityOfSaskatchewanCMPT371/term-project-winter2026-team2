using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BaseControllerWithoutControllerClass : MonoBehaviour
{
    
}


public class BaseController : Controller<IModel, IView>
{
    public override void Init()
    {

    }
}

public class ControllerTests
{
    private GameObject go;
    private BaseController controller;
    private IView view;
    private IModel model;

    // setup controller component
    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        controller = go.AddComponent<BaseController>();
    }

    // clean up object
    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(go);
    }

    // mock the view
    public void MockViewComponent()
    {
        view = Substitute.For<IView>();
        controller.ViewMock = view;
    }

    // mock the model
    public void MockModelComponent()
    {
        model = Substitute.For<IModel>();
        controller.ModelMock = model;
    }

    // test the instantiation of controller component with no missing fields
    [Test]
    public void Instantiation()
    {
        Assert.NotNull(controller, "'controllerInstance' field cannot be null");

        MockViewComponent();
        MockModelComponent();

        controller.CheckModelRef();
        controller.CheckViewRef();
    }

    // test the instantiation of controller component with 'modelInstance' missing
    [Test]
    public void InstantiationWithMissingModelRefs()
    {
        Assert.NotNull(controller, "'controllerInstance' field cannot be null");

        LogAssert.Expect(LogType.Error, "'modelInstance' field is null.");

        MockViewComponent();

        Assert.Throws<AssertionException>(() => controller.CheckModelRef(), "'modelInstance' field cannot be null.");
    }

    // test the instantiation of controller component with 'viewInstance' missing
    [Test]
    public void InstantiationWithMissingViewRefs()
    {
        Assert.NotNull(controller, "'controllerInstance' field cannot be null");

        LogAssert.Expect(LogType.Error, "'viewInstance' field is null.");

        MockModelComponent();

        Assert.Throws<AssertionException>(() => controller.CheckViewRef(), "'viewInstance' field cannot be null.");
    }

    // test setting the value of 'modelInstance' to null
    [Test]
    public void SetModelFieldToNull()
    {
        Assert.NotNull(controller, "'controllerInstance' field cannot be null");

        LogAssert.Expect(LogType.Error, "'value' passed to set 'modelInstance' is null.");

        Assert.Throws<AssertionException>(() => controller.ModelMock = null, "Expected exception to be thrown when setting field 'modelInstance' to null.");
    }

    // test setting the value of 'viewInstance' to null
    [Test]
    public void SetViewFieldToNull()
    {
        Assert.NotNull(controller, "'controllerInstance' field cannot be null");

        LogAssert.Expect(LogType.Error, "'value' passed to set 'viewInstance' is null.");

        Assert.Throws<AssertionException>(() => controller.ViewMock = null, "Expected exception to be thrown when setting field 'viewInstance' to null.");
    }

    // test setting the value of 'modelInstance' automatically
    [Test]
    public void AutomaticallySetModelField()
    {
        Assert.NotNull(controller, "'controllerInstance' field cannot be null");

        LogAssert.Expect(LogType.Warning, "'inspectorWindowModel' value was not set in inspector.");

        go.AddComponent<BaseModel>();

        Assert.DoesNotThrow(() => controller.CheckModelRef(), "Expected no exception to be thrown when 'modelInstance' is automatically set.");
    }

    // test setting the value of 'viewInstance' automatically
    [Test]
    public void AutomaticallySetViewField()
    {
        Assert.NotNull(controller, "'controllerInstance' field cannot be null");

        LogAssert.Expect(LogType.Warning, "'inspectorWindowView' value was not set in inspector.");

        go.AddComponent<BaseView>();

        Assert.DoesNotThrow(() => controller.CheckViewRef(), "Expected no exception to be thrown when 'viewInstance' is automatically set.");
    }
}