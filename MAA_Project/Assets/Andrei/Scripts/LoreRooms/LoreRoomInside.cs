using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreRoomInside : MonoBehaviour
{
    [SerializeField] LoreRoomManager loreManager;
    [SerializeField] ItemActivation loreItem;
    bool enteredRoom = false;
    void Start()
    {
        
    }

    private void Update()
    {
        if(enteredRoom)
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
            loreItem.uiCanvas.SetActive(true);
            enteredRoom = true;
            loreManager.CloseLoreRoom();
        }
    }
}
