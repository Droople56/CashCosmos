using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    float GROW_TIME;
    bool growing;
    float currentTime;
    public int planetNumber;
    int planetValue;

    GameManager mngr;

	// Use this for initialization
	void Start () {
        growing = false;
        currentTime = 0.0f; //used to reset the trees once they're cut down

        switch (planetNumber)
        {
            case 1:
                planetValue = 1;
                GROW_TIME = 4.0f;
                break;
            case 2:
                planetValue = 10;
                GROW_TIME = 6.0f;
                break;
            case 3:
                planetValue = 50;
                GROW_TIME = 8.0f;
                break;
            case 4:
                planetValue = 100;
                GROW_TIME = 10.0f;
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
        growing = true;
        mngr.SellLumber(planetValue);
    }
}
