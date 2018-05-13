using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFloor : MonoBehaviour{

	// Use this for initialization
	void Start () {
		GetComponent<BoxCollider2D>().enabled = true;
		GetComponent<SpriteRenderer>().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			GetComponent<BoxCollider2D> ().enabled = false;
			Destroy (gameObject);
			GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
