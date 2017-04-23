using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private Camera cam;

    public bool shaking = false;
    private float duration = 0.3f;
    private float frequency = 0.075f;
    private float amplitude = 0.05f;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        //StartShake();
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartShake(0, frequency, duration, amplitude);
        }
    }

    public void StartShake(float delay, float freq, float dur, float amp)
    {
        amplitude = amp;
        InvokeRepeating("Shake", 0, freq);
        Invoke("StopShake", dur);
    }

    private void StopShake()
    {
        CancelInvoke("Shake");
    }

    private void Shake()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * amplitude;
        Vector3 pos3 = new Vector3(pos.x, pos.y, cam.transform.position.z);

        cam.transform.position = pos3;
    }
}
