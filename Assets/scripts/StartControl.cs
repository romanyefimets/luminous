using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour {

    public void OnClick()
    {
        SceneManager.LoadScene("main");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            SceneManager.LoadScene("main");
    }
}
