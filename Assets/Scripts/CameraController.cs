using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private bool isCameraLocked = true;
    public float panSpeed = 1.25f;
    private float maxDistanceFromPlayer = 15f;

    // Update is called once per frame
    void Update()
    {
        UnlockCamera();
        if (isCameraLocked)
            transform.position = player.transform.position + new Vector3(3, 3, -5);
        else
            MoveCamera();    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void UnlockCamera(){
        isCameraLocked = !Input.GetKey(KeyCode.LeftShift);
    }

    private void MoveCamera(){
        Vector3 moveDirection = Vector3.zero;

        // Handle movement input and assign the move direction
        if (Input.GetKey(KeyCode.D)) moveDirection.x = panSpeed;
        if (Input.GetKey(KeyCode.A)) moveDirection.x = -panSpeed;
        if (Input.GetKey(KeyCode.W)) moveDirection.y = panSpeed;
        if (Input.GetKey(KeyCode.S)) moveDirection.y = -panSpeed;

        // Check if within max distance from the player before moving
        if (moveDirection != Vector3.zero)
        {
            bool withinMaxDistance = (Mathf.Abs(transform.position.x + moveDirection.x - player.position.x) < maxDistanceFromPlayer &&
                                      Mathf.Abs(transform.position.y + moveDirection.y - player.position.y) < maxDistanceFromPlayer);

            if (withinMaxDistance)
                transform.position += moveDirection * Time.deltaTime;
        }
    }
}
