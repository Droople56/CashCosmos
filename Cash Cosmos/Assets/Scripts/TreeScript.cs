using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    //float GROW_TIME;
    //bool growing;
    //float currentTime;

    public int planetNumber;
    public int planetValue;
    public int planetHealth;
    public float speedMod;

    GameManager mngr;

	// Use this for initialization
	void Start () {

        switch (planetNumber)
        {
            case 1:
                planetValue = 1;
                speedMod = 1.0f;
                planetHealth = 10;
                break;
            case 2:
                planetValue = 10;
                speedMod = 1.5f;
                planetHealth = 15;
                break;
            case 3:
                planetValue = 50;
                speedMod = 2.0f;
                planetHealth = 20;
                break;
            case 4:
                planetValue = -10;
                speedMod = 1.0f;
                planetHealth = 5;
                break;
            default:
                break;
        }

        mngr = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //This Gives the player too much money since they can hold teh mouse down to continue selling, suggestions?
        /*if (planetHealth == 1)
        {
            planetValue = planetValue * 10;
        }*/
	}

    private void OnMouseDown()
    {
        planetHealth--;
        mngr.SellResource(planetValue);
    }

    
}
