using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectAsteroids : MonoBehaviour {

    public int size = 0;

    public int winSize = 1;
    public int loseSize = -1;

    private float collectDelay = 0.075f;

    private Camera cam;
    public Text sizeText;

    private float targetCamZoomSize;
    private float zoomLerpSpeed = 2.5f;

    void Start()
    {
        cam = Camera.main;
        targetCamZoomSize = cam.orthographicSize;
    }

    void Update()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetCamZoomSize, zoomLerpSpeed * Time.deltaTime);
        sizeText.text = size.ToString() + " / " + winSize;
    }

    public void AddMass(int amount, GameObject asteroid)
    {
        size += amount;

        //GetComponent<Rigidbody2D>().mass += asteroid.GetComponent<Asteroid>().massIncrease;
        asteroid.layer = LayerMask.NameToLayer("PlayerPlanet");

        targetCamZoomSize += ((float)amount / 24);

        StartCoroutine(CollectObject(asteroid, collectDelay));
    }

    public void RemoveMass(int amount)
    {
        size -= amount;
        if(size <= loseSize)
        {
            //We lost! Humanity is doomed! :(
            Debug.Log("<color=red>GAME OVER!</color>");
        }
    }

    private IEnumerator CollectObject(GameObject o, float delay)
    {
        o.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        yield return new WaitForSeconds(delay);

        o.GetComponent<Asteroid>().collected = true;
        o.GetComponent<Rigidbody2D>().isKinematic = true;
        o.transform.SetParent(this.transform, true);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Asteroid")
        {
            Asteroid a = col.transform.GetComponent<Asteroid>();

            //We collided with an asteroid!
            if (size >= a.objectSize)
            {
                Debug.Log("Absorbed!");
                //Absorbed Asteroid!
                AddMass(a.sizeGranted, col.transform.root.gameObject);
            }
            else
            {
                Debug.Log("We aren't large enough to absorb this!");
                //Lose some mass!
                //RemoveMass(a.sizeGranted);
            }
        }
    }
}
