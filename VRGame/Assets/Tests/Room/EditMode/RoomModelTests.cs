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
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();
        
        // confirm that roomModel is not null
        Assert.NotNull(roomModel, $"roomModel cannot be null. Got {roomModel}");

        // initialize
        roomModel.Init();

        roomModel.OnDestroy();
        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test getting the 'roomId' of RoomModel.
    /// </summary>
    [Test]
    public void GetRoomId()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();;

        // verify 'roomId' values
        Assert.NotNull(roomModel.Id, $"roomModel.Id cannot be null. Got '{roomModel.Id}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test getting the 'roomName' of RoomModel.
    /// </summary>
    [Test]
    public void GetRoomName()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();

        // verify name values
        Assert.IsNotNull(roomModel.Name, $"roomModel.Name cannot be Null. Got '{roomModel.Name}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test getting the state of 'MinigameCompleted' of RoomModel.
    /// </summary>
    [Test]
    public void GetMinigameCompleted()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();

        // verify 'MinigameComplete' state
        Assert.IsFalse(roomModel.MinigameCompleted, $"roomModel.MinigameCompleted should be initialized to false. Got '{roomModel.MinigameCompleted}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test setting the state of 'MinigameCompleted' of RoomModel.
    /// </summary>
    [Test]
    public void SetMinigameComplete()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();

        // verify 'MinigameComplete' state
        roomModel.MinigameCompleted = true;
        Assert.IsTrue(roomModel.MinigameCompleted, $"roomModel.MinigameCompleted was expected to be true. Got '{roomModel.MinigameCompleted}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test getting the state of 'EducationalDialogueCompleted' of RoomModel.
    /// </summary>
    [Test]
    public void GetEducationalDialogueCompleted()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();

        // verify 'EducationalDialgoueCompoleted' state
        Assert.IsFalse(roomModel.EducationalDialogueCompleted, $"roomModel.EducationalDialogueCompleted should be initialized to false. Got '{roomModel.EducationalDialogueCompleted}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test setting the state of 'EducationalDialogueCompleted' of RoomModel.
    /// </summary>
    [Test]
    public void SetEducationalDialogueCompleted()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();

        // verify educationalDialgoueCompoleted state
        roomModel.EducationalDialogueCompleted = true;
        Assert.IsTrue(roomModel.EducationalDialogueCompleted, $"roomModel.EducationalDialogueCompleted was expected to be true. Got '{roomModel.EducationalDialogueCompleted}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling IsComplete when both 'MinigameCompleted' and 
    /// 'EducationalDialgoueCompleted' are false.
    /// </summary>
    [Test]
    public void IsCompleteFalse()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();

        // verify the value returned from IsComplete()
        var result = roomModel.IsComplete();
        Assert.IsFalse(result, $"roomModel.IsCompelete was expected to return false, Got '{result}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling IsComplete when both 'MinigameCompleted' and 
    /// 'EducationalDialgoueCompleted' are true.
    /// </summary>
    [Test]
    public void IsCompleteTrue()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();

        // verify the value returned from IsComplete()
        roomModel.MinigameCompleted = true;
        roomModel.EducationalDialogueCompleted = true;
        var result = roomModel.IsComplete();
        Assert.IsTrue(result == true, $"roomModel.IsCompelete was expected to return true, Got '{result}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }

    /// <summary>
    /// Test calling IsComplete() when either 'MinigameCompleted' or
    /// 'EducationalDialogueCompleted' is true
    /// </summary>
    [Test]
    public void IsCompleteEdgeCases()
    {
        // create GameObject
        GameObject go = new GameObject();

        // add 'model' component
        IRoomModel roomModel = go.AddComponent<RoomModel>();

        // verify the value returned from IsComplete() when minigameCompleted is true
        roomModel.MinigameCompleted = true;
        var result = roomModel.IsComplete();
        Assert.IsFalse(result, $"roomModel.IsCompelete was expected to return false, Got '{result}'");

        // verify the value returned from IsComplete() when minigameCompleted is true
        roomModel.MinigameCompleted = false;
        roomModel.EducationalDialogueCompleted = true;
        result = roomModel.IsComplete();
        Assert.IsFalse(result, $"roomModel.IsCompelete was expected to return false, Got '{result}'");

        // clean up game object
        Object.DestroyImmediate(go);
    }
}