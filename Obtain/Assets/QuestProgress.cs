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
    [SerializeField]
    private GameObject trackedQuestObject;

    public List<GameObject> allQuestObjects;

    private List<string> ClearedQuests;

    public TriggerSnapshot triggerSnapshot;

    public TestController testController;

    public NavMeshAgentMove navAgent;

    private void Start() {
        trackedQuestObject = allQuestObjects[0];
        navAgent.SetDestination(trackedQuestObject.transform);
        ClearedQuests = new List<string>();
        ResetProgress();
        ChangeSnapshot();
    }

    /*
    public void ChangeObjective(Objectives target) {
        currentObjective = target;

        if(currentObjective == Objectives.Main)
            triggerSnapshot.SwapSnapshot(triggerSnapshot.targetSnapshot, 1);
        else
            triggerSnapshot.SwapSnapshot(triggerSnapshot.targetSnapshotTwo, 1);
    }
    */

    public void ChangeTrackedQuest(GameObject newQuest) {
        trackedQuestObject = newQuest;
        ChangeSnapshot();
        navAgent.SetDestination(trackedQuestObject.transform);
    }

    private void ChangeSnapshot() {
        if(trackedQuestObject.name.Contains("Quinn"))
            triggerSnapshot.SwapSnapshot(triggerSnapshot.targetSnapshot, 1);
        else
            triggerSnapshot.SwapSnapshot(triggerSnapshot.targetSnapshotTwo, 1);

    }

    public void ClearObjective(GameObject questObject) {
        ClearedQuests.Add(questObject.name);
        questObject.GetComponent<QuestTrigger>().relatedQuest.cleared = true;
        if(trackedQuestObject == questObject) {
            print("Tracked quest cleared, changing tracked quest");
            if(allQuestObjects.IndexOf(trackedQuestObject) < allQuestObjects.Count-1) {
                for(int i = 0; i < allQuestObjects.Count; i++) {
                    if(ClearedQuests.Contains(allQuestObjects[allQuestObjects.IndexOf(trackedQuestObject) + i].name))
                        print("Skipped " + allQuestObjects[allQuestObjects.IndexOf(trackedQuestObject) + i].name);
                    else {
                        ChangeTrackedQuest(allQuestObjects[allQuestObjects.IndexOf(trackedQuestObject) + i]);
                        break;
                    }
                        
                }
                
            }
        }
        print("Cleared " + questObject.name); 

        if(ClearedQuests.Count == allQuestObjects.Count) {
            //testController.ChangeTestEnvironment(1);    
            ResetProgress();
        }
    }

    public void ResetProgress() {
        foreach(GameObject q in allQuestObjects) {
            q.GetComponent<QuestTrigger>().relatedQuest.cleared = false;
        }
        ClearedQuests.Clear();
        trackedQuestObject = allQuestObjects[0];
    }
}
