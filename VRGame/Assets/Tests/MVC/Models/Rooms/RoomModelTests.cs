using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Unit tests for RoomModel class.
/// </summary>
public class RoomModelTests
{
    /// <summary>
    /// Test the initialization of RoomModel.
    /// </summary>
    [Test]
    public void Instantiation()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // confirm that roomModel is not null
        Assert.NotNull(roomModel, $"roomModel cannot be null. Got {roomModel}");

        // initialize the component, no errors should occur
        roomModel.Init();

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test getting the room id of RoomModel.
    /// </summary>
    [Test]
    public void GetRoomId()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify id values
        Assert.NotNull(roomModel.Id, $"roomModel.Id cannot be null. Got {roomModel.Id}");
        Assert.GreaterOrEqual(0, roomModel.Id, $"roomModel.Id can only be equal or greater than 0. Got {roomModel.Id}");

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test getting the room name of RoomModel.
    /// </summary>
    [Test]
    public void GetRoomName()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify name values
        Assert.NotNull(roomModel.Name, $"roomModel.Name cannot be null. Got {roomModel.Name}");
    
        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test getting the state of MinigameCompleted of RoomModel.
    /// </summary>
    [Test]
    public void GetMinigameCompleted()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify minigameComplete state
        Assert.IsFalse(roomModel.MinigameCompleted, $"roomModel.MinigameCompleted should be initialized to false. Got {roomModel.MinigameCompleted}");

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test setting the state of MinigameCompleted of RoomModel.
    /// </summary>
    [Test]
    public void SetMinigameComplete()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify minigameComplete state
        roomModel.MinigameCompleted = true;
        Assert.IsTrue(roomModel.MinigameCompleted, $"roomModel.MinigameCompleted was expected to be true. Got {roomModel.MinigameCompleted}");

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test getting the state of EducationalDialogueCompleted of RoomModel.
    /// </summary>
    [Test]
    public void GetEducationalDialogueCompleted()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify educationalDialgoueCompoleted state
        Assert.IsFalse(roomModel.EducationalDialogueCompleted, $"roomModel.EducationalDialogueCompleted should be initialized to false. Got {roomModel.EducationalDialogueCompleted}");

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test setting the state of EducationalDialogueCompleted of RoomModel.
    /// </summary>
    [Test]
    public void SetEducationalDialogueCompleted()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify educationalDialgoueCompoleted state
        roomModel.EducationalDialogueCompleted = true;
        Assert.IsTrue(roomModel.EducationalDialogueCompleted, $"roomModel.EducationalDialogueCompleted was expected to be true. Got {roomModel.EducationalDialogueCompleted}");

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling IsComplete when both minigameCompleted and 
    /// educationalDialgoueCompleted are false.
    /// </summary>
    [Test]
    public void IsCompleteFalse()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify the value returned from IsComplete()
        var result = roomModel.IsComplete();
        Assert.IsFalse(result, $"roomModel.IsCompelete was expected to return false, Got {result}");

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling IsComplete when both minigameCompleted and 
    /// educationalDialgoueCompleted are true.
    /// </summary>
    [Test]
    public void IsCompleteTrue()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify the value returned from IsComplete()
        roomModel.MinigameCompleted = true;
        roomModel.EducationalDialogueCompleted = true;
        var result = roomModel.IsComplete();
        Assert.IsTrue(result, $"roomModel.IsCompelete was expected to return true, Got {result}");

        // free up memory
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling IsComplete() when either minigameCompleted or
    /// educationalDialogueCompleted is true
    /// </summary>
    [Test]
    public void IsCompleteEdgeCases()
    {
        // test setup
        GameObject go = new GameObject();
        IRoomModel roomModel = null;

        // initialize the component, no errors should occur
        roomModel.Init();

        // verify the value returned from IsComplete() when minigameCompleted is true
        roomModel.MinigameCompleted = true;
        var result = roomModel.IsComplete();
        Assert.IsTrue(result, $"roomModel.IsCompelete was expected to return false, Got {result}");

        // verify the value returned from IsComplete() when minigameCompleted is true
        roomModel.MinigameCompleted = false;
        roomModel.EducationalDialogueCompleted = true;
        result = roomModel.IsComplete();
        Assert.IsTrue(result, $"roomModel.IsCompelete was expected to return false, Got {result}");

        // free up memory
        Object.DestroyImmediate(go);
    }

    /*
    Not needed because Model Layer does not interact with Controller and View Layer.
    [UnityTest]
    public IEnumerator PlayModeTest()
    {
        yield return null;
    }
    */
}