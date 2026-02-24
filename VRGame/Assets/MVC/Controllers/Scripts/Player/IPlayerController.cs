using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerController
{
    /// <summary>
    /// Initialize the player controller and validate model/view references
    /// </summary>
    void Awake();
}
