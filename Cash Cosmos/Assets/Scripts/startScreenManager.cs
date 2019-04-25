using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class startScreenManager : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas tutorialCanvas;
    public Button startButton;
    public Button tutorialButton;
    public Button nextButton;
    public bool doTutorial;
    public GameObject badPlanet;
    public GameObject planet1;
    public GameObject planet2;
    public GameObject planet3;
    public GameObject planet4;
    public GameObject planet5;
    public GameObject planet6;
    public GameObject planet7;
    public GameObject planet8;
    public Vector3 position;
    public int tutorialStep;

    public Text tutorialStepText;
    public Text nextButtonText;
    // Start is called before the first frame update
    void Start()
    {
        doTutorial = false;
        position = badPlanet.transform.position;
        tutorialCanvas.enabled = false;
        tutorialStep = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialStep == 0)
        {
            if (badPlanet.transform.position.x > 0.0f)
                position.x -= .05f;
            else
                position.x = 0.0f;

            badPlanet.transform.position = position;

            tutorialStepText.text = "This is a bad planet. Don't tap it. Tapping any other planet will give you cash until it explodes!";
        }
        else if (tutorialStep == 1)
        {
            tutorialStepText.text = "Once you start to get more cash, you can click on the unlocks button to buy new planets to destroy!";
            badPlanet.transform.position = new Vector3(badPlanet.transform.position.x, badPlanet.transform.position.y, 1.0f);
            planet1.transform.position = new Vector3(planet1.transform.position.x, planet1.transform.position.y, -1.0f);
            planet2.transform.position = new Vector3(planet2.transform.position.x, planet2.transform.position.y, -1.0f);
            planet3.transform.position = new Vector3(planet3.transform.position.x, planet3.transform.position.y, -1.0f);
            planet4.transform.position = new Vector3(planet4.transform.position.x, planet4.transform.position.y, -1.0f);
            planet5.transform.position = new Vector3(planet5.transform.position.x, planet5.transform.position.y, -1.0f);
            planet6.transform.position = new Vector3(planet6.transform.position.x, planet6.transform.position.y, -1.0f);
            planet7.transform.position = new Vector3(planet7.transform.position.x, planet7.transform.position.y, -1.0f);
            planet8.transform.position = new Vector3(planet8.transform.position.x, planet8.transform.position.y, -1.0f);

        }
        else if (tutorialStep == 2)
        {
            tutorialStepText.text = "In the meantime, upgrade your tapping with the buttons in your control panel.";
        }
        else if (tutorialStep == 3)
        {
            tutorialStepText.text = "If you're ready to destroy the livelihoods of every living being in the universe, go ahead and press begin.";

            nextButtonText.text = "Begin";
        }
        else if (tutorialStep > 3)
            startGame();
    }

    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void tutorial()
    {
        mainCanvas.enabled = false;
        tutorialCanvas.enabled = true;
        doTutorial = true;
        tutorialStep = 0;
    }

    public void nextTutorialStep()
    {
        tutorialStep++;

    }
}
