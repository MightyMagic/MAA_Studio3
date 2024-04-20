using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroup : MonoBehaviour
{
    [SerializeField] private List<Material> materials = new List<Material>();

    public List<SlowlyChasingMonster> chasingMonsters;

    [SerializeField] AudioSource monsterSource;

    void Start()
    {
        foreach (Transform t in transform)
        {
            if (t.GetComponent<SlowlyChasingMonster>())
            {
                t.GetComponent<SlowlyChasingMonster>().AssignMaterial(materials[Random.Range(0, materials.Count)]);
            }
        }
    }

  
    void Update()
    {
        
    }

    public void PlaySoundOfTheAttackingMonster(Transform t)
    {
        monsterSource.transform.position = t.position;
        monsterSource.Play();
    }

    public void StopMonsterSound()
    {
        if(monsterSource.isPlaying)
        {
            monsterSource.Stop();
        }
    }
}
