using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class QuestProgress : MonoBehaviour {
    /*
    public enum Objectives {
        Main,
        Quinn
    }
    public Objectives currentObjective;
    */
    public QuestText questText;

    public GameObject trackedQuestObject;

    public List<GameObject> allQuestObjects;

    private List<string> ClearedQuests;

    public TriggerSnapshot triggerSnapshot;

    public TestController testController;

    public NavMeshAgentMove navAgent;

    private void Start() {             
        ClearedQuests = new List<string>();
        ResetProgress();
        ChangeSnapshot();
        navAgent.SetDestination(trackedQuestObject.transform);
    }

    public void ChangeTrackedQuest(GameObject newQuest) {       // Takes in a quest object
        trackedQuestObject = newQuest;                          // Sets tracked quest to the new quest object
        ChangeSnapshot();                                       // Changes the audio snapshot to correspond to the new tracked quest
        navAgent.SetDestination(trackedQuestObject.transform);  // Sets the NavAgent's destination to the location of the new tracked quest
        questText.SetText(newQuest);
    }

    private void ChangeSnapshot() {
        if(trackedQuestObject.name.Contains("Quinn"))
            triggerSnapshot.SwapSnapshot(triggerSnapshot.targetSnapshot, 1);        // If the quest name contains "Quinn", it's related to Quinn, and therefore plays the stem for Quinn.
        else
            triggerSnapshot.SwapSnapshot(triggerSnapshot.targetSnapshotTwo, 1);     // Otherwise it's the general quest stem.
    }

    public void ClearObjective(GameObject questObject) {
        ClearedQuests.Add(questObject.name);                                                                                // Adds current quest to the list of cleared quests.
        questObject.GetComponent<QuestTrigger>().relatedQuest.cleared = true;                                               // Sets the Scriptable Object to true.
        if (trackedQuestObject == questObject) {                                                                            // If the cleared quest is also the tracked quest, go through to see what quest should be tracked next.
            if (allQuestObjects.IndexOf(trackedQuestObject) < allQuestObjects.Count - 1) {
                for (int i = 0 ; i < allQuestObjects.Count ; i++) {
                    if (!ClearedQuests.Contains(allQuestObjects[allQuestObjects.IndexOf(trackedQuestObject) + i].name)) {   // Checks if the next quest is cleared or not
                        ChangeTrackedQuest(allQuestObjects[allQuestObjects.IndexOf(trackedQuestObject) + i]);               // If it isn't, set tracked quest and then end the loop.
                        break;
                    }
                }

            }
        }
        /*
        if(ClearedQuests.Count == allQuestObjects.Count) {                                                                  // If all quests are completed, reset progress. 
            //testController.ChangeTestEnvironment(1);    
            ResetProgress();
        }
        */
    }

    public void ResetProgress() {
        foreach(GameObject q in allQuestObjects) {                          // Goes through all quest objects, and sets their completed status to false.
            q.GetComponent<QuestTrigger>().relatedQuest.cleared = false;
        }
        ClearedQuests.Clear();                                              // Clears the list of cleared quests
        ChangeTrackedQuest(allQuestObjects[0]);                             // Sets the tracked quest to the first of the list. 
    }
}
