using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtChecker : MonoBehaviour
{
    public Transform playerCamera;
    public Transform itemToCheck;
    public float lookThreshold = 0.0f; // Adjust this value if needed

    void Update()
    {
        // Calculate the direction vector from the camera to the item in world space
        Vector3 toItem = itemToCheck.position - playerCamera.position;

        // Transform the direction into the item's local space
        Vector3 toLocal = itemToCheck.InverseTransformPoint(playerCamera.position);

        // Check the sign of the z-component to determine if looking from front or behind
        bool lookingFromFront = toLocal.z > lookThreshold;

        // Use the result as needed
        if (lookingFromFront)
        {
            // You are looking at the item from the front along its z-axis
            Debug.Log("Looking from the front");
        }
        else
        {
            // You are looking at the item from behind along its z-axis
            Debug.Log("Looking from behind");
        }
    }
}
