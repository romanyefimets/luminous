using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAuto : MonoBehaviour
{
    public bool flashLightActivated = false;
    private Color oldColor;

    // Use this for initialization
    [SerializeField]
    GameObject listener;
    void Start()
    {
        oldColor = GetComponent<SpriteRenderer>().color;

        if (flashLightActivated == true)
            GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           if (listener != null)
            listener.GetComponent<triggerListener>().triggerAction();
        }

        if (other.tag == "Light" && flashLightActivated == true)
        {
            Debug.Log("Triggered");
            GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    { 
        if (other.tag == "Light" && flashLightActivated == true)
            GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
    }
}
