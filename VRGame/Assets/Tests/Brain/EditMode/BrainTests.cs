using NUnit.Framework;
using UnityEngine;

public class BrainModelTests
{
    private GameObject gameObject;
    private BrainModel brainModel;
    private Animator animator;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject("Brain Model Test Object");
        brainModel = gameObject.AddComponent<BrainModel>();
        animator = gameObject.AddComponent<Animator>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void InitializeTest()
    {
        brainModel.Init();
        Assert.IsNotNull(animator, "Brain model instance failed to initialize");
    }

    [Test]
    public void PauseStateTest()
    {
        brainModel.Init();
        brainModel.pause();
        Assert.IsNotNull(animator, "Brain model instance failed to initialize on pause() test");
        Assert.AreEqual(animator.speed, 0f, "Animator speed failed to set to 0 on pause() test");
    }
}