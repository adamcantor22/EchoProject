using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocate : MonoBehaviour
{

    public GameObject trail;
    public int trails = 6;
    public AudioClip[] clips;

    AudioSource speaker;
    public float cooldown;
    float cooldownTimer;

    void Start() {
        speaker = this.GetComponent<AudioSource>();
        cooldownTimer = cooldown;
    }
  
    void Update()
    {
        
        if(cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;


        if (cooldownTimer <= 0f && Input.GetKeyDown(KeyCode.Mouse0)) {
            for (int i = 0; i < trails; i++)
            {
                GameObject instance = (GameObject)Instantiate(trail, transform.position, Quaternion.Euler(0f,0f,0f));
                instance.GetComponent<ParticleMover>().SetDirection(transform.forward);
                Debug.Log(transform.forward);
                cooldownTimer = cooldown;
                PlaySound(0);
            }
        }
    }

    public void PlaySound(int sound) {
        speaker.PlayOneShot(clips[sound], 0.5f);
    }
}