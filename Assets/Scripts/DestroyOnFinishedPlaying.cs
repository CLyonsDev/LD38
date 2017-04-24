using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFinishedPlaying : MonoBehaviour {

    AudioSource a;
    public bool started = false;

	// Use this for initialization
	void Start () {
        a = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Started");
        if (!a.isPlaying)
        {
            Destroy(this.gameObject);
        }
	}
}
