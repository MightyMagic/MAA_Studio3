using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PuzzleManagerAhmed : MonoBehaviour
{
    [SerializeField] PuzzleCatcher finder;
    [SerializeField] Transform playerCam;
    [SerializeField] Canvas puzzleCanvas;
    [SerializeField] puzzleCaptcherMeter meter;
    [SerializeField] GameObject catcherWindowInClosedeyes;
    [SerializeField] public List<WordInSpace> canvasTexts = new List<WordInSpace>();
    [SerializeField] List<TextMeshProUGUI> panelOrderedTexts = new List<TextMeshProUGUI>();
    [SerializeField] List<PuzzleWordSO> listOfCorrectOrder = new List<PuzzleWordSO>();
    [SerializeField] List<DuplicateTextEffect> duplicateTexts = new List<DuplicateTextEffect>();
    [SerializeField] float activationAngle = 50f;
    public bool allCanvasFilled;

    [SerializeField] int gameCompleteSceneIndex;

    private void Update()
    {
        AddWordsToCanvas();
        AddWordToPanel();
    }

    void AddWordsToCanvas()
    {
        for (int i = finder.CapturedWords.Count - 1; i >= 0; i--)
        {
            canvasTexts[i].wordInSpaceText = finder.CapturedWords[i].wordText.text;
            canvasTexts[i].textMeshPro.text = canvasTexts[i].wordInSpaceText;
        }
    }

    void AddWordToPanel()
    {
        allCanvasFilled = true;
        
        // Check if all canvas text slots are filled
        foreach (WordInSpace wordInSpace in canvasTexts)
        {
            if (string.IsNullOrEmpty(wordInSpace.wordInSpaceText))
            {
                allCanvasFilled = false;
                break;
            }
        }

        // Add word to panel if conditions are met
        for (int i = 0; i < canvasTexts.Count; i++)
        {
            Vector3 textDirection = canvasTexts[i].transform.position - playerCam.transform.position;
            float angle = Vector3.Angle(playerCam.forward, textDirection);

            if (angle < activationAngle && Input.GetKey(KeyCode.Mouse1)
                && !canvasTexts[i].wordIsInPanel
                && canvasTexts[i].wordInSpaceText != ""
                && allCanvasFilled)
            {
                foreach (TextMeshProUGUI text in panelOrderedTexts)
                {
                    if (text.text == canvasTexts[i].wordInSpaceText)
                    {
                        canvasTexts[i].wordIsInPanel = true;
                        break;
                    }
                }

                catcherWindowInClosedeyes.SetActive(true);
                meter.PuzzleCatcherMeterUpdaterInClosedEyes(canvasTexts[i]);

                if (canvasTexts[i].wordSpaceCurrentMeter >= meter.meterThreshold)
                {
                    Debug.Log("Adding word: " + canvasTexts[i].wordInSpaceText);
                    for (int j = 0; j < panelOrderedTexts.Count; j++)
                    {
                        if (string.IsNullOrEmpty(panelOrderedTexts[j].text))
                        {
                            panelOrderedTexts[j].text = canvasTexts[i].wordInSpaceText;
                            break;
                        }
                    }
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
            else if (canvasTexts[i].wordIsInPanel && !Input.GetKey(KeyCode.Mouse1))
            {
                catcherWindowInClosedeyes.SetActive(false);
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
            // Delete next line later
            SceneManager.LoadScene(gameCompleteSceneIndex);
        }
        else
        {
            Debug.Log("False");
            foreach (TextMeshProUGUI text in panelOrderedTexts)
            {
                text.text = "";
                ClearBools();
                foreach(DuplicateTextEffect dupText in duplicateTexts)
                {
                    dupText.ResetDuplicateTextPos();
                }
            }
        }
    }
    void ClearBools()
    {
        foreach(WordInSpace captured in canvasTexts)
        {
            captured.wordIsInPanel = false;
        }
    }

}
