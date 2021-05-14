using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Echolocate : MonoBehaviour
{
    public List<XRController> controllers = null;
    public GameObject trail;
    public int trails = 6;
    public AudioClip[] clips;
    public bool winScreenActive;

    AudioSource speaker;
    public float cooldown;
    float cooldownTimer;
    WinScreen ws;

    void Start() {
        speaker = this.GetComponent<AudioSource>();
        cooldownTimer = cooldown;

        if(winScreenActive) {
            ws = GameObject.Find("KeyText").GetComponent<WinScreen>();
        }
    }

    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        CheckForInput();
        if(Input.GetKeyDown(KeyCode.Mouse0))
            Ping();
    }

    private void CheckForInput()
    {
        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions)
                CheckForPress(controller.inputDevice);
        }
    }

    private void CheckForPress(InputDevice device)
    {
        bool triggerValue;
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            Ping();
    }

    void Ping()
    {

        if (cooldownTimer <= 0f)
        {
            for (int i = 0; i < trails; i++)
            {
                GameObject instance = (GameObject)Instantiate(trail, transform.position, Quaternion.Euler(0f, 0f, 0f));
                instance.GetComponent<ParticleMover>().SetDirection(transform.forward);
                cooldownTimer = cooldown;
                PlaySound(0);
            }

            if(winScreenActive && ws != null) {
                ws.changeTime(-5f);
                Debug.Log("minus 5?");
            }
        }
    }

    public void PlaySound(int sound) {
        speaker.PlayOneShot(clips[sound], 0.5f);
    }
}
