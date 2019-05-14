using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest", order = 1)]
public class QuestObject : ScriptableObject {
    
    public bool cleared;
    public string description;
}
