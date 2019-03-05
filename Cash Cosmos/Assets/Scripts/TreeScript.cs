using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    public int planetNumber;
    public int planetValue;
    public int planetHealth;
    public float speedMod;
    private float planetScale;

    GameManager mngr;

	// Use this for initialization
	void Start () {

        switch (planetNumber)
        {
            case 1:
                planetValue = 1;
                speedMod = Random.Range(0.8f, 1.5f);
                planetHealth = 9;
                planetScale = Random.Range(0.3f, 0.6f);
                break;
            case 2:
                planetValue = 10;
                speedMod = Random.Range(1.5f, 1.9f);
                planetHealth = 14;
                planetScale = Random.Range(0.3f, 0.6f);
                break;
            case 3:
                planetValue = 50;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 19;
                planetScale = Random.Range(0.3f, 0.6f);
                break;
            case 4:
                planetValue = -10;
                speedMod = Random.Range(0.8f, 1.5f);
                planetHealth = 4;
                planetScale = Random.Range(0.3f, 0.6f);
                break;
            default:
                break;
        }

        this.transform.localScale *= planetScale;

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
