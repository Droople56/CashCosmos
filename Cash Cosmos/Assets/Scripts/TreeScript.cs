using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    const float GROW_TIME = 4.0f;
    bool growing;
    float currentTime;

    GameManager mngr;

	// Use this for initialization
	void Start () {
        growing = false;
        currentTime = 0.0f; //used to reset the trees once they're cut down

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
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                currentTime += Time.fixedDeltaTime;
            }
            else
            {
                growing = false;
                currentTime = 0.0f;
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void OnMouseEnter()
    {
        growing = true;
        mngr.SellLumber();
    }
}
