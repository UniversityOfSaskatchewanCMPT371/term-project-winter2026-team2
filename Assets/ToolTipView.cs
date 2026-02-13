using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using JetBrains.Annotations;
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
        title.SetText(data.title);
        description.SetText(data.description);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
