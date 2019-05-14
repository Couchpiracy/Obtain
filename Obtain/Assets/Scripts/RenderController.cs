using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderController : MonoBehaviour {

    private GameObject renderControl;
    private RenderList renderList;

	void Start () {
        renderControl = GameObject.Find("RenderControl");
        renderList = renderControl.GetComponent<RenderList>();
        renderList.renderedItems.Add(new RenderedItem { item = this.gameObject, itemPos = transform.position });
	}
}
