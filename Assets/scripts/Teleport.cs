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
    float coolDown = 0;

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
        // If coolDown is greater than 0, decrement time
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            teleportUI.GetComponent<Text>().text = string.Format("Can teleport in: {0:0.00}", Mathf.Clamp(coolDown, 0, Mathf.Infinity));
        }
        else if (teleportUI.activeSelf)
            teleportUI.SetActive(false);

        // Triggered while left mouse button is held down, performs are necessary checks for teleport
        if (Input.GetMouseButton(0) && coolDown <= 0)
        {
            // Activate teleport UI if currently deactivated
            if (teleportUI.activeInHierarchy == false)
                teleportUI.SetActive(true);

            //// Switch camera control away from following player and set starting mouse position
            //if (mainCamera.GetComponent<CameraController>().player != null)
            //{
            //    mainCamera.GetComponent<CameraController>().player = null;
            //    lastCameraPos = mainCamera.transform.position;
            //}

            // Increment time
            // timeDown += Time.deltaTime;

            // Raycast between player position and mouse position
            Vector2 difference = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            Vector2 dir = difference.normalized;
            float distance = difference.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance);
            Debug.DrawLine(transform.position, dir * distance, new Color(0, 0, 0));

            // Get new camera position
            //Vector3 newCameraPos = mainCamera.transform.position + (mainCamera.ScreenToWorldPoint(Input.mousePosition) - lastCameraPos) * .1f;
            float newCameraSize = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - lastCameraPos).magnitude * .02f;

            // Move camera and update lastCameraPos if new camera position is within range
            //if ((newCameraPos - transform.position).magnitude < 20)
            //{
            //    mainCamera.transform.position = newCameraPos;
            //    lastCameraPos = mainCamera.transform.position;
            //}
            if (mainCamera.orthographicSize < 20)
            {
                mainCamera.orthographicSize += newCameraSize;
                lastCameraPos = mainCamera.transform.position;
            }

            // If teleportUI is currently inactive, activate it
            if (teleportUI.activeSelf == false)
                teleportUI.SetActive(true);

            // Set canTeleport to true if there are no obstructions
            if (hit.collider == null || hit.collider.gameObject.tag != "BlockTeleport")
            {
                teleportUI.GetComponent<Text>().text = "Can teleport";
                canTeleport = true;
            }
            // Update teleport indicator UI to display obstruction message if one exists
            else if (hit.collider != null && hit.collider.gameObject.tag == "BlockTeleport")
            {
                teleportUI.GetComponent<Text>().text = "Unable to teleport - obstruction";
                canTeleport = false;
            }
        }
        // Move camera back towards player
        //else if ((mainCamera.transform.position - transform.position).magnitude > 1)
        //    mainCamera.transform.position += (transform.position - mainCamera.transform.position).normalized;
        else if (mainCamera.orthographicSize > 5)
            mainCamera.orthographicSize -= .5f;
        // Reset camera control
        //else if (mainCamera.GetComponent<CameraController>().player == null)
        //    mainCamera.GetComponent<CameraController>().player = this.gameObject;

        // Triggered on left mouse button up, teleports player if possible and resets values used for teleport
        if (Input.GetMouseButtonUp(0))
        {
            // If canTeleport is set to true, teleport
            if (canTeleport)
            {
                teleportPlayer();
                coolDown = 10;
                teleportUI.SetActive(true);
            }

            // Reset teleport indicator UI and canTeleport status
            //teleportUI.SetActive(false);
            //timeDown = 0;
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

