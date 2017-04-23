using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MENUSpawnAsteroids : MonoBehaviour {

    public GameObject[] asteroids;
    public float delay = 0.5f;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnAsteroid", 0, delay);
	}
	
	private void SpawnAsteroid()
    {
        Vector2 pos = new Vector2(0, Random.Range(-2.5f, 2.5f));
        Instantiate(asteroids[Random.Range(0, asteroids.Length)], new Vector2(transform.position.x, transform.position.y) + pos, Quaternion.identity);
    }
}
