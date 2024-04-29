using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhraseCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI phraseCount;
   [SerializeField] AudioClip gotPhraseAudio;
   
   [SerializeField] AudioSource phraseSource;

    LoreRoomManager roomManager;

    [Header("Debug")]
    public int capturedPhrases = 0;
    public int startingPhrases = 0;
    public int totalPhrases = 0;

    [Header("Tutorial")]
    [SerializeField] int phrasesToFetch;
    [SerializeField] List<PuzzleWord> puzzleWords;
    bool tutorial;

    void Start()
    {
        roomManager = GetComponent<LoreRoomManager>();
        phraseCount.gameObject.SetActive(false);


        if(roomManager != null )
        {
            //capturedPhrases = roomManager.
            startingPhrases = roomManager.alreadyFetchedCount;
            capturedPhrases = startingPhrases;
            totalPhrases = roomManager.TotalToFetch;

            tutorial = false;
        }
        else
        {
            tutorial = true;

            for (int i = 0; i < phrasesToFetch; i++)
            {         
                puzzleWords[i].gameObject.SetActive(false);
            }

            startingPhrases = 0;
            capturedPhrases = 0;
            totalPhrases = phrasesToFetch;

            for(int i = 0; i < phrasesToFetch; i++)
            {
                if (i == capturedPhrases & !puzzleWords[i].gameObject.activeInHierarchy)
                    puzzleWords[i].gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator CapturedPhrase()
    {
        capturedPhrases++;
        phraseCount.text = capturedPhrases.ToString() + " / " + totalPhrases.ToString();
        phraseCount.gameObject.SetActive(true);
        phraseSource.PlayOneShot(gotPhraseAudio);

        yield return new WaitForSeconds(gotPhraseAudio.length + 0.2f);
        phraseCount.gameObject.SetActive(false);

        if(tutorial)
        {
            for (int i = 0; i < phrasesToFetch; i++)
            {
                if (i == capturedPhrases & !puzzleWords[i].gameObject.activeInHierarchy)
                    puzzleWords[i].gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        
    }

    //public
}
