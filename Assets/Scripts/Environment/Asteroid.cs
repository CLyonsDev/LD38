using UnityEngine;

public class Asteroid : MonoBehaviour {

    public int objectSize;
    public int sizeGranted;
    public float massIncrease;
    public bool collected = false;

    private Rigidbody2D rb;
    private SpriteRenderer rend;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if(!rend.isVisible && rb.simulated)
        {
            rb.simulated = false;
        }else if(rend.isVisible && !rb.simulated)
        {
            rb.simulated = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.transform.tag == "Asteroid" && collected) || (col.transform.tag == "Sun" && collected))
        {
            Asteroid a = col.transform.GetComponent<Asteroid>();

            if (a.collected)
                return;

            CollectAsteroids w = transform.root.GetComponent<CollectAsteroids>();
            w.cam.transform.GetComponent<CameraShake>().StartShake(0, 0.075f, 0.3f, 0.035f);

            SoundManager._Instance.PlaySound(w.sounds[Random.Range(0, w.sounds.Length)], col.contacts[0].point, 0.05f, false);

            w.SpawnCollisionParticles(false, Vector3.zero);

            //We collided with an asteroid!
            if (w.size >= a.objectSize)
            {
                Debug.Log("Absorbed!");
                //Absorbed Asteroid!
                w.AddMass(a.sizeGranted, col.transform.gameObject, false);
            }
            else
            {
                Debug.Log("We aren't large enough to absorb this!");
                //Lose some mass!
                //w.RemoveMass(a.sizeGranted);
            }
        }
    }
}
