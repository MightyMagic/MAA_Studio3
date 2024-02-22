using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Windows;

public class BasicRoomShuffle : MonoBehaviour
{
    [SerializeField] UnitShuffle largeShuffle;
    [SerializeField] GameObject[] rooms;
    [SerializeField] AnchorObject[] roomsAnchors;

    [SerializeField] LayerMask groundLayer;

    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject monsterObject;

    Vector3 relativePosition;
    GameObject currentFloor;

    Vector3[] relativePositions = new Vector3[2];
    [SerializeField] Transform emptyMark;
    [SerializeField] Transform emptyMark1;

    private int currentRoomIndex;
    void Start()
    {
        //ReshuffleRooms();
    }

    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            ReshuffleRooms();
        }
    }

    public void ReshuffleRooms()
    {

        RaycastHit hit;
        if (Physics.Raycast(playerObject.transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            relativePositions[0] = hit.point;
            currentFloor = hit.transform.gameObject;

            emptyMark.SetParent(null, true);
            //Vector3 localPosition = currentFloor.transform.InverseTransformPoint(hit.transform.position);
            emptyMark.transform.position = relativePositions[0];
            emptyMark.SetParent(currentFloor.transform);

            Debug.Log("Relative Position: " + relativePosition);
            Debug.Log(hit.transform.name);
        }

        if (Physics.Raycast(monsterObject.transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            relativePositions[1] = hit.point;
            currentFloor = hit.transform.gameObject;

            emptyMark1.SetParent(null, true);
            //Vector3 localPosition = currentFloor.transform.InverseTransformPoint(hit.transform.position);
            emptyMark1.transform.position = relativePositions[1];
            emptyMark1.SetParent(currentFloor.transform);

            Debug.Log("Relative Position: " + relativePositions[1]);
            Debug.Log(hit.transform.name);
        }

        largeShuffle.RearrangeUnits();

        for (int i = 0; i < roomsAnchors.Length; i++)
        {
            int k = UnityEngine.Random.Range(0, roomsAnchors.Length);
            var value = roomsAnchors[k];
            roomsAnchors[k] = roomsAnchors[i];
            roomsAnchors[i] = value;
        }

        for(int i = 0;i < roomsAnchors.Length; i++)
        {
            rooms[i].transform.position = roomsAnchors[i].anchor.transform.position;

            if (roomsAnchors[i].isRotated)
            {
                rooms[i].transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
            else
            {
                rooms[i].transform.rotation = Quaternion.Euler(0, 0f, 0);
            }
        }

        emptyMark.SetParent(null, true);
        relativePositions[0] = emptyMark.transform.position;
        playerObject.transform.position = new Vector3(relativePositions[0].x, playerObject.transform.position.y, relativePositions[0].z);

        emptyMark1.SetParent(null, true);
        relativePositions[1] = emptyMark1.transform.position;
        monsterObject.transform.position = new Vector3(relativePositions[1].x, monsterObject.transform.position.y, relativePositions[1].z);
    }
    

    void MoveTowardsRelativePosition(GameObject go)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < roomsAnchors.Length; i++)
        {
            Gizmos.DrawLine(roomsAnchors[i].anchor.transform.position, roomsAnchors[(i + 1) % roomsAnchors.Length].anchor.transform.position);
        }
    }
}

[System.Serializable]
public class AnchorObject
{
    public GameObject anchor;
    public bool isRotated;
}
