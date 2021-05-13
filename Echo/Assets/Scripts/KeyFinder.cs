using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyFinder : MonoBehaviour
{
    
    GameObject key;
    GameObject door;
    GameObject goal;
    bool hasKey = false;

    public GameObject keyText;
    float textDuration = 5f;
    float textTimer = 0f;

    void Start()
    {
        key = GameObject.FindGameObjectWithTag("Key");
        door = GameObject.FindGameObjectWithTag("Door");
        goal = GameObject.FindGameObjectWithTag("Goal");
    }

    
    void Update()
    {
        if(textTimer > 0f) {
            textTimer -= Time.deltaTime;
            if(textTimer <= 0f) {
                keyText.SetActive(false);
            }
        }
    }

    void CheckForKey() {
        if(!hasKey && Vector3.Distance(key.transform.position, transform.position) <= 1.2f) {
            hasKey = true;
            GetComponent<Echolocate>().PlaySound(2);
            Destroy(key);
        }
    }

    void CheckForDoor() {
        if(Vector3.Distance(door.transform.position, transform.position) <= 2f) {
            if(!hasKey && !keyText.activeInHierarchy) {
                keyText.SetActive(true);
                textTimer = textDuration;
            }

            else if(hasKey) {
                door.SetActive(false);
            }
        }
    }

    void CheckForGoal() {
        if(hasKey && Vector3.Distance(goal.transform.position, transform.position) <= 0.8f) {
            SceneManager.LoadScene(1);
        }
    }
}
