using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class CursorManager : MonoBehaviour {

    public Texture2D cursor;


	// Use this for initialization
	void Start () {
        SetCursor();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetCursor()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
