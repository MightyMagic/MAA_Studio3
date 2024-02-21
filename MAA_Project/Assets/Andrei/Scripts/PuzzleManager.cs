using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] List<PuzzlePiece> puzzlePieces;

    //private int currentCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spotted(PuzzlePiece piece)
    {
        puzzlePieces.Remove(piece);
        if(0 == puzzlePieces.Count)
        {
            Debug.Log("Found all puzzles");
        }
    }

}
