using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static TimeManager _timeManager = null;
    public bool paused = false;
    
    void Awake()
    {
        if (_timeManager == null)
            _timeManager = this;
        else
            Destroy(this);
    }

	// Use this for initialization
	void Start () {
        Pause();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if(paused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
	}

    public void Pause()
    {
        paused = true;
    }

    public void Unpause()
    {
        paused = false;
    }
}
