using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eyes : MonoBehaviour
{
    [SerializeField] KeyCode eyesKey;

    [SerializeField] MonsterDirector monsterDirector;
    [SerializeField] ChunkShuffle chunkShuffle;

    [Header("Cameras")]
    [SerializeField] Camera openEyesCamera;
    [SerializeField] Camera closedEyesCamera;

    public bool eyesClosed;

    [Header("External objects")]
    [SerializeField] Slider anxietySlider;

    public bool timerRunning;
    public float timer;

    [SerializeField] int[] timings;
    private int timingIndex;

    [SerializeField] ChunkShuffle shuffler;

    void Start()
    {
        CloseEyes(false);

        timer = 0f;

        eyesClosed = false;
        timerRunning = true;

        anxietySlider.maxValue = timings[0];
        anxietySlider.value = timings[0] - timer;
    }

    void Update()
    {
        // Eye controls input
        if(Input.GetKeyDown(eyesKey) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            eyesClosed = true;

            timer += 0.5f;

            CloseEyes(true);
        }
        else if(Input.GetKeyUp(eyesKey) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            eyesClosed= false;

            CloseEyes(false);
        }

        // Tracking the anxiety meter
        if (eyesClosed && timerRunning)
        {
            timer += Time.deltaTime;
            anxietySlider.value = timings[timingIndex] - timer;

            if (timer > timings[timingIndex])
            {
                //timerRunning = false;
                timer = 0f;

                timingIndex += 1;
                if (timingIndex == timings.Length)
                    timingIndex = timings.Length - 1;

                anxietySlider.maxValue = timings[timingIndex];

                //shuffler.RearrangeChunks();
                //StartCoroutine(shuffler.RebuildGrid());

                shuffler.FullLayoutSwap();
            }
        }
    }

    void CloseEyes(bool state)
    {
      
        openEyesCamera.enabled = !state;
        closedEyesCamera.enabled = state;
    }
}
