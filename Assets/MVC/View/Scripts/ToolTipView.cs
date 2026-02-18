using TMPro;
using UnityEngine;

public class ToolTipView : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public ToolTipModel data;
    // Start is called before the first frame update
    void Start()
    {
        if (data == null && description == null)
        {
            Debug.LogError("ToolTipModel data and description is not assigned in the Unity Editor.");
            return;
        }
        title.SetText(data.title);
        description.SetText(data.description);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
