using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleCatcher : MonoBehaviour
{
    [SerializeField] puzzleCaptcherMeter puzzleMeter;
    //[SerializeField] EyesControls eyes;
    [SerializeField] Transform playerCamera;
    float lookThreshold = 0.0f;
    [SerializeField] List<PuzzleWord> puzzleWords = new List<PuzzleWord>();
    public List<PuzzleWord> CapturedWords = new List<PuzzleWord>();
    public GameObject puzzleWindow;
    public float activationAngle = 30f;
    public float captureAngle = 10f;
    public float activationDistance = 6f;
    bool currentIsCaptured;
    float angle;

    private void Update()
    {
        CapturePuzzlesOnWalls();
    }
    void CapturePuzzlesOnWalls()
    {
        for(int i = puzzleWords.Count - 1; i >= 0; i--)
        {
            float distatnce = Vector3.Distance(playerCamera.position, puzzleWords[i].transform.position);
            Vector3 cameraToPuzzleText = puzzleWords[i].transform.position - playerCamera.position;
            cameraToPuzzleText.Normalize();

            Vector3 toLocal = puzzleWords[i].transform.InverseTransformPoint(playerCamera.position);

            bool lookingFromFront = toLocal.z > lookThreshold;

            angle = Vector3.Angle(playerCamera.forward, cameraToPuzzleText);

            // in the text setup we are looking at the text from behind of its local position.
            if (distatnce <= activationDistance && !puzzleWords[i].captured)
            {
               currentIsCaptured = false;
                if (angle < activationAngle && !lookingFromFront && !puzzleWords[i].captured)
                {
                    puzzleWindow.SetActive(true);
                    puzzleMeter.gameObject.SetActive(true);
                    if (angle < captureAngle && !lookingFromFront && !puzzleWords[i].captured)
                    {
                        puzzleMeter.PuzzleCatcherMeterUpdater(puzzleWords[i]);
                        if (puzzleWords[i].currentMeter >= puzzleMeter.meterThreshold && !puzzleWords[i].captured)
                        {
                            Debug.Log("you captured the text");
                            CapturedWords.Add(puzzleWords[i]);
                            puzzleWords[i].captured = true;
                            currentIsCaptured = puzzleWords[i].captured;
                           // puzzleWords[i].gameObject.SetActive(false);
                                                     
                        }
                    }
                    else
                    {
                        puzzleWindow.SetActive(false);
                        puzzleMeter.gameObject.SetActive(false);
                    }
                }
            }
           
        }
        if (currentIsCaptured)
        {
            puzzleWindow.SetActive(false);
            puzzleMeter.gameObject.SetActive(false);  
        }
    }    
}
