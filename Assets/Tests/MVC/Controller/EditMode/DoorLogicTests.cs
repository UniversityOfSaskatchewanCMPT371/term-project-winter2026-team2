using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using System.Reflection;

/// <summary>
/// Test class for DoorLogic component in edit mode.
/// This class contains unit tests to verify the behavior of the DoorLogic controller.
/// </summary>
public class DoorLogicTests
{
    /// <summary>
    /// GameObject representing the door in the test scene.
    /// </summary>
    private GameObject doorObject;

    /// <summary>
    /// Instance of DoorLogic component being tested.
    /// </summary>
    private DoorLogic doorLogic;

    /// <summary>
    /// Data model for the door configuration.
    /// </summary>
    private DoorData doorData;
}