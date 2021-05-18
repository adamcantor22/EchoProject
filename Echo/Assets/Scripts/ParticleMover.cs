using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMover : MonoBehaviour
{

    Vector3 cameraPos;
    Vector3 direction;
    float speed = 5f;
    bool backwards = false;

    Echolocate echo;

    // Start by getting the Echolocation script
    void Start()
    {
        echo = Camera.main.GetComponent<Echolocate>();
    }

    void Update()
    {
       // Check if the ping is moving backwards and if the camera
       // has moved since the previous frame, if so call GoTowardsCamera
        if(backwards && cameraPos != Camera.main.transform.position) {
            GoTowardsCamera();
        }

        // Move the ping in its current direction
        transform.position += direction * speed * Time.deltaTime;

        // If the ping is moving towards the camera, and is close enough then
        // destroy the ping object and play the "pong" sound that signifies a return
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < 0.5f && backwards) {
            Destroy(gameObject);
            echo.PlaySound(1);
        }
    }

    // Set the cameraPos to the new camera position and set the directon of the
    // ping to face the new camera position
    void GoTowardsCamera() {
        cameraPos = Camera.main.transform.position;
        SetDirection(Vector3.Normalize(cameraPos - transform.position)) ;
    }

    public void SetDirection(Vector3 dir) {
        direction = dir;
    }

    // When the particle starts collision with another object, send the ping
    // back to the camera. Note that since it only has a function if moving
    // fowards the ping can move through walls on the way back to the player.
    void OnCollisionEnter(Collision other) {
        if(!backwards) {
            backwards = true;
            GoTowardsCamera();
        }
    }
}
