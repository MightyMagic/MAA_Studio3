using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PuzzlePiece : MonoBehaviour
{
    public string puzzleText;
    public TextMeshProUGUI text;

    public Color unseenColor;
    public Color seenColor;

    [SerializeField] PuzzleManager manager;

    PhraseMovement phraseMovement;


    private void Start()
    {
        text.color = unseenColor;
        text.text = puzzleText;

        phraseMovement = GetComponent<PhraseMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "EyeSightCollider")
        {
            PuzzleSpotted();
            this.GetComponent<Collider>().enabled = false;
        }
    }

    public void PuzzleSpotted()
    {
        text.color = seenColor;
        manager.Spotted(this);
        phraseMovement.enabled = false;
    }


}
