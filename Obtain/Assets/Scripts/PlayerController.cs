using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    float horizontal, vertical;
    Vector3 cameraForward, cameraRight;

    public CameraController camera;


	void Start () {
		
	}
	void Update () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        cameraForward = vertical * camera.gameObject.transform.forward;
        cameraRight = horizontal * camera.gameObject.transform.right;

        transform.Translate((cameraForward + cameraRight) / 100 * speed);
		
	}
}
