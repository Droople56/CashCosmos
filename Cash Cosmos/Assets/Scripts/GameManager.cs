//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float currency;
    //private float modifier;

    private float resourcePrice;
    float resourceUpgradePrice;

    float planetUpgradeCost;
    int planetUpgradeAmount;

    const int MAX_PLANETS = 10;
    int numPlanets;

    Text currencyText;
    Text resourceText;
    Button resourceUpgradeButton;
    Button planetUpgradeButton;

    //idle1
    float currentTime;
    float idleCost;
    public float workTime;
    float workValue;
    public float workIncrement;
    Button spaceLumberjacksButton;

    //for idle2
    float currentTime2;
    float idleCost2;
    public float workTime2;
    float workValue2;
    public float workIncrement2;
    Button idle2Button;

    //for idle3
    float currentTime3;
    float idleCost3;
    public float workTime3;
    float workValue3;
    public float workIncrement3;
    Button idle3Button;

    //click strength
    [HideInInspector]
    public int clickUpgradeAmount;
    float clickUpgradeCost;
    public float clickUpgradeIncrement;
    Button clickUpgradeButton;

    public List<GameObject> planets;
    public List<GameObject> destroyedPlanets;
    public GameObject currentBackground;
    public GameObject background;
    GameObject bgTemp;
	public GameObject[] planetList;
	
	public AudioClip crumbleSound;
	public AudioClip explosionSound;

    long lastShutdownTime;
    long currentTimeIdle;

    public Text planetsText;
    public Text resourceValueText;
    public Text tapPowerText;
    public Text lumberjacksText;
    public Text minersText;
    public Text mercenariesText;

    // Use this for initialization
    void Start () {
        checkPlayerPrefs();

        numPlanets = 5;

        currencyText = GameObject.Find("CurrencyText").GetComponent<Text>();

        currentBackground = GameObject.Find("space_01");
        bgTemp = null;

        currentTime = 0.0f;
        currentTime2 = 0.0f;
        currentTime3 = 0.0f;
        
        resourceText = GameObject.Find("ResourceText").GetComponent<Text>();
        resourceText.text = "Resource Price: $" + resourcePrice;

        resourceUpgradeButton = GameObject.Find("ResourceUpgradeButton").GetComponent<Button>();
        resourceUpgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Resource Value + $" + 1;
        resourceUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + resourceUpgradePrice;

        planetUpgradeButton = GameObject.Find("PlanetUpgradeButton").GetComponent<Button>();
        planetUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + planetUpgradeCost;

        clickUpgradeButton = GameObject.Find("ClickUpgradeButton").GetComponent<Button>();
        clickUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + clickUpgradeCost;

        //idle buttons
        spaceLumberjacksButton = GameObject.Find("SpaceLumberjacksButton").GetComponent<Button>();
        spaceLumberjacksButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost;

        idle2Button = GameObject.Find("idle2Button").GetComponent<Button>();
        idle2Button.transform.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost2;

        idle3Button = GameObject.Find("idle3Button").GetComponent<Button>();
        idle3Button.transform.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost3;

        //start with random planets
        for (int i = 0; i < numPlanets; i++)
        {
            int rand = Random.Range(0, 8);
            GameObject thisPlanet;
			thisPlanet = Instantiate(planetList[rand], new Vector3(Random.Range(-3.5f, 2.5f), Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
            planets.Add(thisPlanet);
			/*
            switch (rand)
            {
                case 1:
                    thisPlanet = Instantiate(planet1, new Vector3(Random.Range(-3.5f, 2.5f), Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 2:
                    thisPlanet = Instantiate(planet2, new Vector3(Random.Range(-3.5f, 2.5f), Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 3:
                    thisPlanet = Instantiate(planet3, new Vector3(Random.Range(-3.5f, 2.5f), Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 4:
                    thisPlanet = Instantiate(planet4, new Vector3(Random.Range(-3.5f, 2.5f), Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                default:
                    break;
            }*/
        }
        
    }
	
    //checks for saved data on start
    public void checkPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("clickUpgradeAmount"))
            clickUpgradeAmount = PlayerPrefs.GetInt("clickUpgradeAmount");
        else
            clickUpgradeAmount = 1;

        if (PlayerPrefs.HasKey("clickUpgradeCost"))
            clickUpgradeCost = PlayerPrefs.GetFloat("clickUpgradeCost");
        else
            clickUpgradeCost = clickUpgradeIncrement;

        if (PlayerPrefs.HasKey("currency"))
            currency = PlayerPrefs.GetFloat("currency");
        else
            currency = 0;

        //idle costs
        if (PlayerPrefs.HasKey("idleCost"))
        {
            idleCost = PlayerPrefs.GetFloat("idleCost");
        }
        else
            idleCost = 100;

        if (PlayerPrefs.HasKey("idleCost2"))
        {
            idleCost2 = PlayerPrefs.GetFloat("idleCost2");
        }
        else
            idleCost2 = 1000;

        if (PlayerPrefs.HasKey("idleCost3"))
        {
            idleCost3 = PlayerPrefs.GetFloat("idleCost3");
        }
        else
            idleCost3 = 10000;

        //tree price and upgrades
        if (PlayerPrefs.HasKey("resourcePrice"))
            resourcePrice = PlayerPrefs.GetFloat("resourcePrice");
        else
            resourcePrice = 1.0f;

        if (PlayerPrefs.HasKey("resourceUpgradePrice"))
            resourceUpgradePrice = PlayerPrefs.GetFloat("resourceUpgradePrice");
        else
            resourceUpgradePrice = 10.0f;

        //idle work values
        if (PlayerPrefs.HasKey("workValue"))
            workValue = PlayerPrefs.GetFloat("workValue", workValue);
        else
            workValue = 0.0f;

        if (PlayerPrefs.HasKey("workValue2"))
            workValue2 = PlayerPrefs.GetFloat("workValue2", workValue2);
        else
            workValue2 = 0.0f;

        if (PlayerPrefs.HasKey("workValue3"))
            workValue3 = PlayerPrefs.GetFloat("workValue3", workValue3);
        else
            workValue3 = 0.0f;

        if (PlayerPrefs.HasKey("planetUpgradeAmount"))
            planetUpgradeAmount = PlayerPrefs.GetInt("planetUpgradeAmount");
        else
            planetUpgradeAmount = 1;

        if (PlayerPrefs.HasKey("planetUpgradeCost"))
            planetUpgradeCost = PlayerPrefs.GetFloat("planetUpgradeCost");
        else
            planetUpgradeCost = Mathf.Pow(planetUpgradeAmount, 3) * 200;

        if (PlayerPrefs.HasKey("lastShutdownTime"))
        {
            long.TryParse(PlayerPrefs.GetString("lastShutdownTime", "0"), out lastShutdownTime);
            calculateAwayProfit();
        }
            
    }

    //calculate idle gains between last time app was closed and now
    void calculateAwayProfit()
    {
        //calculates time difference between last shutdown and current time
        System.TimeSpan timePassed = System.DateTime.Now - new System.DateTime(lastShutdownTime);

        float profit = 0.0f;

        //takes seconds past and calculates money earned while away
        if(workValue > 0.0f)
            profit += Mathf.FloorToInt((float)timePassed.TotalSeconds / workTime * workValue);

        if(workValue2 > 0.0f)
            profit += Mathf.FloorToInt((float)timePassed.TotalSeconds / workTime2 * workValue2);

        if(workValue3 > 0.0f)
            profit += Mathf.FloorToInt((float)timePassed.TotalSeconds / workTime3 * workValue3);

        currency += profit;
    }

	// Update is called once per frame
	void Update () {
        currencyText.text = "Monies: $" + System.String.Format("{0:0.0}", System.Math.Round(currency, 1));
        
        idleProfit();
        scrollingPlanets();
        bgCleanup();
        UpdateValues();
        PlayerPrefs.SetFloat("currency",currency);
	}


    public void SellResource(int value)
    {
        currency = resourcePrice + value + currency;
        PlayerPrefs.SetFloat("currency", currency);
    }

    //when the resource price upgraded
    public void IncreaseResourcePrice(float amount)
    {
        if (currency >= resourceUpgradePrice)
        {
            currency -= resourceUpgradePrice;
            resourcePrice += amount;
            PlayerPrefs.SetFloat("resourcePrice", resourcePrice);
        }

        resourceUpgradePrice = Mathf.Pow(resourcePrice, 2) * 10;
        resourceUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + resourceUpgradePrice;
        resourceText.text = "Resource Price: $" + resourcePrice;
        PlayerPrefs.SetFloat("resourceUpgradePrice", resourceUpgradePrice);
    }

    //This is the Add Planet stuff
    public void IncreasePlanetAmount(float amount)
    {
        if (numPlanets == MAX_PLANETS)
        {
            planetUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "MAXED OUT";
        }
        else if (currency >= planetUpgradeCost)
        {
            currency -= planetUpgradeCost;

            if (numPlanets < MAX_PLANETS)
            {
                numPlanets++;
            }

            planetUpgradeAmount++;
            PlayerPrefs.SetInt("planetUpgradeAmount", planetUpgradeAmount);
            planetUpgradeCost = Mathf.Pow(planetUpgradeAmount, 3) * 200;
            planetUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + planetUpgradeCost;
            PlayerPrefs.SetFloat("planetUpgradeCost", planetUpgradeCost);
        }
    }

    public void IncreaseClickStrength()
    {
        if(currency >= clickUpgradeCost)
        {
            currency -= clickUpgradeCost;
            clickUpgradeAmount++;
            PlayerPrefs.SetInt("clickUpgradeAmount", clickUpgradeAmount);

            if(clickUpgradeAmount >= 2)
            {
                clickUpgradeCost = clickUpgradeAmount * 50000 * 10;
            }
            else
            {
                clickUpgradeCost = clickUpgradeAmount * 50000;
            }

            PlayerPrefs.SetFloat("clickUpgradeCost", clickUpgradeCost);
            clickUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + clickUpgradeCost;
        }
    }


    //Add Money on an interval
    public void idleProfit()
    {
        if (workValue > 0.0f)
        {
            if (currentTime > workTime)
            {
                currency += workValue;
                currentTime = 0.0f;
            }
            else
            {
                currentTime += Time.fixedDeltaTime;
            }
            
            PlayerPrefs.SetString("lastShutdownTime", System.DateTime.Now.Ticks.ToString());
        }

        if (workValue2 > 0.0f)
        {
            if (currentTime2 > workTime2)
            {
                currency += workValue2;
                currentTime2 = 0.0f;
            }
            else
            {
                currentTime2 += Time.fixedDeltaTime;
            }

            PlayerPrefs.SetString("lastShutdownTime", System.DateTime.Now.Ticks.ToString());
        }

        if (workValue3 > 0.0f)
        {
            if (currentTime3 > workTime3)
            {
                currency += workValue3;
                currentTime3 = 0.0f;
            }
            else
            {
                currentTime3 += Time.fixedDeltaTime;
            }

            PlayerPrefs.SetString("lastShutdownTime", System.DateTime.Now.Ticks.ToString());
        }
    }

    //upgrade idle button code
    public void idleButton()
    {
        if (currency >= idleCost)
        {
            workValue += workIncrement;
            PlayerPrefs.SetFloat("workValue", workValue);
            currency -= idleCost;
            idleCost *= 2;
            PlayerPrefs.SetFloat("idleCost", idleCost);
        }
        spaceLumberjacksButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost;
    }

    public void idleButton2()
    {
        if(currency >= idleCost2)
        {
            workValue2 += workIncrement2;
            PlayerPrefs.SetFloat("workValue2", workValue2);
            currency -= idleCost2;
            idleCost2 *= 2;
            PlayerPrefs.SetFloat("idleCost2", idleCost2);
        }
        idle2Button.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost2;
    }

    public void idleButton3()
    {
        if (currency >= idleCost3)
        {
            workValue3 += workIncrement3;
            PlayerPrefs.SetFloat("workValue3", workValue3);
            currency -= idleCost3;
            idleCost3 *= 2;
            PlayerPrefs.SetFloat("idleCost3", idleCost3);
        }
        idle3Button.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost3;
    }

    //Scrolling Planets Code
    public void scrollingPlanets()
    {
        if (planets.Count > 0)
        {
            foreach (GameObject planet in planets)
            {
                float speed = 0.005f / planet.GetComponent<TreeScript>().speedMod;
                planet.transform.position -= new Vector3(speed, 0, 0);
            }
        }
        spawnPlanet();
        deletePlanet();
    }

    //Spawn New Planets
    public void spawnPlanet()
    {
        if (planets.Count < numPlanets)
        {
            int rand = Random.Range(0, 8);
            GameObject thisPlanet; //Default Case
			thisPlanet = Instantiate(planetList[rand], new Vector3(3.5f, Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
            planets.Add(thisPlanet);
			/*
            switch (rand)
            {
                case 1:
                    thisPlanet = Instantiate(planet1, new Vector3(3.5f, Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 2:
                    thisPlanet = Instantiate(planet2, new Vector3(3.5f, Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 3:
                    thisPlanet = Instantiate(planet3, new Vector3(3.5f, Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 4:
                    thisPlanet = Instantiate(planet4, new Vector3(3.5f, Random.Range(-4.35f, 0.9f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                default:
                    break;
            }*/
            
        }
    }

    //Cleanup Planets
    public void deletePlanet()
    {
        //I had crash problem when I tried to run this all in one foreach, so I did this scuff workaround.
        foreach (GameObject planet in planets)
        {
            if (planet.GetComponent<TreeScript>().planetHealth < 0)
            {
                //planet.GetComponent<TreeScript>().planetValue *= 10;
                destroyedPlanets.Add(planet);
            }
            else if (planet.transform.position.x < -3.5)
            {
                destroyedPlanets.Add(planet);
            }
        }
        foreach (GameObject planet in destroyedPlanets)
        {
            planets.Remove(planet);
            Destroy(planet);
        }
        destroyedPlanets.RemoveRange(0, destroyedPlanets.Count);
    }

    //swap out long parallax bg
    public void bgCleanup()
    {
        if (currentBackground.transform.position.x < -9.65)
        {
            bgTemp = currentBackground;
            currentBackground = Instantiate(background, new Vector3(14.6f, -1.78f, 1), Quaternion.identity);
        }
        if (bgTemp != null && bgTemp.transform.position.x < -14.6)
        {
            Destroy(bgTemp);
        }
    }

    //set variables to their initial values
    public void ResetUpgrades()
    {
        currency = 0;

        resourcePrice = 1.0f;
        resourceUpgradePrice = 10.0f;

        planetUpgradeAmount = 1;
        planetUpgradeCost = Mathf.Pow(planetUpgradeAmount, 3) * 200;
        numPlanets = 5;

        clickUpgradeAmount = 1;
        clickUpgradeCost = clickUpgradeIncrement;

        //Update button texts
        resourceText.text = "Resource Price: $" + resourcePrice;
        resourceUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + resourceUpgradePrice;
        planetUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + planetUpgradeCost;
        clickUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + clickUpgradeCost;
        spaceLumberjacksButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost;
        idle2Button.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost2;
        idle3Button.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost3;

        //Update PlayerPrefs
        PlayerPrefs.SetFloat("currency", currency);
        PlayerPrefs.SetFloat("resourcePrice", resourcePrice);
        PlayerPrefs.SetFloat("resourceUpgradePrice", resourceUpgradePrice);
        PlayerPrefs.SetInt("planetUpgradeAmount", planetUpgradeAmount);
        PlayerPrefs.SetFloat("planetUpgradeCost", planetUpgradeCost);
        PlayerPrefs.SetInt("clickUpgradeAmount", clickUpgradeAmount);
        PlayerPrefs.SetFloat("clickUpgradeCost", clickUpgradeCost);

        //reset workValues
        workValue = 0.0f;
        workValue2 = 0.0f;
        workValue3 = 0.0f;
        PlayerPrefs.SetFloat("workValue", workValue);
        PlayerPrefs.SetFloat("worKValue2", workValue2);
        PlayerPrefs.SetFloat("worKValue3", workValue3);

        //reset idleCosts
        idleCost = 100;
        idleCost2 = 1000;
        idleCost3 = 10000;
        PlayerPrefs.SetFloat("idleCost", idleCost);
        PlayerPrefs.SetFloat("idleCost2", idleCost2);
        PlayerPrefs.SetFloat("idleCost3", idleCost3);
    }

    public void UpdateValues()
    {
        planetsText.GetComponent<Text>().text = "Planets: " + numPlanets;
        resourceValueText.GetComponent<Text>().text = "Resource Value: " + resourcePrice;
        tapPowerText.GetComponent<Text>().text = "Tap Power: " + clickUpgradeAmount;
        lumberjacksText.GetComponent<Text>().text = "Lumberjack Value: " + workValue;
        minersText.GetComponent<Text>().text = "Miner Value: " + workValue2;
        mercenariesText.GetComponent<Text>().text = "Mercenary Value: " + workValue3;
    }
}


