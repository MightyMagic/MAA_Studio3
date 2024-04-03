using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSound : MonoBehaviour
{

    AudioSource source;


    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (source != null)
        {
            if(!source.isPlaying)
            {
                source.Play();
            }

        }
    }
}
