using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MENUWorldMovement : MonoBehaviour {

    public float modifier = 0.25f;
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * modifier, transform.position.z);
	}
}
