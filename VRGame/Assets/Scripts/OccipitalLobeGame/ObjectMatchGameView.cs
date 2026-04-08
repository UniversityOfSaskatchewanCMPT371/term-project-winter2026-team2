using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    // Store references to the guess box, and various buttons so we can enable and disable them as needed
    [SerializeField] private GameObject guessBox;
    [SerializeField] private GameObject submitButton;
    [SerializeField] private GameObject startLevelButton;
    [SerializeField] private GameObject startTutorialButton;
    [SerializeField] private GameObject leaveTutorialButton;

    // Store references to the text components so they can be updated here
    [SerializeField] private TextMeshProUGUI inLevelDisplay;
    [SerializeField] private TextMeshProUGUI outOfLevelDisplay;


    // Test-accessible properties for UI elements
    internal GameObject GuessBox { get => guessBox; set => guessBox = value; }
    internal GameObject SubmitButton { get => submitButton; set => submitButton = value; }
    internal GameObject StartLevelButton { get => startLevelButton; set => startLevelButton = value; }
    internal GameObject StartTutorialButton { get => startTutorialButton; set => startTutorialButton = value; }
    internal GameObject LeaveTutorialButton { get => leaveTutorialButton; set => leaveTutorialButton = value; }
    internal TextMeshProUGUI InLevelDisplay { get => inLevelDisplay; set => inLevelDisplay = value; }
    internal TextMeshProUGUI OutOfLevelDisplay { get => outOfLevelDisplay; set => outOfLevelDisplay = value; }
    /// <inheritdoc/>
    public override void Init()
    {
        CheckControllerRef();
        // Set the camera for the canvas
        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }
        
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(false);
        }


        startLevelButton.SetActive(true);
        startTutorialButton.SetActive(true);
        leaveTutorialButton.SetActive(false);
        guessBox.SetActive(false);
        submitButton.SetActive(false);

        inLevelDisplay.gameObject.SetActive(false);
        outOfLevelDisplay.gameObject.SetActive(false);
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
        startTutorialButton.SetActive(false);
        leaveTutorialButton.SetActive(false);

        inLevelDisplay.gameObject.SetActive(true);
        outOfLevelDisplay.gameObject.SetActive(false);
    }

    /// <inheritdoc/>
    public void ExitLevel()
    {
        guessBox.SetActive(false);
        submitButton.SetActive(false);
        startLevelButton.SetActive(true);
        startTutorialButton.SetActive(true);
        inLevelDisplay.gameObject.SetActive(false);
        outOfLevelDisplay.gameObject.SetActive(true);
    }

    /// <inheritdoc/>
    public void UpdateTimer(int seconds)
    {
        inLevelDisplay.text = "Time: " + Mathf.CeilToInt(seconds).ToString();
    }

    /// <inheritdoc/>
    public void UpdateScore(int totalScore, int[] levelScores)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("Total Score: " + totalScore);
        for (int i = 0; i < levelScores.Length; i++)
        sb.AppendLine("Level " + (i+1) + " Score: " + levelScores[i]);
        
        outOfLevelDisplay.text = sb.ToString();
    }
    
    /// <inheritdoc/>
    public void EnterTutorial()
    {
        startLevelButton.SetActive(false);
        startTutorialButton.SetActive(false);
        leaveTutorialButton.SetActive(true);
        guessBox.SetActive(true);
        submitButton.SetActive(true);
        inLevelDisplay.gameObject.SetActive(false);
        outOfLevelDisplay.gameObject.SetActive(false);
    }
    
    /// <inheritdoc/>
    public void ExitTutorial()
    {
        leaveTutorialButton.SetActive(false);
        startTutorialButton.SetActive(true);
        startLevelButton.SetActive(true);
        guessBox.SetActive(false);
        submitButton.SetActive(false);
        inLevelDisplay.gameObject.SetActive(false);
        outOfLevelDisplay.gameObject.SetActive(false);
    }
}
