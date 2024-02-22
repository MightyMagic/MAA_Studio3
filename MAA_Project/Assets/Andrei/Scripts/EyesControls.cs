using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


/// <summary>
/// Ideally should be done via observer
/// </summary>
public class EyesControls : MonoBehaviour
{
    [SerializeField] GameObject monster;
    [SerializeField] SimpleMonster monsterLogic;

    [SerializeField] GameObject[] puzzleObjects;
    [SerializeField] string tagToFind;

    [SerializeField] Slider anxietySlider;
    [SerializeField] Image grayNoise;

    public bool eyesClosed;
    public bool timerRunning;
    public float timer;

    [SerializeField] int[] timings;
    private int timingIndex;
    [SerializeField] BasicRoomShuffle roomShuffle;

    void Start()
    {
        timer = 0f;

        eyesClosed = false;
        timerRunning = true;

        anxietySlider.maxValue = timings[0];
        anxietySlider.value = timings[0] - timer;

        ChangeUI(false);
        ShowMonster(false);

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        SwapMeshes();

        if (eyesClosed && timerRunning)
        {
            timer += Time.deltaTime;
            anxietySlider.value = timings[timingIndex] - timer;

            if (timer > timings[timingIndex])
            {
                timerRunning = false;
                timer = 0f;

                timingIndex += 1;
                if(timingIndex == timings.Length)
                    timingIndex = timings.Length - 1;

                anxietySlider.maxValue = timings[timingIndex];
                monsterLogic.ChooseWanderingPoint();
                roomShuffle.ReshuffleRooms();
            }
        }

        if (eyesClosed)
        {
            monsterLogic.WanderAround();
        }
        else
        {
            monsterLogic.ChasePlayer();
        }


    }

    private void SwapMeshes()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !eyesClosed)
        {
            DisableMeshes(PopulateArrayWithTag(tagToFind));
            eyesClosed = true;

            timer += 0.5f;

            ChangeUI(true);
            ActivatePuzzles(puzzleObjects, false);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && eyesClosed)
        {
            EnableMeshes(PopulateArrayWithTag(tagToFind));

            eyesClosed = false;
            timerRunning = true;

            ChangeUI(false);
            ActivatePuzzles(puzzleObjects, true);
        }
    }

    private GameObject[] PopulateArrayWithTag(string tagToFind)
    {
        GameObject[] tagArray = GameObject.FindGameObjectsWithTag(tagToFind);

        if (tagArray.Length == 0)
            Debug.LogError("Array of meshes is empty");

        return tagArray;
    }

    private void EnableMeshes(GameObject[] meshObjects)
    {
        ShowMonster(false);
        foreach (GameObject obj in meshObjects)
        {
            //Transform meshT = obj.transform;
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;

            //foreach (Transform child in meshT)
            //{
            //    MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
            //
            //    if (meshRenderer != null)
            //    {
            //        meshRenderer.enabled = true;
            //    }
            //}

        }
    }
    
    private void DisableMeshes(GameObject[] meshObjects)
    {
        ShowMonster(true);
        foreach (GameObject obj in meshObjects)
        {
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
        }
    }

    private void ShowMonster(bool showMonster)
    {
        MeshRenderer monsterMesh = monster.GetComponent<MeshRenderer>();
        if (showMonster)
        { 
            monsterMesh.enabled = true;
        }
        else
        {
            monsterMesh.enabled = false;
        }
    }

    private void ActivatePuzzles(GameObject[] puzzles, bool activate)
    {
        if(activate)
        {
            foreach (GameObject obj in puzzles)
            {
                obj.SetActive(true);
            }
        }
        else { foreach (GameObject obj in puzzles) { obj.SetActive(false); } }
    }

    private void ChangeUI(bool b)
    {
        grayNoise.enabled = b;
        anxietySlider.gameObject.SetActive(b);
    }
}
