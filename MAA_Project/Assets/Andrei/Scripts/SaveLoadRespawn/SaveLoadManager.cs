using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager saveLoad { get; private set; }

    [Header("Player and monster")]
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject monsterObject;

    [Header("Level layout")]
    [SerializeField] ChunkShuffle shuffler;

    [Header("Phrases")]
    [SerializeField] LoreRoomManager roomManager;
    [SerializeField] PuzzleManagerAhmed puzzleManager;

    [Header("Player spawn")]
    [SerializeField] WakeUpInBed wakeUpScript;
    [SerializeField] Transform loreRoomSpawn;

    [Header("Monster spawn")]
    [SerializeField] List<Transform> monsterSpawnPoints;

    private void Awake()
    {

        if (saveLoad != null && saveLoad != this)
        {
            Destroy(this);
        }
        else
        {
            saveLoad = this;
        }

        //RestartMainLevel();
        StartCoroutine(StartLevel());
    }

    private void Update()
    {
       //if (Input.GetKeyDown(KeyCode.Q))
       //{
       //    PlayerPrefs.SetInt("PhraseIndex", roomManager.phraseIndex);
       //    StartCoroutine(StartLevel());
       //    //RestartMainLevel();
       //}
    }

    public void RestartMainLevel()
    {
        StopAllCoroutines();

        // Disable player and monster
        DisablePlayerAndMonster(true);

        // Place the level, build grid
        shuffler.SpawnFirstLayout();

        // Spawn player
        //playerObject.SetActive(true);
        SpawnPlayer();
        // either near lore room door or in bed

        // I assign a corresponding player pref in the game over, so if null or just from flashback -> lore room
        // if another -> bed

        // Track phrases and spawn them
        roomManager.DisableAllPhrases();
        roomManager.FecthPreviousPhrases(roomManager.LatestPhraseIndex());
        roomManager.SpawnCurrentPhrases(roomManager.LatestPhraseIndex());

        // Spawn monster
        monsterObject.transform.position = monsterSpawnPoints[Random.Range(0, monsterSpawnPoints.Count)].position;

        // Enable them
        DisablePlayerAndMonster(false);
    }

    public IEnumerator StartLevel()
    {
        StopAllCoroutines();

        // Disable player and monster
        DisablePlayerAndMonster(true);

        // Place the level, build grid
        shuffler.SpawnFirstLayout();

        // Spawn player
        //playerObject.SetActive(true);
        if (PlayerPrefs.HasKey("PlayerSpawn"))
        {
            switch (PlayerPrefs.GetInt("PlayerSpawn"))
            {
                case 0:
                    playerObject.transform.position = loreRoomSpawn.position; playerObject.SetActive(true); break; //yield return new WaitForSeconds(0.5f); 
                case 1:
                    StartCoroutine(wakeUpScript.WakeUp()); break;
            }        
        }
        else
        {
            //yield return new WaitForSeconds(0.5f);
            PlayerPrefs.SetInt("PlayerSpawn", 0);
            playerObject.transform.position = loreRoomSpawn.position;
        }
        // either near lore room door or in bed

        // I assign a corresponding player pref in the game over, so if null or just from flashback -> lore room
        // if another -> bed

        // Track phrases and spawn them
        roomManager.DisableAllPhrases();
        roomManager.FecthPreviousPhrases(roomManager.LatestPhraseIndex());
        roomManager.SpawnCurrentPhrases(roomManager.LatestPhraseIndex());

        // Spawn monster
        monsterObject.transform.position = monsterSpawnPoints[Random.Range(0, monsterSpawnPoints.Count)].position;
        monsterObject.SetActive(true);

        // Enable them
        //DisablePlayerAndMonster(false);
        yield return null;
    }

    void DisablePlayerAndMonster(bool argument)
    {
        playerObject.SetActive(!argument);
        monsterObject.SetActive(!argument);
    }

    public void SpawnPlayer()
    {
        if (PlayerPrefs.HasKey("PlayerSpawn"))
        {
            switch (PlayerPrefs.GetInt("PlayerSpawn")){
                case 0: 
                    playerObject.transform.position = loreRoomSpawn.position;
                    
                    break;
                case 1:
                    StartCoroutine(wakeUpScript.WakeUp()); break;
            }
        }
        else
        {
            PlayerPrefs.SetInt("PlayerSpawn", 1);
            playerObject.transform.position = loreRoomSpawn.position;
        }
    }
}
