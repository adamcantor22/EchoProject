using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{

    float timer = 90f;
    Text t;

    void Start() {
        t = GetComponent<Text>();
    }

    public void changeTime(float timeChange) {
        timer += timeChange;
    }

    // Update is called once per frame
    void Update()
    {
        changeTime(-Time.deltaTime);

        t.text = "Game Restarting in: " + Mathf.CeilToInt(timer) + "s\n\n(Shoot pings to go faster!)";

        if(timer <= 0f) {
            SceneManager.LoadScene(0);
        }
    }
}
