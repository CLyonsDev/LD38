using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    float moveForce = 8f;

    public GameObject thrusterPrefab;
    public Transform thrusterSpawnLoc;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            rb.velocity = Vector2.zero;

            //Vector2 dir = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 clickPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 dir = (clickPos - new Vector2(transform.position.x, transform.position.y)).normalized;

            rb.AddForce(-dir * moveForce, ForceMode2D.Impulse);

            GameObject thruster = (GameObject)Instantiate(thrusterPrefab, thrusterSpawnLoc, false);
            thruster.transform.position = thrusterSpawnLoc.position;
        }
	}
}
