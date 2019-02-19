using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float currency;
    private float modifier;
    private bool mod1;
    private bool mod2;

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

	// Use this for initialization
	void Start () {
        currency = 0;
        modifier = 1.0f;

        currencyText = GameObject.Find("CurrencyText").GetComponent<Text>();


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
    }
	
	// Update is called once per frame
	void Update () {
        currencyText.text = "Monies: $" + currency;
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

    public void SellLumber()
    {
        currency += lumberPrice;
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
}
