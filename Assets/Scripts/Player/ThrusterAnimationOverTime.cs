using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAnimationOverTime : MonoBehaviour {

    public GameObject thrusterGO;
    public Transform thrusterOffsetT;
    public float delay = 0.75f;

	void Start () {
        StartCoroutine(FireThruster());
	}

    private IEnumerator FireThruster()
    {
        while(true)
        {
            GameObject effect = (GameObject)Instantiate(thrusterGO, thrusterOffsetT);

            effect.transform.position = thrusterOffsetT.position;
            effect.transform.rotation = thrusterOffsetT.rotation;
            effect.transform.localScale = Vector3.one;

            yield return new WaitForSeconds(delay);
        }
    }
}
