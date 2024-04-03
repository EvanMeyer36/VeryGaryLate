using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOpenDoor : MonoBehaviour
{

    public float rayDistance = 5f; // distance to check for door collider
    public LayerMask playerLayer;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // shoots a ray from camera 

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Door")) // checks if the ray hit a collider tagged as "Door"
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<Door>().ToggleDoor(); // if player object presses "E", the door's "ToggleDoor" function gets called
                }
            }
        }
    }
}
