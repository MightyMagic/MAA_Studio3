using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhraseMovement : MonoBehaviour
{
    [SerializeField] GameObject initialWall;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] GameObject puzzleObject;

    [SerializeField] float topYCoordinate;
    [SerializeField] float bottomYCoordinate;

    Vector3 initialOffset;
    Quaternion initialRotation;

    [SerializeField] float speedMagnitudeY;
    [SerializeField] float speedMagnitudeV;
    [SerializeField] float velocitySwapTimer;
    float fluctuationTimer = 0f;
    Vector3 currentVelocity;
    GameObject currentWall;
    int selectedInteger;

    void Start()
    {
        // DetermineWall();
        // initialOffset = transform.position - currentWall.transform.position;

        initialRotation = transform.localRotation;
        initialOffset = transform.position - initialWall.transform.position;

        RandomizeVelocity();
        //puzzleObject = GetComponent<GameObject>();

    }

    void Update()
    {
        fluctuationTimer += Time.deltaTime;

        if(fluctuationTimer > velocitySwapTimer)
        {
            RandomizeVelocity();
            fluctuationTimer = 0f;
        }

        if(transform.position.y < bottomYCoordinate || transform.position.y > topYCoordinate)
        {
            currentVelocity = new Vector3(currentVelocity.x, -currentVelocity.y, currentVelocity.z);
        }

        transform.position += currentVelocity * Time.deltaTime;

        DetermineNextWall();
    }

    void RandomizeVelocity()
    {
        int randomIndex = Random.Range(0, 2);
        selectedInteger = randomIndex == 0 ? 1 : -1;

        currentVelocity = transform.forward * speedMagnitudeV * selectedInteger + transform.up * selectedInteger * speedMagnitudeY;
        
    }



    void DetermineNextWall()
    {
        RaycastHit hit;
        if (Physics.Raycast(puzzleObject.transform.position, currentVelocity, out hit, 1.5f, wallLayer))
        {
            currentWall = hit.transform.gameObject;

            //puzzleObject.transform.SetParent(null, true);        
            //puzzleObject.transform.SetParent(currentWall.transform);


            //puzzleObject.transform.localRotation = hit.transform.rotation;
            
            if(selectedInteger > 0)
            {
                puzzleObject.transform.localRotation *= Quaternion.Euler(0, 90f, 0);

            }
            else
            {
                puzzleObject.transform.localRotation *= Quaternion.Euler(0, -90f, 0);

            }


            RandomizeVelocity();
            Debug.DrawLine(puzzleObject.transform.position, puzzleObject.transform.position + currentVelocity, Color.red);
        }
        else
        {
            Debug.DrawLine(puzzleObject.transform.position, puzzleObject.transform.position + currentVelocity, Color.green);
        }
    }
}
