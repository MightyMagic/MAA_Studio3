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


    private void Start()
    {
        text.color = unseenColor;
        text.text = puzzleText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            PuzzleSpotted();
            this.GetComponent<Collider>().enabled = false;
        }
    }

    public void PuzzleSpotted()
    {
        text.color = seenColor;
        manager.Spotted(this);
    }


}
