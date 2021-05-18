using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressFinder : MonoBehaviour
{

    GameObject key;
    GameObject door;
    GameObject goal;
    bool hasKey = false;

    public GameObject keyText;
    float textDuration = 5f;
    float textTimer = 0f;

    // Initialize the GameObjects used in the scene
    void Start()
    {
        key = GameObject.FindGameObjectWithTag("Key");
        door = GameObject.FindGameObjectWithTag("Door");
        goal = GameObject.FindGameObjectWithTag("Goal");
    }

    // Once per frame, make sure the KeyText is not on the screen too long, then
    // check if the player is close enough to the key, door, or exit
    void Update()
    {
        if(textTimer > 0f) {
            textTimer -= Time.deltaTime;
            if(textTimer <= 0f) {
                keyText.SetActive(false);
            }
        }

        CheckForKey();
        CheckForDoor();
        CheckForGoal();
    }

    // If the player is close enough to the key, set the hasKey boolean to true,
    // play a sound acknowledging that the key was obtained, and destroy the key
    void CheckForKey() {
        if(!hasKey && Vector3.Distance(key.transform.position, transform.position) <= 1.2f) {
            hasKey = true;
            GetComponent<Echolocate>().PlaySound(2);
            Destroy(key);
        }
    }

    // If the player is close enough to the door, check if the player has the key
    // If the key has not been obtained, display the keyText and reset the timer
    // If the key has been obtained, then remove the door so the player can continue
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

    // If the player has the key and is past the door, send the player to the
    // "Win" scene. The key is still checked for to make sure the player cannot
    // win by clipping through the door somehow.
    void CheckForGoal() {
        if(hasKey && Vector3.Distance(goal.transform.position, transform.position) <= 0.8f) {
            SceneManager.LoadScene(1);
        }
    }
}
