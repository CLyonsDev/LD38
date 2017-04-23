using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceOnSpawn : MonoBehaviour {

    public float force = .75f;

    private Rigidbody2D rb;
    public GameObject debrisGO;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.AddForce(-Vector2.right * force, ForceMode2D.Impulse);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.root.tag == "Player")
        {
            Camera.main.GetComponent<CameraShake>().StartShake(0, 0.075f, 0.3f, 0.075f);
            SpawnDebris(col.contacts[0].point);
        }
    }

    private void SpawnDebris(Vector3 pos)
    {
        pos += new Vector3(0, 0, -1);
        Instantiate(debrisGO, pos, Quaternion.identity);
    }
}
