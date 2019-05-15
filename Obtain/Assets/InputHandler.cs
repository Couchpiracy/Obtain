using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [Header("Data")]
    public Button[] buttons;
    private GameObject player;
    [SerializeField]
    public DataObject firstData, secondData;
    private DataObject dataObject;
    [Header("Cubes")]
    public bool placeCubes = false;
    public GameObject positionCube;
    public Color[] colors = new Color[4];

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.name == "Player1") {
            dataObject = firstData;
        }
        else dataObject = secondData;
        ClearData();
    }

    private void Update() {

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            buttons[0].onClick.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2)) {
            buttons[1].onClick.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3)) {
            buttons[2].onClick.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.Alpha4)) {
            buttons[3].onClick.Invoke();
        }
    }

    public void SetCoordinate(int index) {
        if (placeCubes) {
            GameObject posCube = Instantiate(positionCube) as GameObject;
            posCube.transform.position = new Vector3(player.transform.position.x, 100, player.transform.position.z);
            posCube.GetComponent<MeshRenderer>().material.color = colors[index];
        }       

        switch (index) {
            case 0:
                dataObject.DesertPos.Add(player.transform.position);
                break;
            case 1:
                dataObject.VillagePos.Add(player.transform.position);
                break;
            case 2:
                dataObject.QuinnPos.Add(player.transform.position);
                break;
            case 3:
                dataObject.QuarryPos.Add(player.transform.position);
                break;
        }
    }

    public void ClearData() {
        dataObject.DesertPos.Clear();
        dataObject.VillagePos.Clear();
        dataObject.QuinnPos.Clear();
        dataObject.QuarryPos.Clear();
    }

    private void OnApplicationQuit() {

    }
}
