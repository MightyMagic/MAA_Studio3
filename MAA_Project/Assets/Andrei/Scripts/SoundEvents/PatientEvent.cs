using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientEvent : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips;
    [SerializeField] float coolDown;

    bool isPlaying = false;
    float timer = 0f;
    AudioSource source;
    int clipIndex = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlaying & timer < coolDown)
        {
            timer += Time.deltaTime;
            print("Cooldown timer: " + timer);
        }

        if(timer > coolDown)
        {
            isPlaying = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!isPlaying)
            {
                source.clip = clips[clipIndex];
                timer = 0f;
                isPlaying = true;

                source.Play();

                clipIndex = (clipIndex + 1) % clips.Count;
            }
        }
    }
}
