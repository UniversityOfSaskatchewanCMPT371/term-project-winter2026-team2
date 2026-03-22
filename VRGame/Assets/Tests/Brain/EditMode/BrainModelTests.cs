using NUnit.Framework;
using UnityEngine;
using FsCheck;

public class BrainModelTests
{
    /// <summary>
    /// Attributes necessary to test model component
    /// </summary>
    private GameObject gameObject;
    private BrainModel brainModel;
    private Animator animator;

    /// <summary>
    /// Initializes attributes and calls on Init() method to initialize Brain Model component
    /// </summary>
    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject("Brain Model Test Object");
        brainModel = gameObject.AddComponent<BrainModel>();
        animator = gameObject.AddComponent<Animator>();
        brainModel.Init();
    }

    /// <summary>
    /// Tests the animator built on Init()
    /// </summary>
    [Test]
    public void InitializeTest()
    {
        Assert.IsNotNull(animator, "Brain model instance failed to initialize");
        Assert.IsNotNull(brainModel, "brainModel failed to initialize on test");
    }

    /// <summary>
    /// Tests the pause state of the model component on pause()
    /// </summary>
    [Test]
    public void PauseStateTest()
    {
        Assert.IsNotNull(animator, "Brain model instance is not initialized on pause() test");
        brainModel.pause();
        Assert.AreEqual(animator.speed, 0f, "Animator speed failed to set to 0 on pause() test");
    }

    /// <summary>
    /// Tests the resume state of the model component after calling pause()
    /// </summary>
    [Test]
    public void ResumeStateTest()
    {
        Assert.IsNotNull(animator, "Brain model instance is not initialized on resume() test");
        brainModel.pause();
        brainModel.resume();
        Assert.AreEqual(animator.speed, 1.0f, "Animator speed failed to set to 1.0 on pause() test");
    }
}