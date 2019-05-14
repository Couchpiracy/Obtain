using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAreaWeights : MonoBehaviour {
    
    public enum AreaType {Red, Green, Blue};
    public AreaType type;
    public float inWeight, outWeight;
    private float targetWeight, currentWeight;
    private string parameterName;

    public GameObject player;
    

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            parameterName = type + "_Weight";
            //AkSoundEngine.SetRTPCValue(parameterName, inWeight, GameObject.FindGameObjectWithTag("Player"));
            StartCoroutine(LerpValue(parameterName, inWeight));
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform.tag == "Player") {
            parameterName = type + "_Weight";
            //AkSoundEngine.SetRTPCValue(parameterName, outWeight, GameObject.FindGameObjectWithTag("Player"));
            StartCoroutine(LerpValue(parameterName, outWeight));
        }
    }

    private IEnumerator LerpValue(string name, float target) {
        targetWeight = target;

        print(Mathf.Abs(currentWeight - targetWeight));

        print("Current: " + currentWeight + " Target: " + targetWeight);
        while (Mathf.Abs(currentWeight - targetWeight) > 0.1f) {
            currentWeight = Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime);
            //AkSoundEngine.SetRTPCValue(parameterName, currentWeight);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return 0;
    }

}
