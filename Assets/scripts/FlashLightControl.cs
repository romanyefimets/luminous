using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControl : MonoBehaviour {

    public Camera mainCamera;
	
	// Update is called once per frame
	void Update () {
        // Used https://youtu.be/m-J5sCRipA0?t=23m27s for this code.
        Vector3 lookPosition = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position).normalized;

        float rotZ = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }
}
