using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceEfficientGeneratingTiles : MonoBehaviour {

    GameObject AllyGrid;
    GameObject EnemyGrid;
    GameObject Vivi;

    string[] TileSelect;
    void Awake() {
        GenerateTileSelection();
        SelectTiles();
        }

    void Start() {
       
        Vivi = (GameObject)Resources.Load("Vivi");
        AllyGrid = (GameObject)Resources.Load("AllyTeamTiles");
        EnemyGrid = (GameObject)Resources.Load("EnemyTeamTiles");
        GameObject AllyTiles = Instantiate(AllyGrid, new Vector3(2.5f, 0, 0), this.transform.rotation);
        AllyTiles.transform.eulerAngles = new Vector3(0, 180, 0);
        GameObject EnemyTiles = Instantiate(EnemyGrid, new Vector3(-2.5f, 0, 0), this.transform.rotation);

        foreach(GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>()) {
            if(go.CompareTag("Parent") == true) {
                foreach(Transform child in go.GetComponentsInChildren<Transform>()) {
                    int i; // Chance for a unit to spawn
                    //int j = UnityEngine.Random.Range(1,21); // Tile Selection Row 1:[1-5] Row 2:[6-10] Row 3:[11-15] Row 4:[16-20] 
                    int j = 0;
                    int k = 0; // Decides whether to spawn a unit or not
                    int l = 0; // temp value
                    if(child.name.Substring(0,4) == "Tile") {
                        if(child.name.Length == 5) {
                            j = Int32.Parse(child.name.Substring(4, 1));
                            print(child.name.Substring(4, 1));
                        }
                        else {
                            j = Int32.Parse(child.name.Substring(4, 2));
                            print(child.name.Substring(4, 2));
                        }

                        if (j > 0 && j < 6) {
                            i = UnityEngine.Random.Range(1, 100);
                            if(i < 61) {
                                k = 1;
                            }
                        }
                        else if(j > 5 && j < 11) {
                            i = UnityEngine.Random.Range(1, 100);
                            if(i < 41) {
                                k = 1;
                            }
                        }
                        else if(j > 10 && j < 16) {
                            i = UnityEngine.Random.Range(1, 100);
                            if(i < 31) {
                                k = 1;
                            }
                        }
                        else if(j > 15 && j < 21) {
                            i = UnityEngine.Random.Range(1, 100);
                            if (i < 21) {
                                k = 1;
                            }
                        }
                        else {
                            print("Something went horribly wrong. j is: " + j);
                        }

                        for (l = 0; l < k; l++) {    
                            Vector3 chPos = child.transform.position;
                            print("I got this far");
                            Vector3 newRotation = new Vector3(chPos.x, chPos.y, chPos.z);
                            GameObject Unit = Instantiate(Vivi, new Vector3(chPos.x, chPos.y + 0.5f, chPos.z), new Quaternion(0, 0, 0, 0));
                            Vivi.transform.eulerAngles = newRotation;
                        }
                    }
                }
            }   
        }
    }
    

    private void GenerateTileSelection() {
        int i = 1;
        TileSelect = new string[21];
        for (i = 1; i < 21; i++) {
            TileSelect[i] = ("Tile" + (i));
            print(TileSelect[i]);
        }
    }

    private void SelectTiles() {
        
    }

    /*     
    private Transform FindChildren() {
        foreach (GameObject go in UnityEngine.Object.FindObjectsOfType<GameObject>()) {
            if (go.CompareTag("Parent") == true) {
                foreach (Transform child in go.GetComponentsInChildren<Transform>()) {
                    return child;
                }

            }
            else {
                return null;
            }
            return null;
        }
        return null;
    }
    private void SpawnVivis(Transform FindChildren) {
        if (FindChildren.Find(TileSelect[j]) == true) {
            Vector3 chPos = FindChildren.Find(TileSelect[j]).transform.position;
            print("Found " + TileSelect[j]);
            Vector3 newRotation = new Vector3(chPos.x, chPos.y, chPos.z);
            GameObject Unit = Instantiate(Vivi, new Vector3(chPos.x, chPos.y + 0.5f, chPos.z), new Quaternion(0, 0, 0, 0));
            Vivi.transform.eulerAngles = newRotation;
        }
    }
    */
    }

