using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUnlocks : MonoBehaviour
{
    UnlockTile[] planetTiles = new UnlockTile[6];
    public UnlockTile tile1;
    public UnlockTile tile2;
    public UnlockTile tile3;
    public UnlockTile tile4;
    public UnlockTile tile5;
    public UnlockTile tile6;

    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<GameManager>();

        planetTiles[0] = tile1;
        planetTiles[1] = tile2;
        planetTiles[2] = tile3;
        planetTiles[3] = tile4;
        planetTiles[4] = tile5;
        planetTiles[5] = tile6;

        planetTiles[0].isPurchaseable = true;

        GetUnlockPrefs();
    }

    public void GetUnlockPrefs()
    {
        if (PlayerPrefs.HasKey("numPlanets"))
        {
            PlayerPrefs.SetInt("numPlanets", manager.planetsPurchased);

            for (int i = 0; i < manager.planetsPurchased - 2; i++)
            {
                planetTiles[i].unlocked = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("numPlanets", 2);
            manager.planetsPurchased = 2;
        }

    }
    
    public void ResetUnlocks()
    {
        for(int i = 0; i < 6; i++)
        {
            if (planetTiles[i].unlocked == true)
            {
                planetTiles[i].unlocked = false;
                planetTiles[i].isPurchaseable = false;
            }
        }
        planetTiles[0].isPurchaseable = true;

        manager.planetsPurchased = 2;
        PlayerPrefs.SetInt("planetsPurchased", manager.planetsPurchased);
    }

    public void UnlockPlanet(int index)
    {
        if (!planetTiles[index].unlocked)
        {
            if (index == 0)
            {
                planetTiles[0].unlocked = true;

                manager.planetsPurchased++;
                PlayerPrefs.SetInt("planetsPurchased", manager.planetsPurchased);

                manager.currency -= planetTiles[0].cost;
                PlayerPrefs.SetFloat("currency", manager.currency);

                planetTiles[1].isPurchaseable = true;

            }
            else if (planetTiles[index - 1].unlocked == true && manager.currency >= planetTiles[index].cost)
            {
                //unlock this planet
                planetTiles[index].unlocked = true;

                manager.planetsPurchased++;
                PlayerPrefs.SetInt("planetsPurchased", manager.planetsPurchased);

                manager.currency -= planetTiles[index].cost;
                PlayerPrefs.SetFloat("currency", manager.currency);

                planetTiles[index + 1].isPurchaseable = true;
            }
            else
            {
                //error pop-up
                Debug.Log("Not Enough Cash");
            }
        }
        else
        {
            Debug.Log("Already Unlocked Planet " + (index + 1));
        }
    }
}
