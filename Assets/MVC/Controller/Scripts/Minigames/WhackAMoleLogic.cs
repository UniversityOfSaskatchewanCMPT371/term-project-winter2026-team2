using UnityEngine;
using UnityEngine.Assertions;

[ RequireComponent(typeof(WhackAColorData)) ]
public class WhackAMoleLogic : MonoBehaviour
{
    public WhackAColorData whackAColorData;
    public GameObject colorCubes;

    void Awake()
    {
        Assert.IsNotNull(colorCubes, "Field ColorCubes cannot be null");

        StartMiniGame();
    }

    private void RandomizeCubeColors(Color[] colorPool)
    {
        for (int i = 0; i < colorCubes.transform.childCount; i++)
        {
            GameObject children = colorCubes.transform.GetChild(i).gameObject;

            ChangeCubeColor(children, colorPool[Random.Range(0, colorPool.Length)]);
        }
    }

    private void ResetCubeColors()
    {
        for (int i = 0; i < colorCubes.transform.childCount; i++)
        {
            GameObject children = colorCubes.transform.GetChild(i).gameObject;

            ChangeCubeColor(children, whackAColorData.defaultColor);
        }
    }

    public void StartMiniGame()
    {
        // throw exception here
        print("minigame started");
        if (!colorCubes) return;

        if (!whackAColorData.IsReady() || whackAColorData.IsPlaying()) return;
        print("passed sanity checks");
        Color[] colorPool = whackAColorData.GetListOfRandomColors();

        whackAColorData.SetTargetColor(colorPool[Random.Range(0,colorPool.Length)]);

        RandomizeCubeColors(colorPool);
    }

    public void FinishMiniGame()
    {
        if (!colorCubes) return;

        if (!whackAColorData.IsPlaying()) return;

        ResetCubeColors();
        whackAColorData.ResetData();
    }

    private void ChangeCubeColor(GameObject cube, Color color)
    {
        cube.GetComponent<Renderer>().material.color = color;
    }

    void Update()
    {
        
    }
}
