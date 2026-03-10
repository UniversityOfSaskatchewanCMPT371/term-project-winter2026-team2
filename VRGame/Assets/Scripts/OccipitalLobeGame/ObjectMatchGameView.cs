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
    

    // Store references to the guess box and submit button so we can enable and disable them as needed
    [SerializeField] private GameObject guessBox;
    [SerializeField] private GameObject submitButton;

    // Test-accessible properties for UI elements
internal GameObject GuessBox { get => guessBox; set => guessBox = value; }
internal GameObject SubmitButton { get => submitButton; set => submitButton = value; }
    public override void Init()
    {
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(false);
        }
    }

    /// </inheritdoc>
    public void ShowObjects(string[] ObjectIDs)
    {
        foreach (GameObject obj in allObjects)
        {
            if (System.Array.Exists(ObjectIDs, element => element == obj.name))
            {
                obj.SetActive(true);
            }
            else
            {
                Debug.LogWarning("ObjectMatchGameView was asked to show object with ID " + obj.name + " but that ID was not found in the list of active object IDs.");
                obj.SetActive(false);
            }
        }
    }

    public void removeGuess()
    {
        throw new System.NotImplementedException();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
