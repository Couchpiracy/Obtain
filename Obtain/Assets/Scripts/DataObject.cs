using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "dataObject", menuName = "Data")]
public class DataObject : ScriptableObject {

    public List<Vector3> DesertPos = new List<Vector3>();
    public List<Vector3> VillagePos = new List<Vector3>();
    public List<Vector3> QuinnPos = new List<Vector3>();
    public List<Vector3> QuarryPos = new List<Vector3>();
}
