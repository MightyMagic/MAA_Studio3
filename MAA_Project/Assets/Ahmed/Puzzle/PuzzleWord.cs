using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleWord : MonoBehaviour
{
    [SerializeField] PuzzleWordSO wordSO;
    public TextMeshProUGUI wordText;
    public bool captured;
    public float currentMeter = 0;

    private void Start()
    {
        wordText = GetComponentInChildren<TextMeshProUGUI>();
        wordText.text = wordSO.Word;
    }
}
