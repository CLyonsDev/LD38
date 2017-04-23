using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    float moveForce = 8f;
    float power = 0;
    float minPower = 0;
    float maxPower = 2;

    int popLostOnLaunch = 25000;

    public GameObject thrusterPrefab;
    public Transform thrusterSpawnLoc;

    public GameObject powerContainer;
    public Image powerBar;

    private Rigidbody2D rb;
    public BackgroundScroll scroll;
    private WorldManager world;

    public GameObject buildingsParticleSystemGO;

    private bool charging = false;
    private bool up = true;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        world = GetComponent<WorldManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (.paused)
        //    return;
        if (TimeManager._timeManager.paused)
            return;

		if(Input.GetMouseButtonDown(0))
        {
            charging = true;
            powerContainer.SetActive(true);
        }else if(Input.GetMouseButtonUp(0))
        {
            charging = false;
            powerContainer.SetActive(false);

            Launch(power * moveForce);
        }

        if(charging)
        {

            if(up)
            {
                power += Time.deltaTime;
                if (power >= maxPower)
                    up = false;
            }
            else
            {
                power -= Time.deltaTime;
                if (power <= minPower)
                    up = true;
            }

            powerBar.fillAmount = (power / maxPower);

            //Debug.Log(power * moveForce);
        }

        scroll.dir = rb.velocity;
	}

    private void Launch(float p)
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        rb.velocity = Vector2.zero;

        //Vector2 dir = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 clickPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 dir = (clickPos - new Vector2(transform.position.x, transform.position.y)).normalized;

        rb.AddForce(-dir * p, ForceMode2D.Impulse);

        GameObject thruster = (GameObject)Instantiate(thrusterPrefab, thrusterSpawnLoc, false);
        thruster.transform.position = thrusterSpawnLoc.position;

        world.pop -= (popLostOnLaunch * (int)power);
        power = 0;

        foreach (ParticleSystem ps in buildingsParticleSystemGO.transform.GetChild(0).GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }
    }
}
