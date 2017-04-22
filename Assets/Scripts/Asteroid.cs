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
            CollectAsteroids w = transform.root.GetComponent<CollectAsteroids>();

            //We collided with an asteroid!
            if (w.size >= a.objectSize)
            {
                Debug.Log("Absorbed!");
                //Absorbed Asteroid!
                w.AddMass(a.sizeGranted, col.transform.root.gameObject);
            }
            else
            {
                Debug.Log("We aren't large enough to absorb this!");
                //Lose some mass!
                w.RemoveMass(a.sizeGranted);
            }
        }
    }
}
