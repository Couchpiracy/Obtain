using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {
    
    public QuestObject relatedQuest;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
            if(relatedQuest.cleared == false)
                other.gameObject.GetComponent<QuestProgress>().ClearObjective(gameObject);

    }
}
