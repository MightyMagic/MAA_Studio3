using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSounds : MonoBehaviour
{
    [SerializeField] List<AudioClip> monsterSounds = new List<AudioClip>();
    public AudioSource monsterSoundSource;
    [SerializeField] float coolDownBottom;
    [SerializeField] float coolDownTop;


    float coolDown;
    float timer = 0f;
    List<AudioClip> soundsBuffer = new List<AudioClip>();

    void Start()
    {
        coolDown = Random.Range(coolDownBottom, coolDownTop);
      
    }

    void Update()
    {
        if(timer < coolDown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (!monsterSoundSource.isPlaying)
            {
                timer = 0f;
                coolDown = Random.Range(coolDownBottom, coolDownTop);
                PlayRandomSound();
            }
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
