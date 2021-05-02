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

    void Start()
    {
        echo = Camera.main.GetComponent<Echolocate>();
    }

    // Update is called once per frame
    void Update()
    {
        if(backwards && cameraPos != Camera.main.transform.position) {
            GoTowardsCamera();
        }
            
        transform.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < 0.5f && backwards) {
            Destroy(gameObject);
            echo.PlaySound(1);
        }
    }

    void GoTowardsCamera() {
        cameraPos = Camera.main.transform.position;
        SetDirection(Vector3.Normalize(cameraPos - transform.position)) ;
    }

    public void SetDirection(Vector3 dir) {
        direction = dir;
    }

    void OnCollisionEnter(Collision other) {
        if(!backwards) {
            backwards = true;
            GoTowardsCamera();
        }
    }
}
