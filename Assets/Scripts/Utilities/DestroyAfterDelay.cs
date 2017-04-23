using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour {

    public float delay;

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyThisPlease());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator DestroyThisPlease()
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
