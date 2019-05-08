using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestProgress : MonoBehaviour {
    /*
    public enum Objectives {
        Main,
        Quinn
    }
    public Objectives currentObjective;
    */
    public QuestObject trackedQuest;

    public List<QuestObject> allQuests;

    private List<string> ClearedQuests;

    public TriggerSnapshot triggerSnapshot;

    public TestController testController;

    private void Start() {
        trackedQuest = allQuests[0];
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

    public void ChangeTrackedQuest(QuestObject newQuest) {
        trackedQuest = newQuest;
        ChangeSnapshot();
    }

    private void ChangeSnapshot() {
        if(trackedQuest.name.Contains("Quinn"))
            triggerSnapshot.SwapSnapshot(triggerSnapshot.targetSnapshot, 1);
        else
            triggerSnapshot.SwapSnapshot(triggerSnapshot.targetSnapshotTwo, 1);

    }

    public void ClearObjective(QuestObject quest) {
        ClearedQuests.Add(quest.name);
        quest.cleared = true;
        if(trackedQuest == quest)
            ChangeTrackedQuest(allQuests[allQuests.IndexOf(trackedQuest) + 1]);

        print("Cleared " + quest.name); 

        if(ClearedQuests.Count == allQuests.Count) {
            //testController.ChangeTestEnvironment(1);    
            ResetProgress();
        }
    }

    public void ResetProgress() {
        foreach(QuestObject q in allQuests) {
            q.cleared = false;
        }
        ClearedQuests.Clear();
        trackedQuest = allQuests[0];
    }
}
