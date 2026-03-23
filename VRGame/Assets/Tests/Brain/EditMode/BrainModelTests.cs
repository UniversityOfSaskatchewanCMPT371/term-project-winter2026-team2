using FsCheck;
using FsCheck.Fluent;
using NUnit.Framework;
using UnityEngine;

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

    /// <summary>
    /// Property based test to check pause() sets the animation speed to 0f
    /// </summary>
    [Test]
    public void PauseAlwaysZeroTest()
    {
        Prop.ForAll<float>(speed =>
        {
            animator.speed = speed;
            brainModel.pause();
            return animator.speed == 0f;
        }).QuickCheckThrowOnFailure();
    }

    /// <summary>
    /// Property based test to check resume() sets the animation speed to 1.0f
    /// </summary>
    [Test]
    public void ResumeAlwaysOneTest()
    {
        Prop.ForAll<float>(speed =>
        {
            animator.speed = speed;
            brainModel.resume();
            return animator.speed == 1.0f;
        }).QuickCheckThrowOnFailure();
    }


    /// <summary>
    /// Property based test to check pause() and resume() are idempotent 
    /// (i.e. calling pause() multiple times keeps speed at 0f, calling resume() multiple times keeps speed at 1.0f)
    /// </summary>
    [Test]
    public void PauseResumeIdempotentTest()
    {
        Prop.ForAll<float>(speed =>
        {
            animator.speed = speed;
            brainModel.pause();
            brainModel.pause();
            return animator.speed == 0.0f;
        }).QuickCheckThrowOnFailure();

        Prop.ForAll<float>(speed =>
        {
            animator.speed = speed;
            brainModel.resume();
            brainModel.resume();
            return animator.speed == 1.0f;
        }).QuickCheckThrowOnFailure();
    }

    /// <summary>
    /// Property based test to check that calling pause() is the same as calling resume() followed by pause()
    /// </summary>
    [Test]
    public void PauseEqualsResumePauseTest()
    {
        Prop.ForAll<float>(speed =>
        {
            animator.speed = speed;
            brainModel.resume();
            brainModel.pause();
            float speed1 = animator.speed;

            animator.speed = speed;
            brainModel.pause();
            float speed2 = animator.speed;

            return speed2 == speed1;
        }).QuickCheckThrowOnFailure();
    }

    /// <summary>
    /// Property based test to check that calling resume() is the same as calling pause() followed by resume()
    /// </summary>
    [Test]
    public void ResumeEqualsPauseResumeTest()
    {
        Prop.ForAll<float>(speed =>
        {
            animator.speed = speed;
            brainModel.pause();
            brainModel.resume();
            float speed1 = animator.speed;

            animator.speed = speed;
            brainModel.resume();
            float speed2 = animator.speed;

            return speed2 == speed1;
        }).QuickCheckThrowOnFailure();
    }
}