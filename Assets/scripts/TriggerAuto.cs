using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAuto : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    GameObject listener;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           listener.GetComponent<triggerListener>().triggerAction();
        }
    }
}
