using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour {

    public GameObject buttons;
    public Scrollbar scroller;
    Vector3 originalPos;
    public float maxDist;

    [Tooltip("0: Right to Left\n1:Left to Right\n2:Top to Bottom\n3:Bottom to Top")]
    public int direction = 0;

	// Use this for initialization
	void Start () {
        originalPos = buttons.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 moveVector = Vector3.zero;
        switch (direction)
        {
            case 0:
                moveVector = new Vector3(scroller.value * maxDist, 0);
                break;
            case 1:
                moveVector = new Vector3(-scroller.value * maxDist, 0);
                break;
            case 2:
                moveVector = new Vector3(0, scroller.value * maxDist);
                break;
            case 3:
                moveVector = new Vector3(0, -scroller.value * maxDist);
                break;
            default:
                Debug.Log("Must choose a value between 1 and 3 for direction");
                break;
        }
        buttons.transform.position = originalPos + moveVector;
            
    }
      
}
