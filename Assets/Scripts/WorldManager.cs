using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {

    //[Range(-100, 100)]
    float minTemp = -25;
    float maxTemp = 25;

    public int temperature = 0;
    private float factor = 10f;

    private float goldilocksDist = 20;

    public bool hasEnoughSize = false;
    public bool inGoldilocksZone = false;

    private int pop = 7000000;
    private int popCutoff = 0; //Lose once our population reaches this number.

    int popRandomNum = 0;

    private float lossRate = 0.01f;
    private float dist;

    private int baseLossAmt = 0;
    private int currentLossAmt;

    private Transform sun;

    public Text populationCounterText;
    public Image temperatureImage;

    public GameObject marker;
    public GameObject winGO;

	// Use this for initialization
	void Start () {
        sun = GameObject.FindGameObjectWithTag("Sun").transform;

        currentLossAmt = baseLossAmt;

        StartCoroutine(KillPopulation());

        Debug.Log(goldilocksDist);
        //goldilocksDist;
        Debug.Log(goldilocksDist);

        //Instantiate(marker, sun.position + (-sun.right * goldilocksDist), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {

        UpdateLosses(temperature);
        UpdateTemperature();
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
            currentLossAmt = (int)(Mathf.Abs(temperature) * factor);
            inGoldilocksZone = false;
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

        if(inGoldilocksZone && hasEnoughSize)
        {
            Debug.Log("<color=yellow>Winner!</color>");
            winGO.SetActive(true);

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
            if (!inGoldilocksZone)
            {
                popRandomNum = Random.Range(100, 999);
            }

            populationCounterText.text = pop.ToString("##,#") + "," + popRandomNum;

            yield return new WaitForSeconds(lossRate);

            pop -= currentLossAmt;

            if (pop <= popCutoff)
            {
                pop = popCutoff;
                Debug.Log("<color=red>GAME OVER! ALL CITIZENS ARE DEAD!</color>");
            }
        }
    }
}
