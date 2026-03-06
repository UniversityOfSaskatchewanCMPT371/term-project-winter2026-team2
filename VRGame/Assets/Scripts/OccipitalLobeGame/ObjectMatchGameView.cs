using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMatchGameView : View<IObjectMatchGameController>, IObjectMatchGameView
{
    [SerializeField] private GameObject[] allObjects;
    [SerializeField] private GameObject guessBox;
    [SerializeField] private GameObject submitButton;
    public override void Init()
    {
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(false);
        }

        guessBox.SetActive(false);
        submitButton.SetActive(false);
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

        guessBox.SetActive(true);
        submitButton.SetActive(true);
    }
}
