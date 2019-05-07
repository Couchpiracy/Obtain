using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTiles : MonoBehaviour {
    GameObject Tile;
    GameObject parent1;
    GameObject parent2;

    // Use this for initialization
    void Start () {
        Tile = (GameObject)Resources.Load("Cube");
        parent1 = new GameObject("Ally Team Tiles");
        parent2 = new GameObject("Enemy Team Tiles");
        GenerateTileGrid(4, 5, 1.5f, parent1);
        GenerateTileGrid(4, 5, -1.5f, parent2);
        }

    void GenerateTileGrid(float width, float height, float offset, GameObject parent) {
        float widthStart;
        float heightStart;
        int k = 1;
        if(width % 2 == 0) {
            widthStart = width/2 - 0.5f + offset;
            }
        else {
            widthStart = width/2;
            }
        if(height % 2 == 0) {
            heightStart = height/2 - 0.5f;
            }
        else {
            heightStart = height/2;
            }
       
        for(int i = 0; i < height; i++) {
            for(int j = 0; j < width; j++) {
                GameObject Box = (GameObject)Instantiate(Tile, new Vector3(widthStart+offset, 0, heightStart), transform.rotation, parent.transform);
                Box.name = "Tile" + k;
                k++;
                //Tile.transform.parent = parent.transform;
                widthStart--;
                }
            widthStart = widthStart + width;
            heightStart--;
            }

        k = 1;
        }

}
