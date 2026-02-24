using UnityEngine;
using NUnit.Framework;
using TMPro;
using UnityEngine.TestTools;

public class ToolTipViewTests
{
    private GameObject toolTipGameObject;
    private ToolTipView toolTipView;
    private ToolTipModel toolTipModel;

    [UnityTest]
    public IEnumerator Start_SetsTextFromAssignedModel()
    {
        //root gameobject for the view
        toolTipGameObject = new GameObject("ToolTip");
        toolTipView = toolTipGameObject.AddComponent<ToolTipView>();
        
        //child gameobjects for title and description
        var titleObject = new GameObject("Title");
        titleObject.transform.SetParent(toolTipGameObject.transform);
        toolTipView.title = titleObject.AddComponent<TextMeshProUGUI>();

        //description child gameobject
        var descriptionObject = new GameObject("Description");
        descriptionObject.transform.SetParent(toolTipGameObject.transform);
        toolTipView.description = descriptionObject.AddComponent<TextMeshProUGUI>();

        //tooltipmodel
        toolTipModel = (ToolTipModel)ScriptableObject.CreateInstance(typeof(ToolTipModel));
        toolTipModel.Title = "PlayMode Title";
        toolTipModel.Description = "PlayMode Description";
    
        //assign the model to the view before Start is called
        toolTipView.data = toolTipModel;

        //wait a frame for Start to execute
        yield return null;

        Assert.AreEqual("PlayMode Title", toolTipView.title.text);
        Assert.AreEqual("PlayMode Description", toolTipView.description.text);

        //cleanup
        UnityEngine.Object.DestroyImmediate(toolTipGameObject);
        UnityEngine.Object.DestroyImmediate(toolTipModel);
    }
}
