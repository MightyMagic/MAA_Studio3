using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreRoomManager : MonoBehaviour
{
    [Header("Phrases in sequence")]
    [SerializeField] List<PhrasesToActivate> phrasesToActivate = new List<PhrasesToActivate>();
    [SerializeField] PuzzleCatcher puzzleCatcher;
    private int phrasesIndex = 0;

    [Header("Objects related to the room")]

    [SerializeField] GameObject roomObject;
    [SerializeField] GameObject wallBehindDoor;
    [SerializeField] GameObject InsideRoomTrigger;
    [SerializeField] GameObject OutsideRoomTrigger;

    public int phraseIndex;
    

    [Header("Monster and player")]

    float monsterSpeed;
    [SerializeField] SimpleMonster monsterScript;
    [SerializeField] Eyes eyeScript;


   


    private void Awake()
    {
        PlayerPrefs.SetInt("PhraseIndex", phraseIndex);

        monsterSpeed = monsterScript.moveSpeed;
        roomObject.SetActive(false);

        wallBehindDoor.SetActive(false);

        //DisableAllPhrases();
        //ActivatePhrasesByIndex(phrasesIndex);
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

    public int LatestPhraseIndex()
    {
        //PlayerPrefs.SetInt("PhraseIndex", phraseIndex);

        if (!PlayerPrefs.HasKey("PhraseIndex"))
        {
            PlayerPrefs.SetInt("PhraseIndex", 0);
        }
          return PlayerPrefs.GetInt("PhraseIndex");    
    }

    public void FecthPreviousPhrases(int phrasesIndex)
    {
        Debug.LogError("Entering fetching phase here!");
        //int phraseCount = CurrentPhrasesToOpenRoom();
        //phrasesIndex--;

        if(phrasesIndex > -1)
        {
            puzzleCatcher.CapturedWords.Clear();

            for (int i = 0; i < phrasesIndex; i++)
            {
                for (int j = 0; j < phrasesToActivate[i].phraseObjects.Count; j++)
                {
                    puzzleCatcher.CapturedWords.Add(phrasesToActivate[i].phraseObjects[j].phraseObject);
                    phrasesToActivate[i].phraseObjects[j].phraseObject.captured = true;
                    phrasesToActivate[i].phraseObjects[j].phraseObject.currentMeter = 1.1f;
                }
            }
        }       
    }

    public void SpawnCurrentPhrases(int phrasesIndex)
    {

        phrasesIndex++;

        for (int i = 0; i < phrasesIndex; i++)
        {
            for (int j = 0; j < phrasesToActivate[i].phraseObjects.Count; j++)
            {
                phrasesToActivate[i].phraseObjects[j].phraseObject.gameObject.SetActive(true);

                if (phrasesToActivate[i].phraseObjects[j].spawnPoints.Count > 0)
                {
                    int spawnIndex = Random.Range(0, phrasesToActivate[i].phraseObjects[j].spawnPoints.Count);
                    phrasesToActivate[i].phraseObjects[j].phraseObject.gameObject.transform.position = phrasesToActivate[i].phraseObjects[j].spawnPoints[spawnIndex].position;
                    phrasesToActivate[i].phraseObjects[j].phraseObject.gameObject.transform.rotation = phrasesToActivate[i].phraseObjects[j].spawnPoints[spawnIndex].rotation;
                }        
            }
        }
    }

    public void DisableAllPhrases()
    {
        for(int i = 0; i < phrasesToActivate.Count; i++)
        {
            for(int j = 0; j < phrasesToActivate[i].phraseObjects.Count; j++)
            {
                phrasesToActivate[i].phraseObjects[j].phraseObject.gameObject.SetActive(false);
            }
        }
    }
   //
   // public void ActivatePhrasesByIndex(int index)
   // {
   //     if(index < phrasesToActivate.Count)
   //     {
   //         for (int j = 0; j < phrasesToActivate[index].phrasesObjects.Count; j++)
   //         {
   //             phrasesToActivate[index].phrasesObjects[j].gameObject.SetActive(true);
   //         }
   //     }
   // }

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
        //ActivatePhrasesByIndex(phrasesIndex);
    }

}

[System.Serializable]
public class PhrasesToActivate
{
    public int phrasesCount;
    public List<PhraseObject> phraseObjects = new List<PhraseObject>();
}

[System.Serializable]
public class PhraseObject 
{
    public PuzzleWord phraseObject;
    public List<Transform> spawnPoints;
}
