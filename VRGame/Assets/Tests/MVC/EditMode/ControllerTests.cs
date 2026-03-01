using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BaseController : Controller<IModel,IView>
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

    [SetUp]
    public void Setup()
    {
        // setup component
        go = new GameObject();
        controller = go.AddComponent<BaseController>();
    }

    [TearDown]
    public void TearDown()
    {
        // clean up objects
        UnityEngine.Object.DestroyImmediate(go);
    }

    public void MockViewComponent()
    {
        // mock the view
        view = Substitute.For<IView>();
        controller.View = view;
    }

    public void MockModelComponent()
    {
        // mock the model
        model = Substitute.For<IModel>();
        controller.Model = model;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        Assert.NotNull(controller, "'controller' field cannot be null");

        MockViewComponent();
        MockModelComponent();

        controller.Start();
    }

    [Test]
    public void InstantiationWithMissingModelRefs()
    {
        Assert.NotNull(controller, "'controller' field cannot be null");

        LogAssert.Expect(LogType.Error, "'model' field is null.");
        LogAssert.Expect(LogType.Assert, "'model' field cannot be null.");

        MockViewComponent();

        controller.Start();
    }

    [Test]
    public void InstantiationWithMissingViewRefs()
    {
        Assert.NotNull(controller, "'controller' field cannot be null");

        LogAssert.Expect(LogType.Error, "'view' field is null.");
        LogAssert.Expect(LogType.Assert, "'view' field cannot be null.");

        MockModelComponent();

        controller.Start();
    }
}