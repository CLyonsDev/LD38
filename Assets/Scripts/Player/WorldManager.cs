using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

    //[Range(-100, 100)]
    float minTemp = -30;
    float maxTemp = 30;

    public int temperature = 0;
    private float factor = 10f;

    //private float goldilocksDist = 40;

    public bool hasEnoughSize = false;
    public bool inGoldilocksZone = false;

    [HideInInspector]
    public bool win = false;
    [HideInInspector]
    public bool lose = false;
    [HideInInspector]
    public bool SECRETWIN = false;

    [HideInInspector]
    public int pop = 7000000;
    [HideInInspector]
    public int popCutoff = 0; //Lose once our population reaches this number.

    string popRandomNum = string.Empty;

    public float lossRate = 0.01f;
    private float dist;

    private int baseLossAmt = 31;
    private int currentLossAmt;

    private Transform sun;
    private Rigidbody2D rb;

    public Text populationCounterText;
    public Image temperatureImage;

    public GameObject marker;
    public GameObject winGO;
    public GameObject loseGO;

	// Use this for initialization
	void Start () {
        sun = GameObject.FindGameObjectWithTag("Sun").transform;

        currentLossAmt = baseLossAmt;

        StartCoroutine(KillPopulation());

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateTemperature();

        if (TimeManager._timeManager.paused)
            return;

        UpdateLosses(temperature);
        CheckIfWin();
    }

    private void UpdateLosses(float temp)
    {
        if(temperature == 0)
        {
            currentLossAmt = 0;
            inGoldilocksZone = true;
        }
        else
        {
            currentLossAmt = baseLossAmt + (int)(Mathf.Abs(temperature) * factor);
            inGoldilocksZone = false;
        }

        if(pop <= popCutoff)
        {
            pop = popCutoff;
        }
    }

    private void CheckIfWin()
    {
        //Debug.Log("checkifwin");
        CollectAsteroids a = GetComponent<CollectAsteroids>();
        if(a.size >= a.winSize)
        {
            hasEnoughSize = true;
        }

        if(inGoldilocksZone && hasEnoughSize && rb.velocity.magnitude <= 1f)
        {
            Debug.Log("<color=yellow>Winner!</color>");
            winGO.SetActive(true);
            win = true;

            GetComponent<Rigidbody2D>().drag = 25;
            GetComponent<Rigidbody2D>().angularDrag = 25;
        }
    }

    private void UpdateTemperature()
    {
        //temperatureImage.fillAmount = (Mathf.Abs((float)temperature / 100));
        dist = (transform.position - sun.position).magnitude - (sun.GetComponent<CircleCollider2D>().radius * sun.localScale.x) ;


        temperature = (int)Mathf.Clamp(dist - maxTemp, minTemp, maxTemp);
        //Debug.Log(temperature);


        temperatureImage.fillAmount = Mathf.InverseLerp(minTemp, maxTemp, -(float)temperature);
    }

    private IEnumerator KillPopulation()
    {
        while(true)
        {
            if (!TimeManager._timeManager.paused)
            {
                if (pop <= popCutoff)
                {
                    //popRandomNum = "000";
                    populationCounterText.text = "0";
                }
                else if (!inGoldilocksZone)
                {
                    popRandomNum = Random.Range(100, 999).ToString();
                    populationCounterText.text = pop.ToString("##,#") + "," + popRandomNum;
                }

                pop -= currentLossAmt;

                if (pop <= popCutoff)
                {
                    pop = popCutoff;
                    populationCounterText.text = "0";
                    lose = true;
                    Debug.Log("<color=red>GAME OVER! ALL CITIZENS ARE DEAD!</color>");
                    loseGO.SetActive(true);
                }
            }
            yield return new WaitForSeconds(lossRate);
        }
    }
}
