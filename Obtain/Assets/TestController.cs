using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestController : MonoBehaviour {

    public GenerateGrid generator;

    public GameObject firstSeed;
    public GameObject secondSeed;

    public Vector3 spawnLocation;
    public Vector3 quinnLocation;

    public GameObject player;
    public GameObject quinn;
    public GameObject navAgent;

    public void ChangeTestEnvironment(int index) {

        generator.mapIndex = index;
        firstSeed.SetActive(false);
        secondSeed.SetActive(true);

        player.transform.position = spawnLocation;
        quinn.transform.position = quinnLocation;
        navAgent.transform.position = spawnLocation;
    }
}
