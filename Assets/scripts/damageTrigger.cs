using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageTrigger : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    float damageAmount = 10;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().setDamage(damageAmount);
        }
    }
}
