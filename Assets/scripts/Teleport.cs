using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {

    [SerializeField]
    Camera mainCamera;
    private bool canTeleport;
    private Vector3 lastCameraPos;
    public GameObject teleportUI;

    public float startTime;
    float timeDown = 0;

    Rigidbody2D myBody;

    // Use this for initialization
    void Start ()
    {
        startTime = 0;
        myBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Triggered while left mouse button is held down, performs are necessary checks for teleport
        if (Input.GetMouseButton(0))
        {
            // Activate teleport UI if currently deactivated
            if (teleportUI.activeInHierarchy == false)
                teleportUI.SetActive(true);

            // Switch camera control away from following player and set starting mouse position
            if (mainCamera.GetComponent<CameraController>().player != null)
            {
                mainCamera.GetComponent<CameraController>().player = null;
                lastCameraPos = mainCamera.transform.position;
            }

            // Increment time
            timeDown += Time.deltaTime;

            // Raycast between player position and mouse position
            Vector2 difference = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            Vector2 dir = difference.normalized;
            float distance = difference.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance);

            // Get new camera position
            Vector3 newCameraPos = mainCamera.transform.position + (mainCamera.ScreenToWorldPoint(Input.mousePosition) - lastCameraPos) * .1f;

            // Move camera and update lastCameraPos if new camera position is within range
            if ((newCameraPos - transform.position).magnitude < 20)
            {
                mainCamera.transform.position = newCameraPos;
                lastCameraPos = mainCamera.transform.position;
            }

            // Set canTeleport to true if there are no obstructions and 2 seconds of charge has passed
            if (timeDown >= 2 && (hit.collider == null || hit.collider.gameObject.tag != "BlockTeleport"))
            {
                teleportUI.GetComponent<Text>().text = string.Format("Can teleport in: {0:0.00}", Mathf.Clamp(2 - timeDown, 0, 2));
                canTeleport = true;
            }
            // Update teleport indicator UI to display obstruction message if one exists
            else if (hit.collider != null && hit.collider.gameObject.tag == "BlockTeleport")
                teleportUI.GetComponent<Text>().text = "Unable to teleport - obstruction";
            // Update teleport indicator UI to display charge countdown if there are no obstructions
            else
                teleportUI.GetComponent<Text>().text = string.Format("Can teleport in: {0:0.00}", Mathf.Clamp(2 - timeDown, 0, 2));
        }
        // Move camera back towards player
        else if ((mainCamera.transform.position - transform.position).magnitude > 1)
            mainCamera.transform.position += (transform.position - mainCamera.transform.position).normalized;
        // Reset camera control
        else if (mainCamera.GetComponent<CameraController>().player == null)
            mainCamera.GetComponent<CameraController>().player = this.gameObject;

        // Triggered on left mouse button up, teleports player if possible and resets values used for teleport
        if (Input.GetMouseButtonUp(0))
        {
            // If canTeleport is set to true, teleport
            if (canTeleport)
                teleportPlayer();

            // Reset teleport indicator UI, timer, and canTeleport status
            teleportUI.SetActive(false);
            timeDown = 0;
            canTeleport = false;
        }
    }

    public void teleportPlayer()
    {
        Vector2 difference = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        Vector2 dir = difference.normalized;
        float distance = difference.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance);

        if (hit.collider == null || hit.collider.gameObject.tag != "BlockTeleport")
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            myBody.MovePosition(new Vector2(mousePos.x, mousePos.y));
        }
    }
}

