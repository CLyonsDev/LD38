using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavyBackground : MonoBehaviour {

    BackgroundScroll bg;
    public float speedModifier = 1;

	// Use this for initialization
	void Start () {
        bg = GetComponent<BackgroundScroll>();
	}
	
	// Update is called once per frame
	void Update () {
        bg.dir.y = (Mathf.Sin(Time.time) * speedModifier);
	}
}
