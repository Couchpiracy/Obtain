using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentMove : MonoBehaviour {

    public Transform player;
    public Transform destination;
    private float speed;
    private AudioSource source;

    private float minAngle = 90, maxAngle = 180;

    NavMeshAgent _agent;

    void Start() {
        source = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
        speed = _agent.speed;

        if (_agent == null)
            Debug.LogError("No NavMeshAgent attached to " + gameObject.name);
        
    }


    void Update() {
        LimitDistance();
        SetVolume();
    }

    public void SetDestination(Transform target) {
        destination = target;
        _agent.SetDestination(destination.position);
    }

    private void LimitDistance() {
        if ((transform.position - player.transform.position).magnitude >= 3f) {
            _agent.speed = speed;
            _agent.SetDestination(player.position);
        }
        else if (((transform.position - player.transform.position).magnitude >= 2.2f) )/*&& ((transform.position - player.transform.position).magnitude < 4.0f))*/ {
            _agent.speed = 0;
        }
        else {
            _agent.speed = speed;
            _agent.SetDestination(destination.position);
        }
    }

    private void SetVolume() {

        Vector3 playerAngle = player.GetComponentInChildren<CameraController>().transform.forward;
        Vector3 targetAngle = player.transform.position - transform.position;

        //source.volume = Mathf.Clamp(Vector3.Angle(playerAngle, targetAngle), minAngle, maxAngle) * 1 / minAngle - 1;
        if (Vector3.Angle(playerAngle, targetAngle) >= 90) {
            // Byts ut mot snapshot 1
            if (source.volume < 0.25f)
                source.volume += Time.deltaTime;
            else
                source.volume = Mathf.Lerp(source.volume, Vector3.Angle(playerAngle, targetAngle) / 180, Time.deltaTime);
            //
        }
        else
            // Byts ut mot snapshot 2
            source.volume -= Time.deltaTime;
            //

        //print(Vector3.Angle(playerAngle, targetAngle));

        //if(Vector3.Angle(playerAngle, targetAngle))

    }
}
