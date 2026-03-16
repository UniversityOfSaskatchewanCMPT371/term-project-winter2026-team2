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
        brainModel.Init();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void InitializeTest()
    {
        Assert.IsNotNull(animator, "Brain model instance failed to initialize");
    }

    [Test]
    public void PauseStateTest()
    {
        Assert.IsNotNull(animator, "Brain model instance is not initialized on pause() test");
        brainModel.pause();
        Assert.AreEqual(animator.speed, 0f, "Animator speed failed to set to 0 on pause() test");
    }

    [Test]
    public void ResumeStateTest()
    {
        Assert.IsNotNull(animator, "Brain model instance is not initialized on resume() test");
        brainModel.pause();
        brainModel.resume();
        Assert.AreEqual(animator.speed, 1.0f, "Animator speed failed to set to 1.0 on pause() test");
    }
}