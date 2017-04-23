using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    public float scrollModifier = 5f;

    public Vector2 dir = Vector2.zero;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Renderer>().material.mainTextureOffset += (dir / scrollModifier) * Time.deltaTime;
	}
}
