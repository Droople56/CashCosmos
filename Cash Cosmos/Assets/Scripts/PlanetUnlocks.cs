using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUnlocks : MonoBehaviour
{
    GameManager manager;
    int planetNum;
    int planetsUnlocked;

    float planetCost = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<GameManager>();
        planetNum = manager.planetsPurchased;

        GetUnlockPrefs();
    }

    void GetUnlockPrefs()
    {
        if (PlayerPrefs.HasKey("planetsUnlocked"))
            planetsUnlocked = PlayerPrefs.GetInt("planetsUnlocked");
        else
            planetsUnlocked = 0;

        if (PlayerPrefs.HasKey("planetCost"))
            planetCost = PlayerPrefs.GetFloat("planetCost");
        else
            planetCost = 1000f;
    }
    
    public void ResetUnlocks()
    {
        planetsUnlocked = 0;
        PlayerPrefs.SetInt("planetsUnlocked", planetsUnlocked);
    }

    public void UnlockPlanet(int unlockNum)
    {
        if (unlockNum == planetsUnlocked + 1 && manager.currency >= planetCost)
        {
            planetsUnlocked++;
            PlayerPrefs.SetInt("planetsUnlocked", planetsUnlocked);

            planetCost *= 10;
            PlayerPrefs.SetFloat("planetCost", planetCost);
        }
        else
        {
            //error pop-up
        }
    }
}
