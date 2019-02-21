using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float currency;
    private float modifier;

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

	// Use this for initialization
	void Start () {
        currency = 0;
        modifier = 1.0f;

        currencyText = GameObject.Find("CurrencyText").GetComponent<Text>();

        idleUpgrade = false;
        idleCost = 100;
        currentTime = 0.0f;
        workTime = 15.0f;
        workValue = 15;


        //tree price and upgrades
        lumberPrice = 1.0f;
        lumberUpgradePrice = 10.0f;
        treeGrowthFactor = 1.0f;

        fertilizerAmount = 1;
        fertilizerCost = Mathf.Pow(fertilizerAmount, 3) * 200;

        lumberText = GameObject.Find("LumberText").GetComponent<Text>();
        lumberUpgradeButton = GameObject.Find("LumberUpgradeButton").GetComponent<Button>();
        lumberUpgradeButton.transform.GetChild(0).GetComponent<Text>().text = LUMBER_STR + 1;
        lumberUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + lumberUpgradePrice;

        fertilizerUpgradeButton = GameObject.Find("FertilizerUpgradeButton").GetComponent<Button>();
        fertilizerUpgradeButton.transform.GetChild(0).GetComponent<Text>().text = "Double Tree Growth";
        fertilizerUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + fertilizerCost;

        spaceLumberjacksButton = GameObject.Find("SpaceLumberjacksButton").GetComponent<Button>();
        spaceLumberjacksButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost;

    }
	
	// Update is called once per frame
	void Update () {
        currencyText.text = "Monies: $" + currency;
        idleProfit();
	}

    public void SellLumber(int value)
    {
        currency = lumberPrice + value + currency;
    }

    //when the lumber price upgraded
    public void IncreaseLumberPrice(float amount)
    {
        if (currency >= lumberUpgradePrice)
        {
            currency -= lumberUpgradePrice;
            lumberPrice += amount;
        }

        lumberUpgradePrice = Mathf.Pow(lumberPrice, 2) * 10;
        lumberUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + lumberUpgradePrice;
        lumberText.text = "Lumber Price: $" + lumberPrice;
    }

    public void IncreaseFertilizer(float amount)
    {
        if(currency >= fertilizerCost)
        {
            currency -= fertilizerCost;
            treeGrowthFactor *= 2.0f;
        }

        fertilizerAmount++;
        fertilizerCost =  Mathf.Pow(fertilizerAmount, 3) * 200;
        fertilizerUpgradeButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + fertilizerCost;
    }

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
            }
            currency -= idleCost;
            idleCost *= 2;
        }
        spaceLumberjacksButton.transform.GetChild(1).GetComponent<Text>().text = "Cost: $" + idleCost;
    }
}
