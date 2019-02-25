using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float currency;
    //private float modifier;

    private float lumberPrice;
    float lumberUpgradePrice;

    public float treeGrowthFactor;
    float fertilizerCost;
    int fertilizerAmount;

    const string LUMBER_STR = "Lumber Price + $";

    Text currencyText;
    Text lumberText;
    Button lumberUpgradeButton;
    Button fertilizerUpgradeButton;
    Button spaceLumberjacksButton;

    //for idle
    bool idleUpgrade;
    float idleCost;
    float currentTime;
    float workTime;
    int workValue;

    public List<GameObject> planets;
    public GameObject planet1;
    public GameObject planet2;
    public GameObject planet3;
    public GameObject planet4;

    // Use this for initialization
    void Start () {

        checkPlayerPrefs();
        //modifier = 1.0f;

        currencyText = GameObject.Find("CurrencyText").GetComponent<Text>();

        
        idleUpgrade = false;



        currentTime = 0.0f;
        workTime = 15.0f;



        lumberText = GameObject.Find("LumberText").GetComponent<Text>();
        lumberUpgradeButton = GameObject.Find("LumberUpgradeButton").GetComponent<Button>();
        lumberUpgradeButton.transform.GetChild(0).GetComponent<Text>().text = LUMBER_STR + 1;
        lumberUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + lumberUpgradePrice;

        fertilizerUpgradeButton = GameObject.Find("FertilizerUpgradeButton").GetComponent<Button>();
        fertilizerUpgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Double Tree Growth";
        fertilizerUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + fertilizerCost;

        spaceLumberjacksButton = GameObject.Find("SpaceLumberjacksButton").GetComponent<Button>();
        spaceLumberjacksButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost;

        //start with random planets
        int rand = Random.Range(1, 4);
        GameObject thisPlanet;
        switch (rand)
        {
            case 1:
                thisPlanet = Instantiate(planet1, new Vector3(Random.Range(-3.5f,2.5f), Random.Range(-4.0f, 0.75f), 0), Quaternion.identity);
                planets.Add(thisPlanet);
                break;
            case 2:
                thisPlanet = Instantiate(planet2, new Vector3(Random.Range(-3.5f, 2.5f), Random.Range(-4.0f, 0.75f), 0), Quaternion.identity);
                planets.Add(thisPlanet);
                break;
            case 3:
                thisPlanet = Instantiate(planet3, new Vector3(Random.Range(-3.5f, 2.5f), Random.Range(-4.0f, 0.75f), 0), Quaternion.identity);
                planets.Add(thisPlanet);
                break;
            case 4:
                thisPlanet = Instantiate(planet4, new Vector3(Random.Range(-3.5f, 2.5f), Random.Range(-4.0f, 0.75f), 0), Quaternion.identity);
                planets.Add(thisPlanet);
                break;
            default:
                break;
        }

    }
	
    //checks for saved data on start
    public void checkPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("currency"))
            currency = PlayerPrefs.GetFloat("currency");
        else
            currency = 0;

        if (PlayerPrefs.HasKey("idleCost"))
        {
            idleCost = PlayerPrefs.GetFloat("idleCost");
            idleUpgrade = true;
        }
        else
            idleCost = 100;

        //tree price and upgrades
        if (PlayerPrefs.HasKey("lumberPrice"))
            lumberPrice = PlayerPrefs.GetFloat("lumberPrice");
        else
            lumberPrice = 1.0f;

        if (PlayerPrefs.HasKey("lumberUpgradePrice"))
            lumberUpgradePrice = PlayerPrefs.GetFloat("lumberUpgradePrice");
        else
            lumberUpgradePrice = 10.0f;

        if (PlayerPrefs.HasKey("treeGrowthFactor"))
            treeGrowthFactor = PlayerPrefs.GetFloat("treeGrowthFactor");
        else
            treeGrowthFactor = 1.0f;

        if (PlayerPrefs.HasKey("workValue"))
            workValue = PlayerPrefs.GetInt("workValue", workValue);
        else
            workValue = 15;

        if (PlayerPrefs.HasKey("fertilizerAmount"))
            fertilizerAmount = PlayerPrefs.GetInt("fertilizerAmount");
        else
            fertilizerAmount = 1;

        if (PlayerPrefs.HasKey("fertilizerCost"))
            fertilizerCost = PlayerPrefs.GetFloat("fertilizerCost");
        else
            fertilizerCost = Mathf.Pow(fertilizerAmount, 3) * 200;
    }

	// Update is called once per frame
	void Update () {
        currencyText.text = "Monies: $" + currency;
        
        idleProfit();
        scrollingPlanets();
        PlayerPrefs.SetFloat("currency",currency);
	}


    public void SellLumber(int value)
    {
        currency = lumberPrice + value + currency;
        PlayerPrefs.SetFloat("currency", currency);
    }

    //when the lumber price upgraded
    public void IncreaseLumberPrice(float amount)
    {
        if (currency >= lumberUpgradePrice)
        {
            currency -= lumberUpgradePrice;
            lumberPrice += amount;
            PlayerPrefs.SetFloat("lumberPrice", lumberPrice);
        }

        lumberUpgradePrice = Mathf.Pow(lumberPrice, 2) * 10;
        lumberUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + lumberUpgradePrice;
        lumberText.text = "Lumber Price: $" + lumberPrice;
        PlayerPrefs.SetFloat("lumberUpgradePrice", lumberUpgradePrice);
    }

    public void IncreaseFertilizer(float amount)
    {
        if(currency >= fertilizerCost)
        {
            currency -= fertilizerCost;
            treeGrowthFactor *= 2.0f;

            PlayerPrefs.SetFloat("treeGrowthFactor", treeGrowthFactor);
        }

        fertilizerAmount++;
        PlayerPrefs.SetInt("fertilizerAmount", fertilizerAmount);
        fertilizerCost =  Mathf.Pow(fertilizerAmount, 3) * 200;
        fertilizerUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + fertilizerCost;
        PlayerPrefs.SetFloat("fertilizerCost", fertilizerCost);
    }

    //Add Money on an interval
    public void idleProfit()
    {
        if (idleUpgrade)
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
        }
    }

    //upgrade idle button code
    public void idleButton()
    {
        if (currency >= idleCost)
        {
            if (idleUpgrade == false)
            {
                idleUpgrade = true;
            }
            else
            {
                workValue += 15;
                PlayerPrefs.SetInt("workValue", workValue);
            }
            currency -= idleCost;
            idleCost *= 2;
            PlayerPrefs.SetFloat("idleCost", idleCost);
        }
        spaceLumberjacksButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost;
    }

    //Scrolling Planets Code
    public void scrollingPlanets()
    {
        if (planets.Count > 0)
        {
            foreach (GameObject planet in planets)
            {
                planet.transform.position -= new Vector3(0.005f, 0, 0);
            }
        }
        spawnPlanet();
        deletePlanet();
    }

    //Spawn New Planets
    public void spawnPlanet()
    {
        if (planets.Count < 5)
        {
            int rand = Random.Range(1, 4);
            //Default Case
            GameObject thisPlanet;
            switch (rand)
            {
                case 1:
                    thisPlanet = Instantiate(planet1, new Vector3(3.5f, Random.Range(-4.0f, 0.75f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 2:
                    thisPlanet = Instantiate(planet2, new Vector3(3.5f, Random.Range(-4.0f, 0.75f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 3:
                    thisPlanet = Instantiate(planet3, new Vector3(3.5f, Random.Range(-4.0f, 0.75f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                case 4:
                    thisPlanet = Instantiate(planet4, new Vector3(3.5f, Random.Range(-4.0f, 0.75f), 0), Quaternion.identity);
                    planets.Add(thisPlanet);
                    break;
                default:
                    break;
            }
            
        }
    }

    //Cleanup Planets
    public void deletePlanet()
    {
        foreach (GameObject planet in planets)
        {
            if (planet.transform.position.x < -3.5)
            {
                GameObject thisPlanet = planet;
                planets.Remove(planet);
                Destroy(thisPlanet);
            }
        }
    }
}


