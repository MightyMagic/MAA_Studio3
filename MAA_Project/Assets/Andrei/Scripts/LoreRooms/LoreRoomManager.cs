using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreRoomManager : MonoBehaviour
{
    [Header("Phrases in sequience")]
    [SerializeField] List<PhrasesToActivate> phrasesToActivate = new List<PhrasesToActivate>();
    private int phrasesIndex = 0;

    [Header("Objects related to the room")]

    [SerializeField] GameObject roomObject;
    [SerializeField] GameObject wallBehindDoor;
    [SerializeField] GameObject InsideRoomTrigger;
    [SerializeField] GameObject OutsideRoomTrigger;
    

    [Header("Monster and player")]

    float monsterSpeed;
    [SerializeField] SimpleMonster monsterScript;
    [SerializeField] Eyes eyeScript;


   


    private void Awake()
    {
        monsterSpeed = monsterScript.moveSpeed;
        roomObject.SetActive(false);

        wallBehindDoor.SetActive(false);

        DisableAllPhrases();
        ActivatePhrasesByIndex(phrasesIndex);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int CurrentPhrasesToOpenRoom()
    {
        if(phrasesIndex < phrasesToActivate.Count)
        {
            return phrasesToActivate[phrasesIndex].phrasesCount;

        }
        else
        {
            return phrasesToActivate[phrasesToActivate.Count - 1].phrasesCount;
        }
    }

    void DisableAllPhrases()
    {
        for(int i = 0; i < phrasesToActivate.Count; i++)
        {
            for(int j = 0; j < phrasesToActivate[i].phrasesObjects.Count; j++)
            {
                phrasesToActivate[i].phrasesObjects[j].SetActive(false);
            }
        }
    }

    public void ActivatePhrasesByIndex(int index)
    {
        if(index < phrasesToActivate.Count)
        {
            for (int j = 0; j < phrasesToActivate[index].phrasesObjects.Count; j++)
            {
                phrasesToActivate[index].phrasesObjects[j].SetActive(true);
            }
        }
    }

    public void OpenLoreRoom()
    {
        roomObject.SetActive(true);
        wallBehindDoor.SetActive(false);

        InsideRoomTrigger.SetActive(true);
        OutsideRoomTrigger.SetActive(false);
    }

    public void CloseLoreRoom()
    {
        wallBehindDoor.SetActive(true);
        eyeScript.enabled = false;
        monsterScript.moveSpeed = 0;
    }

    public void LoreRoomComplete()
    {
        // open the room again, activate the monster
        wallBehindDoor.SetActive(false);
        eyeScript.enabled = true;
        OutsideRoomTrigger.SetActive(true);
    }

    public void DisableLoreRoom()
    {
        monsterScript.moveSpeed = monsterSpeed;

        wallBehindDoor.SetActive(true);
        roomObject.SetActive(false);

        InsideRoomTrigger.SetActive(false);
        OutsideRoomTrigger.SetActive(false);

        phrasesIndex++;
        ActivatePhrasesByIndex(phrasesIndex);
    }

}

[System.Serializable]
public class PhrasesToActivate
{
    public int phrasesCount;
    public List<GameObject> phrasesObjects = new List<GameObject>();
}
