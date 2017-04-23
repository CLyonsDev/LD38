using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    Transform player;
    public float lerpSpeed = 3f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y, transform.position.z), lerpSpeed * Time.fixedDeltaTime);
	}
}
