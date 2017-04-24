using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectAsteroids : MonoBehaviour {

    public int size = 0;

    public int winSize = 1;
    public int secretWinSize = 1;
    public int loseSize = -1;

    private float collectDelay = 0.075f;

    private int popLostOnCollect = 500;

    [HideInInspector]
    public Camera cam;
    public Text sizeText;

    private float targetCamZoomSize;
    private float zoomLerpSpeed = 2.5f;

    public GameObject collisionParticleSystemGO;

    public AudioClip[] sounds;

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

    public void AddMass(int amount, GameObject asteroid, bool worldCollided)
    {
        size += amount;

        //GetComponent<Rigidbody2D>().mass += asteroid.GetComponent<Asteroid>().massIncrease;
        asteroid.layer = LayerMask.NameToLayer("PlayerPlanet");

        //targetCamZoomSize += ((float)amount / 55f);
        targetCamZoomSize += ((float)asteroid.GetComponent<Asteroid>().objectSize / 300);

        if(asteroid.transform.tag == "Sun" && size >= secretWinSize)
        {
            //Hoooo boy you've really done it now.
            GetComponent<WorldManager>().SECRETWIN = true;
            Debug.Log("<color=orange>SECRET WIN ACHIEVED</color>");
        }

        if (worldCollided)
            StartCoroutine(CollectObject(asteroid, collectDelay));
        else
            StartCoroutine(CollectObject(asteroid, 0f));
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
        GetComponent<WorldManager>().pop -= popLostOnCollect;

        Rigidbody2D r = o.GetComponent<Rigidbody2D>();

        r.velocity = Vector2.zero;

        yield return new WaitForSeconds(delay);
        
        r.GetComponent<Asteroid>().collected = true;
        r.GetComponent<Rigidbody2D>().isKinematic = true;
        r.transform.SetParent(this.transform, true);
    }

    public void SpawnCollisionParticles(bool worldCollide, Vector3 pos)
    {
        GameObject ps;

        if(worldCollide)
        {
            ps = (GameObject)Instantiate(collisionParticleSystemGO, pos, Quaternion.identity);
        }
        else
        {
            ps = (GameObject)Instantiate(collisionParticleSystemGO, transform.position, Quaternion.identity);
        }

        ps.transform.position += new Vector3(0, 0, -1);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager._Instance.PlaySound(sounds[Random.Range(0, sounds.Length)], col.contacts[0].point, 0.05f, false);

        cam.transform.GetComponent<CameraShake>().StartShake(0, 0.075f, 0.3f, 0.075f);
        SpawnCollisionParticles(true, col.contacts[0].point);

        if(col.transform.tag == "Asteroid")
        {
            Asteroid a = col.transform.GetComponent<Asteroid>();

            //We collided with an asteroid!
            if (size >= a.objectSize)
            {
                //Debug.Log("Absorbed!");
                //Absorbed Asteroid!
                AddMass(a.sizeGranted, col.transform.gameObject, true);
            }
            else
            {
                //Debug.Log("We aren't large enough to absorb this!");
                //Lose some mass!
                //RemoveMass(a.sizeGranted);
            }
        }else if(col.transform.tag == "Sun")
        {
            //YOU FLEW INTO THE SUN! WHAT HAPPENS NEXT???
            Asteroid a = col.transform.GetComponent<Asteroid>();

            if(size >= a.objectSize)
            {
                //Secret ending.
            }
            else
            {
                GetComponent<WorldManager>().lossRate = 50000;
            }
        }
    }
}
