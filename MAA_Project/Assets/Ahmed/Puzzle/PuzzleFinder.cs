using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PuzzleFinder : MonoBehaviour
{
    [SerializeField] puzzleCatcherMeter puzzleMeter;
    [SerializeField] EyesControls eyes;
    [SerializeField] Transform playerCamera;
    float lookThreshold = 0.0f;
    public Transform puzzleText;
    public GameObject puzzleWindow;
    public float activationAngle = 30f;
    public float captureAngle = 10f;
    public float activationDistance = 10f;
    float angle;

    private void Update()
    {
        ActivatePuzzleWindow();
    }
    void ActivatePuzzleWindow()
    {
        float distatnce = Vector3.Distance(playerCamera.position, puzzleText.position);
        Vector3 cameraToPuzzleText = puzzleText.position - playerCamera.position;
        cameraToPuzzleText.Normalize();

        Vector3 toLocal = puzzleText.InverseTransformPoint(playerCamera.position);

        bool lookingFromFront = toLocal.z > lookThreshold;

        angle = Vector3.Angle(playerCamera.forward, cameraToPuzzleText);

        // in the text setup we are looking at the text from behind of its local position.
        if (distatnce <= activationDistance && !eyes.eyesClosed) 
        {
            if(angle < activationAngle && !lookingFromFront)
            {
                puzzleWindow.SetActive(true);
                puzzleMeter.gameObject.SetActive(true);
                if (angle < captureAngle && !lookingFromFront)
                {
                    puzzleMeter.PuzzleCatcherMeterUpdater();
                   if(puzzleMeter.currentMeter >= puzzleMeter.meterThreshold)
                    {
                        Debug.Log("you captured the text");
                        puzzleWindow.SetActive(false);
                        puzzleMeter.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            puzzleWindow.SetActive(false);
            puzzleMeter.gameObject.SetActive(false);
        }
    }
}
