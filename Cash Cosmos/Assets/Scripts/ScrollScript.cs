using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour {

    public GameObject buttons;
    public Scrollbar scroller;
    Vector3 originalPos;
    public float maxDist;

	// Use this for initialization
	void Start () {
        originalPos = buttons.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        buttons.transform.position = originalPos + new Vector3(scroller.value * maxDist,0);
	}
}
