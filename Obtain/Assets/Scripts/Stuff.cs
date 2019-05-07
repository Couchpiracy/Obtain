using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuff : MonoBehaviour {

    public Transform player;
    public Transform pillarObject;
    Queue<Coord> pillarCoords;
    Coord currentPillar;
    Coord playerPos;
    bool placedPillar;
    
    void Start() {
        pillarCoords = new Queue<Coord>();
        currentPillar = new Coord(999,-999);
        pillarCoords.Enqueue(currentPillar);
    }

    void FixedUpdate() {
        playerPos = new Coord((int)player.position.x, (int)player.position.z);

        if(playerPos.x != currentPillar.x || playerPos.z != currentPillar.z) {
            for(int x = 0; x < 5; x++) {
                Transform newPillar = Instantiate(pillarObject, new Vector3(playerPos.x + x, 0, playerPos.z + x), Quaternion.identity) as Transform;

                }
            currentPillar = new Coord(playerPos.x, playerPos.z);
            pillarCoords.Enqueue(currentPillar);
            }
        }
}

[System.Serializable]
public struct Coord {
    public int x;
    public int z;

    public Coord(int _x, int _z) {
        x = _x;
        z = _z;

    }
    public static bool operator ==(Coord c1, Coord c2) {
        return c1.x == c2.x && c1.z == c2.z;
    }
    public static bool operator !=(Coord c1, Coord c2) {
        return !(c1 == c2);
    }

}
