using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockTile : MonoBehaviour
{
    Text description;

    public float cost = 100.0f;
    public float yield = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        description = this.transform.GetChild(1).GetComponent<Text>();
        description.text = "Cost: " + cost + "\nDestruction Profit: " + yield;
    }

}
