using UnityEngine;
using UnityEngine.UI;

public class QuestText : MonoBehaviour {
    
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetText(GameObject questObject)
    {
        text.text = questObject.GetComponent<QuestTrigger>().relatedQuest.description;
    }
}
