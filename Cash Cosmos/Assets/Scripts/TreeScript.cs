using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    float GROW_TIME;
    bool growing;
    float currentTime;
    public int planetNumber;
    int planetValue;
    public int planetHealth;
    public float speedMod;
    public bool destroyed;

    GameManager mngr;

	// Use this for initialization
	void Start () {
        growing = false;
        currentTime = 0.0f; //used to reset the trees once they're cut down
        destroyed = false;

        switch (planetNumber)
        {
            case 1:
                planetValue = 1;
                GROW_TIME = 4.0f;
                speedMod = 1.0f;
                planetHealth = 10;
                break;
            case 2:
                planetValue = 10;
                GROW_TIME = 6.0f;
                speedMod = 1.5f;
                planetHealth = 15;
                break;
            case 3:
                planetValue = 50;
                GROW_TIME = 8.0f;
                speedMod = 2.0f;
                planetHealth = 20;
                break;
            case 4:
                planetValue = -10;
                GROW_TIME = 10.0f;
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
        CutDown();
	}

    float timer;

    public void CutDown()
    {
        if (growing)
        {
            if (currentTime < GROW_TIME / mngr.treeGrowthFactor)
            { 
                if (GetComponent<SpriteRenderer>().enabled == true)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<CircleCollider2D>().enabled = false;
                }
                currentTime += Time.fixedDeltaTime;
            }
            else
            {
                growing = false;
                currentTime = 0.0f;
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }

    private void OnMouseDown()
    {
        planetHealth--;
        if (destroyed)
        {
            mngr.SellLumber(planetValue * 10);
        }
        else
        {
            mngr.SellLumber(planetValue);
        }
        
    }

    
}
