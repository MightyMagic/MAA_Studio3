using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreRoomInside : MonoBehaviour
{
    [SerializeField] LoreRoomManager loreManager;
    bool enteredRoom = false;
    void Start()
    {
        
    }

    private void Update()
    {
        if(enteredRoom & Input.GetKeyDown(KeyCode.L))
        {
            enteredRoom = false;    
            loreManager.LoreRoomComplete();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entered the room");
            enteredRoom = true;
            loreManager.CloseLoreRoom();
        }
    }
}
