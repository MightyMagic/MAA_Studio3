using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Windows;

public class BasicRoomShuffle : MonoBehaviour
{
    [SerializeField] GameObject[] rooms;
    [SerializeField] AnchorObject[] roomsAnchors;

    private int currentRoomIndex;
    void Start()
    {
        ReshuffleRooms();
    }

    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            ReshuffleRooms();
        }
    }

    void ReshuffleRooms()
    {
        for(int i = 0; i < roomsAnchors.Length; i++)
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
