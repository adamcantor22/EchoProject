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

    // Get the audio source that the sounds will play from, initialize the cooldown
    // timer. Additionally, if the player is in the "win" scene, initialize the
    // associated script.
    void Start() {
        speaker = this.GetComponent<AudioSource>();
        cooldownTimer = cooldown;

        if(winScreenActive) {
            ws = GameObject.Find("KeyText").GetComponent<WinScreen>();
        }
    }

    // Once per frame, decrement the cooldown timer, then check for input
    // from the VR controllers. Additionally, check if the left mouse button
    // has been pressed in case the headset being used does not have controllers
    void Update()
    {
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        CheckForInput();
        if(Input.GetKeyDown(KeyCode.Mouse0))
            Ping();
    }

    // Check each controller in the "controllers" list and see if they are able to
    // provide input. If so, check if the trigger was pressed on that particular device.
    private void CheckForInput()
    {
        foreach (XRController controller in controllers)
        {
            if (controller.enableInputActions)
                CheckForPress(controller.inputDevice);
        }
    }

    // Check if the trigger on the controller that is being checked is considered
    // "pressed", if so call Ping() to send out a ping
    private void CheckForPress(InputDevice device)
    {
        bool triggerValue;
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            Ping();
    }

    void Ping()
    {
        // Check that the ping is not on cooldown
        if (cooldownTimer <= 0f)
        {
            // For each of the trails that will follow the invisible ping,
            // instantiate the trail, set the direction to foward, reset the
            // cooldown timer, and play the "ping" sound
            for (int i = 0; i < trails; i++)
            {
                GameObject instance = (GameObject)Instantiate(trail, transform.position, Quaternion.Euler(0f, 0f, 0f));
                instance.GetComponent<ParticleMover>().SetDirection(transform.forward);
                cooldownTimer = cooldown;
                PlaySound(0);
            }

            // If the player is in the "win" scene, then have the WinScreen script
            // decrement the timer for the "win" scene
            if(winScreenActive && ws != null) {
                ws.changeTime(-5f);
            }
        }
    }

    // Play the sound at the position that was passed for half a second
    public void PlaySound(int sound) {
        speaker.PlayOneShot(clips[sound], 0.5f);
    }
}
