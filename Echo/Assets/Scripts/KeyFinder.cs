using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFinder : MonoBehaviour
{
    
    GameObject key;
    GameObject door;
    bool hasKey = false;

    public GameObject keyText;
    float textDuration = 5f;
    float textTimer = 0f;
    float maxDistance = 2f;

    void Start()
    {
        key = GameObject.FindGameObjectWithTag("Key");
        door = GameObject.FindGameObjectWithTag("Door");
    }

    
    void Update()
    {
        if(textTimer > 0f) {
            textTimer -= Time.deltaTime;
            if(textTimer <= 0f) {
                keyText.SetActive(false);
            }
        }

        if(!hasKey && Vector3.Distance(key.transform.position, transform.position) <= maxDistance) {
            hasKey = true;
            GetComponent<Echolocate>().PlaySound(2);
            Destroy(key);
        }

        if(Vector3.Distance(door.transform.position, transform.position) <= maxDistance) {
            if(!hasKey && !keyText.activeInHierarchy) {
                keyText.SetActive(true);
                textTimer = textDuration;
            }

            else if(hasKey) {
                //go to end screen
            }
        }
    }
}
