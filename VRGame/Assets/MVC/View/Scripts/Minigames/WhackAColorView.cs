using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ RequireComponent(typeof(WhackAColorLogic)) ]

/// <summary>
/// View layer for WhackAColor minigame.
/// </summary>
public class WhackAColorView : MonoBehaviour
{
    public GameObject cubesContainer;                  // Reference to the parent of colorable cubes
    public GameObject colorImage;                      // Reference to the colorable image
    public GameObject textUI;                          // Reference to the label UI
    public GameObject startButton;                     // Reference to the start button UI
    public WhackAColorLogic whackAColorLogic;          // Reference to the controller layer

    /// <summary>
    /// Changes the text on the title.
    /// </summary>
    /// <param name="text">Text to be changed into.</param>
    private void changeTitleText(string text)
    {
        textUI.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    /// <summary>
    /// Handles collision input from hammer.
    /// </summary>
    /// <param name="collision">Reference to the collider the hammer hit.</param>
    public void OnHammerHit(Collision collision)
    {
        if (collision.gameObject.transform.parent == cubesContainer.transform)
        {
            whackAColorLogic.VerifyColorHit(collision.gameObject.GetComponent<Renderer>().material.color);   
        }
    }

    /// <summary>
    /// Changes the color of the given object.
    /// </summary>
    /// <param name="obj">GameObject reference.</param>
    /// <param name="color">Color to change into.</param>
    public void ChangeCubeColor(GameObject obj, Color color)
    {
        obj.GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// Changes the color of the colorable image.
    /// </summary>
    /// <param name="color">Color to change into.</param>
    public void ChangeTargetColor(Color color)
    {
        colorImage.GetComponent<Image>().color = color;
    }

    /// <summary>
    /// Changes the color of the cubes in CubesContainer to the given ColorPool in the given sequence.
    /// </summary>
    /// <param name="colorPool">Randomize color pool</param>
    public void RandomizeCubeColors(Color[] colorPool)
    {
        for (int i = 0; i < cubesContainer.transform.childCount; i++)
        {
            GameObject child = cubesContainer.transform.GetChild(i).gameObject;
            ChangeCubeColor(child, colorPool[i]);
        }
    }

    /// <summary>
    /// Changes the color fo the cubes in CubesContainer to a specific color.
    /// </summary>
    /// <param name="color">Colro to change into.</param>
    public void ChangeAllCubeColors(Color color)
    {
        for (int i = 0; i < cubesContainer.transform.childCount; i++)
        {
            GameObject child = cubesContainer.transform.GetChild(i).gameObject;
            ChangeCubeColor(child, color);
        }
    }

    /// <summary>
    /// Displays the status of the game.
    /// </summary>
    /// <param name="score">Current score.</param>
    /// <param name="goal">Goal to reach.</param>
    /// <param name="timer">Current time.</param>
    public void DisplayStatus(int score, int goal, float timer)
    {
        changeTitleText($"Timer: {Math.Round(timer)}\nScore: {score}/{goal}");
    }

    /// <summary>
    /// Handles input from start button being pressed.
    /// </summary>
    public void OnStartButtonPressed()
    {
        startButton.SetActive(false);

        whackAColorLogic.StartGame();

        CancelInvoke();
    }

    /// <summary>
    /// Displays the title of the game.
    /// </summary>
    public void DisplayTitle()
    {
        changeTitleText("Whack a Color");
    }

    /// <summary>
    /// Displays the result.
    /// </summary>
    /// <param name="result">Game results.</param>
    public void DisplayResults(bool result)
    {
        startButton.SetActive(true);

        if (result)
        {
            changeTitleText("You Win!");
        } else
        {
            changeTitleText("You Lose!");
        }
    }
}
