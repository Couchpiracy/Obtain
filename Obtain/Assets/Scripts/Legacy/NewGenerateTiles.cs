using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerateTiles : MonoBehaviour {

    GameObject Tile;
    GameObject parent1;
    GameObject parent2;

    // Use this for initialization
    void Start() {
        Tile = (GameObject)Resources.Load("Cube");
        parent1 = new GameObject("Ally Team Tiles");
        parent2 = new GameObject("Enemy Team Tiles");
        GenerateTileGrid(4, 5, 180, 0f, parent1);
        GenerateTileGrid(4, 5, 180, -0f, parent2);
        }

    // In hindsight, why the fuck am I doing this every runtime, when I can just save the grids as a prefab?

    void GenerateTileGrid(float width, float height, float rotation, float offset, GameObject parent) {
        parent.transform.position = new Vector3(0.5f, 0.5f, 4.5f);
        float widthStart;
        float heightStart;
        int k = 1;
        if(width % 2 == 0) {
            widthStart = width / 2 + offset;
            }
        else {
            widthStart = width / 2  + offset;
            //widthStart -= 0.5f;
            }
        if(height % 2 == 0) {
            heightStart = height / 2 ;
            }
        else {
            heightStart = height / 2;
            //heightStart -= 0.5f;
            }

        for(int i = 0; i < width; i++) {
            for(int j = 0; j < height; j++) {
                GameObject Box = Instantiate(Tile, new Vector3(widthStart, 0, heightStart), transform.rotation, parent.transform);
                Box.name = "Tile" + k;
                k++;
                heightStart++;
                }
            heightStart = heightStart - height;
            widthStart--;
            }
        k = 1;
        parent.transform.eulerAngles = new Vector3(0, rotation, 0);

        }
    }
