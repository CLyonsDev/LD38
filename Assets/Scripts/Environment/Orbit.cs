using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public float speed = 5f;
    public Transform p;

    Asteroid a;

	// Use this for initialization
	void Start () {
        a = GetComponent<Asteroid>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(!a.collected)
            transform.RotateAround(p.position, Vector3.forward, speed * Time.deltaTime);
	}
}
