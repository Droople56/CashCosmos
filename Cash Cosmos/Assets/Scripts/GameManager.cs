using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private float currency;
    private float modifier;
    private bool mod1;
    private bool mod2;

	// Use this for initialization
	void Start () {
        currency = 0;
        modifier = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Calc Modifier
    float calcModifier()
    {
        float total = 1.0f;
        return total;
    }

    //Check modifier upgrades
    void checkUpgrades()
    {

    }
}
