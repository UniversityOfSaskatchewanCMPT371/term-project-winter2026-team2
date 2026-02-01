using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ RequireComponent(typeof(WhackAColorLogic)) ]
public class WhackAColorView : MonoBehaviour
{
    public GameObject cubesContainer;
    public GameObject colorImage;
    public GameObject textUI;
    public GameObject startButton;
    public WhackAColorLogic whackAColorLogic;

    public void OnHammerHit(Collision collision)
    {
        if (collision.gameObject.transform.parent == cubesContainer.transform)
        {
            whackAColorLogic.VerifyColorHit(collision.gameObject.GetComponent<Renderer>().material.color);   
        }
    }

    public void ChangeCubeColor(GameObject obj, Color color)
    {
        obj.GetComponent<Renderer>().material.color = color;
    }

    public void ChangeTargetColor(Color color)
    {
        colorImage.GetComponent<Image>().color = color;
    }

    public void RandomizeCubeColors(Color[] colorPool)
    {
        for (int i = 0; i < cubesContainer.transform.childCount; i++)
        {
            GameObject child = cubesContainer.transform.GetChild(i).gameObject;
            ChangeCubeColor(child, colorPool[i]);
        }
    }

    public void ChangeAllCubeColors(Color color)
    {
        for (int i = 0; i < cubesContainer.transform.childCount; i++)
        {
            GameObject child = cubesContainer.transform.GetChild(i).gameObject;
            ChangeCubeColor(child, color);
        }
    }

    public void OnUpdate(int score, int goal, float timer)
    {
        textUI.GetComponentInChildren<TextMeshProUGUI>().text = $"Timer: {Math.Round(timer)}\nScore: {score}/{goal}";
    }

    public void MinigameReady()
    {
        textUI.GetComponentInChildren<TextMeshProUGUI>().text = "Whack a Color";
    }

    public void OnStartButtonPressed()
    {
        startButton.SetActive(false);

        whackAColorLogic.StartGame();

        CancelInvoke();
    }

    public void MinigameFinished(bool result)
    {
        startButton.SetActive(true);

        if (result)
        {
            textUI.GetComponentInChildren<TextMeshProUGUI>().text = $"You Win!";
        } else
        {
            textUI.GetComponentInChildren<TextMeshProUGUI>().text = $"You Lose!";
        }

        Invoke("MinigameReady", 5);
    }
}
