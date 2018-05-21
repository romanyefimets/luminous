using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform target;
    public float velocity = 10;

    private Rigidbody2D body;
    private int direction;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        direction = -1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 difference = transform.position - target.position;

        if (difference.magnitude <= 20)
            transform.position += direction * difference.normalized * Time.deltaTime * velocity;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Light")
            direction = 1;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Light")
            direction = -1;
    }
}
