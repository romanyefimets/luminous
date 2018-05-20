using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour {

	private Rigidbody2D rb2d;
	public float fallDelay;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.collider.CompareTag("Player")){
			StartCoroutine (fall ());
		}
	}

	IEnumerator fall(){
		yield return new WaitForSeconds(fallDelay);
		rb2d.isKinematic = false;
		rb2d.gravityScale = 3;
		GetComponent<Collider2D> ().isTrigger = true;
		yield return 0;
	}

}
