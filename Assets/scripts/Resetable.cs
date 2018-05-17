using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetable : MonoBehaviour {

    private Vector3 location;
	void Start () {
        location = gameObject.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset()
    {
        gameObject.SetActive(true);
        gameObject.transform.localPosition = location;
    }
}
