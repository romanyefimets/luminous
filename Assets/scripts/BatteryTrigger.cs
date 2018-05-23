using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryTrigger : MonoBehaviour {

    public float increaseAmount = 30;

	// Use this for initialization
	void Start () {
		GetComponent<BoxCollider2D> ().enabled = true;
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // On collision with player, increase player's batterylife by given amount and destroy this object
        if (other.tag == "Player")
        {
            other.transform.Find("armPivot").Find("arm").GetComponent<FlashLightControl>().SetBattery(increaseAmount);
			GetComponent<BoxCollider2D> ().enabled = false;
			GetComponent<SpriteRenderer> ().enabled = false;
        }
    }
}
