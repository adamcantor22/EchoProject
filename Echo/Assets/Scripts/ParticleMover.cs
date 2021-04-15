using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMover : MonoBehaviour
{

    Vector3 startPos;
    Vector3 direction;
    float speed = 4f;
    bool backwards = false;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, startPos) < 1f && backwards) {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 dir) {
        direction = dir;
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("COLLISION");
        direction *= -1;
        backwards = true;
    }
}
