using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] List<PuzzlePiece> puzzlePieces;

    [SerializeField] TextMeshProUGUI counterText;

    int initialNumber;
    //private int currentCount;

    // Start is called before the first frame update
    void Start()
    {
        initialNumber = puzzlePieces.Count;
        counterText.text = "Phrases fetched " + (initialNumber - puzzlePieces.Count) + " / " + initialNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spotted(PuzzlePiece piece)
    {
        puzzlePieces.Remove(piece);

        counterText.text = "Phrases fetched " + (initialNumber - puzzlePieces.Count) + " / " + initialNumber;

        if (0 == puzzlePieces.Count)
        {
            Debug.Log("Found all puzzles");
            SceneManager.LoadScene("SimpleMainMenu");
        }
    }

}
