using UnityEngine;

public class Asteroid : MonoBehaviour {

    public int objectSize;
    public int sizeGranted;
    public float massIncrease;
    public bool collected = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Asteroid" && collected)
        {
            Asteroid a = col.transform.GetComponent<Asteroid>();

            if (a.collected)
                return;

            CollectAsteroids w = transform.root.GetComponent<CollectAsteroids>();
            w.cam.transform.GetComponent<CameraShake>().StartShake(0, 0.075f, 0.3f, 0.035f);

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
