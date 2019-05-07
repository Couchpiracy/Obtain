using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IdkButItIsReallyCool))]
public class GenerateTileGridEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        IdkButItIsReallyCool Map = target as IdkButItIsReallyCool;

        Map.GenerateMap();
        }
    }