using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	public DoorScript door;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player")
			door.DoorOpens ();

	}

	void OnTriggerExit2D(Collider2D other){
		//if (other.tag == "Player")
		//	door.DoorCloses();
	}
}
