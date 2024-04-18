using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMonologue : MonoBehaviour
{
    int counter = 0;
    [SerializeField] AudioSource source;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !source.isPlaying && counter == 0)
        {
            counter++;
            source.Play();
        }
    }
}
