using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleManagerAhmed : MonoBehaviour
{
    [SerializeField] PuzzleFinder finder;
    [SerializeField] Canvas puzzleCanvas;
    [SerializeField] List<TextMeshProUGUI> canvasTexts = new List<TextMeshProUGUI>();
    [SerializeField] List<TextMeshProUGUI> panelOrderedTexts = new List<TextMeshProUGUI>();
    [SerializeField] List<PuzzleWordSO> listOfCorrectOrder = new List<PuzzleWordSO>();

    private void Update()
    {
        AddWordsToCanvas();
        AddWordToPanel();
    }

    void AddWordsToCanvas()
    {
        for (int i = finder.CapturedWords.Count - 1; i >= 0; i--)
        {
            canvasTexts[i].text = finder.CapturedWords[i].wordText.text;
        }
    }

    void AddWordToPanel()
    {
        for (int i = 0; i < canvasTexts.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                for (int j = 0; j < panelOrderedTexts.Count; j++)
                {
                    if (string.IsNullOrEmpty(panelOrderedTexts[j].text))
                    {
                        panelOrderedTexts[j].text = canvasTexts[i].text;
                        break;
                    }
                }
                // Check if all panel slots are filled after adding a word
                bool allFilled = true;
                foreach (TextMeshProUGUI text in panelOrderedTexts)
                {
                    if (string.IsNullOrEmpty(text.text))
                    {
                        allFilled = false;
                        break;
                    }
                }
                if (allFilled)
                {
                    ComparePanelWithCorrectOrder();
                }
            }
        }
    }

    void ComparePanelWithCorrectOrder()
    {
        bool allMatch = true;
        for (int i = 0; i < panelOrderedTexts.Count; i++)
        {
            if (panelOrderedTexts[i].text != listOfCorrectOrder[i].Word)
            {
                allMatch = false;
                break;
            }
        }

        if (allMatch)
        {
            Debug.Log("Success");
        }
        else
        {
            Debug.Log("False");
            foreach (TextMeshProUGUI text in panelOrderedTexts)
            {
                text.text = "";
            }
        }
    }
}
