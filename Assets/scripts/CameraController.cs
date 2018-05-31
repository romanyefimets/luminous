﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    private bool track;
    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        track = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            exit();
        }
        if (Input.GetButton("PanLeft"))
        {
            gameObject.transform.position += new Vector3(-.05f, 0, 0);
        }
        else if (Input.GetButton("PanRight"))
        {
            gameObject.transform.position += new Vector3(.05f, 0, 0);
        }

        if (Input.GetButtonDown("Track"))
        {
            track = !track;
        }
        
    }

    public void exit()
    {
        Application.Quit();
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (player != null && track)
            transform.position = player.transform.position + offset;
    }
}
