using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMotion : MonoBehaviour {

    public GameObject background;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position -= new Vector3(0.001f, 0, 0);
	}
}
