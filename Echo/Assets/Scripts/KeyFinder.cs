using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFinder : MonoBehaviour
{
    
    GameObject key;
    bool hasKey = false;

    void Start()
    {
        key = GameObject.FindGameObjectWithTag("key");
    }

    
    void Update()
    {
        if(!hasKey && Vector3.Distance(key.transform.position, transform.position) <= 0.5f) {
            hasKey = true;
            Destroy(key);
        }
    }
}
