using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    public int planetNumber;
    public int planetValue;
    public int planetHealth;
    public float speedMod;
    private float planetScale;
    bool giveFeedback;
    private Vector2 planetFeedbackScale;
    private int scaleTimer;
    GameManager mngr;
	
	private AudioSource audioSrc;
	
	void Awake() {
		mngr = GameObject.Find("GameManager").GetComponent<GameManager>();
		audioSrc = mngr.GetComponent<AudioSource>();
	}
	
	// Use this for initialization
	void Start () {
        
        scaleTimer = 0;
        giveFeedback = false;
        switch (planetNumber)
        {
            case 1:
                planetValue = 1;
                speedMod = Random.Range(0.8f, 1.5f);
                planetHealth = 9;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
            case 2:
                planetValue = 10;
                speedMod = Random.Range(1.5f, 1.9f);
                planetHealth = 14;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
            case 3:
                planetValue = 50;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 19;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 4:
                planetValue = 50;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 19;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 5:
                planetValue = 50;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 19;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 6:
                planetValue = 50;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 19;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 7:
                planetValue = 50;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 19;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 8:
                planetValue = 50;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 19;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
            case 9:
                planetValue = -10;
                speedMod = Random.Range(0.8f, 1.5f);
                planetHealth = 4;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
            default:
                break;
        }

        this.transform.localScale *= planetScale;
        planetFeedbackScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        clickFeedback();
    }

    private void OnMouseDown()
    {
		audioSrc.PlayOneShot(mngr.crumbleSound, 0.8f);
        planetHealth--;
        giveFeedback = true;
        finalClick();
        mngr.SellResource(planetValue);
    }

    void clickFeedback()
    {
        if (giveFeedback)
        {
            if (scaleTimer <= 4)
            {
                planetFeedbackScale.x += .005f;
                planetFeedbackScale.y += .005f;
            }
            else if(scaleTimer>=5 && scaleTimer < 10)
            {
                planetFeedbackScale.x -= .0051f;
                planetFeedbackScale.y -= .0051f;
            }
            else
            {
                scaleTimer = 0;
                giveFeedback = false;
            }
            scaleTimer++;
            transform.localScale = planetFeedbackScale;
        }
    }

    private void finalClick()
    {
        if (planetHealth == -1)
        {
			audioSrc.PlayOneShot(mngr.explosionSound, 0.2f);
            this.planetValue = this.planetValue * 10;
        }
    }
}
