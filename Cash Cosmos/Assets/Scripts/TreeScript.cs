using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

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
                planetHealth = 9;
                break;
            case 2:
                planetValue = 10;
                speedMod = 1.5f;
                planetHealth = 14;
                break;
            case 3:
                planetValue = 50;
                speedMod = 2.0f;
                planetHealth = 19;
                break;
            case 4:
                planetValue = -10;
                speedMod = 1.0f;
                planetHealth = 4;
                break;
            default:
                break;
        }

        mngr = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnMouseDown()
    {
        planetHealth--;
        finalClick();
        mngr.SellResource(planetValue);
    }

    private void finalClick()
    {
        if (planetHealth == -1)
        {
            this.planetValue = this.planetValue * 10;
        }
    }
}
