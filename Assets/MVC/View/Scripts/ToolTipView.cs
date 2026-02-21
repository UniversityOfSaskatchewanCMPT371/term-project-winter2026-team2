using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class ToolTipView : MonoBehaviour, IToolTipView
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public ToolTipModel data;

    // Start is called before the first frame update
    void Start()
    {
      Assert.IsNotNull(title, "Title TextMeshProUGUI component is not assigned in the inspector.");
      Assert.IsNotNull(description, "Description TextMeshProUGUI component is not assigned in the inspector.");
      title.SetText(data.title);
      description.SetText(data.description);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //interface part for testing
    public void UpdateContent(IToolTipModel model)
    {
        title.SetText(model.Title);
        description.SetText(model.Description);
    }
    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }
    
}
