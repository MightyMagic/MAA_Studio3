using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eyes : MonoBehaviour
{
    [SerializeField] KeyCode eyesKey;

    [SerializeField] MonsterDirector monsterDirector;
    [SerializeField] ChunkShuffle chunkShuffle;

    [Header("Eyes delay")]
    [SerializeField] float delayBetweenClosing;
    [SerializeField] float delayBeforeClosing;
    float closeTimer = 0f;
    float closingEyesTimer = 0f;
    bool closingEyes = false;

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

    PlayerMovmentAhmed movementScript;
    float initialMovementSpeed;

    void Start()
    {

        closeTimer = delayBetweenClosing + 0.1f;

        CloseEyes(false);

        timer = 0f;

        eyesClosed = false;
        timerRunning = true;

        anxietySlider.maxValue = timings[0];
        anxietySlider.value = timings[0] - timer;

        movementScript = GetComponent<PlayerMovmentAhmed>();
        initialMovementSpeed = movementScript.playerSpeed;
    }

    void Update()
    {
        // Eye controls input
        if(Input.GetKeyDown(eyesKey) || Input.GetKeyDown(KeyCode.Mouse1) && closeTimer > delayBetweenClosing)
        {
            closeTimer = 0f;

            eyesClosed = true;

            //timer += 2.5f;

            movementScript.playerSpeed = initialMovementSpeed * 0.4f;

            CloseEyes(true);
        }
        else if(Input.GetKeyUp(eyesKey) || Input.GetKeyUp(KeyCode.Mouse1) && !closingEyes)
        {
            closingEyes = true;
            //eyesClosed= false;
            
        }

        if(closingEyes)
        {
            closingEyesTimer += Time.deltaTime;
            if(closingEyesTimer > delayBeforeClosing)
            {

                closingEyesTimer = 0f;
                eyesClosed = false;
                closingEyes = false;
                movementScript.playerSpeed = initialMovementSpeed;
                CloseEyes(false);
            }
        }

        closeTimer += Time.deltaTime;

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

                //shuffler.FullLayoutSwap();
                StartCoroutine(shuffler.FullLAyoutSwapCoroutine());
            }
        }
    }

    void CloseEyes(bool state)
    {
      
        openEyesCamera.enabled = !state;
        closedEyesCamera.enabled = state;
    }
}
