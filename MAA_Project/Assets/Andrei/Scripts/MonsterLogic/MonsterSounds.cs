using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSounds : MonoBehaviour
{
    [SerializeField] List<AudioClip> monsterSounds = new List<AudioClip>();
    [SerializeField] AudioSource monsterSoundSource;
    [SerializeField] float coolDown;

    float timer = 0f;
    List<AudioClip> soundsBuffer = new List<AudioClip>();
    void Start()
    {
      
    }

    void Update()
    {
        if(timer < coolDown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            PlayRandomSound();
        }
    }

    public void PlayRandomSound()
    {

        if (monsterSounds.Count == 0)
        {
            if(soundsBuffer.Count != 0)
            {
                for (int i = 0; i < soundsBuffer.Count; i++)
                {
                    monsterSounds.Add(soundsBuffer[i]);
                }

                soundsBuffer.Clear();
            }     
        }

        int index = Random.Range(0, monsterSounds.Count);

        if(!monsterSoundSource.isPlaying)
        {
            monsterSoundSource.PlayOneShot(monsterSounds[index]);
            soundsBuffer.Add(monsterSounds[index]);
            monsterSounds.RemoveAt(index);
        }
    }
}
