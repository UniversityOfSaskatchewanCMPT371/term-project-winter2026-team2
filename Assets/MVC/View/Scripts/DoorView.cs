using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[ RequireComponent(typeof(DoorLogic)) ]
[ RequireComponent(typeof(Collider)) ]
public class DoorView : MonoBehaviour
{
    public DoorLogic doorLogic;

    private void Awake()
    {
        Assert.IsNotNull(doorLogic, "Field DoorLogic cannot be null.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        doorLogic.OnPlayerEnter(other.gameObject);
    }
}
