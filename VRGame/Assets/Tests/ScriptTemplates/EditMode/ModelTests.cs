using NUnit.Framework;
using UnityEngine;

public class BaseModelWithoutModelClass : MonoBehaviour
{
    
}

public class BaseModel : Model
{
    public override void Init()
    {

    }
}

public class ModelTests
{
    private GameObject go;
    private BaseModel model;

    // setup model component
    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        model = go.AddComponent<BaseModel>();
    }

    // clean up object
    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(go);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        Assert.NotNull(model, "'modelInstance' field cannot be null");

        Assert.DoesNotThrow(() => model.Start(), "Expected no exception to be thrown, but one was thrown.");
    }
}