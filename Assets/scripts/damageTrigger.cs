using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageTrigger : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    float damageAmount = 10;

    private PlayerController playerController;
	void Start ()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerDamage")
        {
            print(gameObject.name);
            playerController.setDamage(damageAmount);
        }
    }
}
