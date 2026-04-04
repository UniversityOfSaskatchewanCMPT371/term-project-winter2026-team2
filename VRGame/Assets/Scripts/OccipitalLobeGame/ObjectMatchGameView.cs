using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMatchGameView : View<IObjectMatchGameController>, IObjectMatchGameView
{
    [SerializeField] private GameObject[] allObjects;
    internal IObjectMatchGameController controller;
    
    // Test-accessible property for allObjects
    internal GameObject[] AllObjects
    {
        get => allObjects;
        set => allObjects = value;
    }
    

    // Store references to the guess box, submit button, and start buttons so we can enable and disable them as needed
    [SerializeField] private GameObject guessBox;
    [SerializeField] private GameObject submitButton;
    [SerializeField] private GameObject startLevelButton;


    // Test-accessible properties for UI elements
    internal GameObject GuessBox { get => guessBox; set => guessBox = value; }
    internal GameObject SubmitButton { get => submitButton; set => submitButton = value; }
    internal GameObject StartLevelButton { get => startLevelButton; set => startLevelButton = value; }

    /// <inheritdoc/>
    public override void Init()
    {
        CheckControllerRef();
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(false);
        }

        startLevelButton.SetActive(true);
        guessBox.SetActive(false);
        submitButton.SetActive(false);
    }

    /// </inheritdoc>
    public void ShowObjects(string[] ObjectIDs)
    {
        if (ObjectIDs == null)
        {
            Debug.LogWarning("ObjectMatchGameView was asked to show objects with an empty or null list of object IDs. No objects will be shown.");
            return;
        }
        foreach (string id in ObjectIDs)
        {
            GameObject obj = System.Array.Find(allObjects, element => element.name == id);
            if (obj != null)
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.LogWarning("ObjectMatchGameView was asked to show object with ID " + id + " but that ID was not found in the list of all objects.");
            }
        }
    }

    /// <inheritdoc/>
    public void ClearAllObjects()
    {
    foreach (GameObject obj in allObjects)
        {
            obj.SetActive(false);
        }
    }

    /// <inheritdoc/>
    public void EnterLevel()
        {
        startLevelButton.SetActive(false);
        guessBox.SetActive(true);
        submitButton.SetActive(true);
    }

    /// <inheritdoc/>
    public void ExitLevel()
    {
        guessBox.SetActive(false);
        submitButton.SetActive(false);
        startLevelButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
