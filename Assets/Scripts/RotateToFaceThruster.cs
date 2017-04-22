using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFaceThruster : MonoBehaviour {

    private float lerpSpeed = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float angleRad = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;

        this.transform.rotation = Quaternion.Euler(0, 0, angleDeg + 180);
	}
}
