using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    float timer = 90f;
    Text t;

    // Initialize the text object
    void Start() {
        t = GetComponent<Text>();
    }

    public void changeTime(float timeChange) {
        timer += timeChange;
    }

    // Once per frame, decrement the timer if necessary and make sure the countdown
    // text is still being displayed. Once the timer is at or below zero, send
    // the player back to the main scene.
    void Update()
    {
        changeTime(-Time.deltaTime);

        t.text = "Game Restarting in: " + Mathf.CeilToInt(timer) + "s\n\n(Shoot pings to go faster!)";

        if(timer <= 0f) {
            SceneManager.LoadScene(0);
        }
    }
}
