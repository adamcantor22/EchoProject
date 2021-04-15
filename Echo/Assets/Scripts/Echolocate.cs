using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocate : MonoBehaviour
{

    public GameObject trail;
    public int trails = 6;
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            for (int i = 0; i < trails; i++)
            {
                GameObject instance = (GameObject)Instantiate(trail, transform.position, transform.rotation);
                instance.GetComponent<ParticleMover>().SetDirection(new Vector3(0f, -.1f, 1f));
            }
        }
    }
}
