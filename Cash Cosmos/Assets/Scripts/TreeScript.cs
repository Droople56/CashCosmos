using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TreeScript : MonoBehaviour {
	public SpriteRenderer m_sprRender;
	
    public int planetNumber;
    public int planetValue;
    public int planetHealth;
    public float speedMod;
    private float planetScale;
    bool giveFeedback;
	public bool isAlive = true;
    private Vector2 planetFeedbackScale;
    private int scaleTimer;
    GameManager mngr;

    //For paywall
    public bool purchased;
	
	public Sprite[] explosion;
	
    int clickStrength;
	
	private AudioSource audioSrc;
	
	void Awake() {
		mngr = GameObject.Find("GameManager").GetComponent<GameManager>();
		audioSrc = mngr.GetComponent<AudioSource>();
        clickStrength = mngr.clickUpgradeAmount;
	}
	
	// Use this for initialization
	void Start () {
		m_sprRender = GetComponent<SpriteRenderer>();
        
        scaleTimer = 0;
        giveFeedback = false;
        switch (planetNumber)
        {
            //neg planet
            case 0:
                planetValue = -10;
                speedMod = Random.Range(0.8f, 1.5f);
                planetHealth = 4;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
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
                planetValue = 100;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 29;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 5:
                planetValue = 300;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 34;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 6:
                planetValue = 600;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 39;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 7:
                planetValue = 1000;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 44;
                planetScale = Random.Range(0.5f, 0.7f);
                break;
			case 8:
                planetValue = 10000;
                speedMod = Random.Range(2.0f, 2.5f);
                planetHealth = 49;
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
		if (planetHealth > -1) {
			audioSrc.PlayOneShot(mngr.crumbleSound, 0.8f);
			planetHealth -= clickStrength;
			giveFeedback = true;
			finalClick();
			mngr.SellResource(planetValue * clickStrength);
		}
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
        if (planetHealth <= -1)
        {
			audioSrc.PlayOneShot(mngr.explosionSound, 0.2f);
			StartCoroutine(Explosion());
            this.planetValue = this.planetValue * 10 * clickStrength;
        }
    }
	
	private IEnumerator Explosion()
    {	
		int index = 0;
        float timeToNextFrame = 1 / 30.0f;		
		while (isAlive) {
			yield return new WaitForSeconds(timeToNextFrame);
			
			if (index >= explosion.Length) {
				//index = 0;
				isAlive = false;
			}
			else {
				m_sprRender.sprite = explosion[index];
				index++;
			}
		}
		
        yield break;
    }
}
