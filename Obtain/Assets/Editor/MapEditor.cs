using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (GenerateGrid))]
public class MapEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GenerateGrid Map = target as GenerateGrid;

        Map.GenerateMap();
        }
    }
