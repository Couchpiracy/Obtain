using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderList : MonoBehaviour {

    [SerializeField] private int maxDistanceFromPlayer;

    private GameObject player;

    public List<RenderedItem> renderedItems;


	void Start () {
        player = GameObject.Find("Player");
        renderedItems = new List<RenderedItem>();

        StartCoroutine(RenderCheck());
	}

    IEnumerator RenderCheck() {
        while(true) {
            List<RenderedItem> removeList = new List<RenderedItem>();

            if(renderedItems.Count > 0) {
                foreach(RenderedItem entry in renderedItems) {
                    if(Vector3.Distance(player.transform.position, entry.itemPos) > maxDistanceFromPlayer) {
                        if(entry.item == null) {
                            removeList.Add(entry);
                            }
                        else {
                            entry.item.SetActive(false);
                            }
                        }
                    else {
                        if(entry.item == null) {
                            removeList.Add(entry);
                            }
                        else {
                            entry.item.SetActive(true);
                            }
                        }
                    }
                }
            yield return new WaitForSeconds(Time.deltaTime);
            }
        }
}

public class RenderedItem {

    public GameObject item;
    public Vector3 itemPos;
    }
