using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour {

    public int amount = 10;
    public float radius = 2f;

    public Vector3 scale = Vector3.one;
    public int objSize = 1;
    public int sizeGranted = 1;

    public GameObject[] asteroidPrefabs;

	void Start () {
        for (int i = 0; i < amount; i++)
        {
            GameObject asteroid = (GameObject)Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)]);

            asteroid.transform.localScale = scale;
            asteroid.GetComponent<Asteroid>().objectSize = this.objSize;
            asteroid.GetComponent<Asteroid>().sizeGranted = this.sizeGranted;

            asteroid.transform.SetParent(this.transform);
            asteroid.transform.localPosition = Vector2.zero;

            Vector2 pos = Random.insideUnitCircle * radius;
            asteroid.transform.localPosition = pos;
            //Debug.Log("Spawning asteroid at (" + pos.x + "," + pos.y + ").");
        }
	}
}
