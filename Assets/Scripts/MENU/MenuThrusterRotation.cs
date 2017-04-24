using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuThrusterRotation : MonoBehaviour {

    public float modifier = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(transform.parent.position, transform.forward, Mathf.Sin(Time.timeSinceLevelLoad) * modifier);
	}
}
