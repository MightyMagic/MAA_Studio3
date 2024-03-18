using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInPlace : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;

    [SerializeField] BasicFpsMovement movement;


    private GameObject markObject;
    Quaternion newAngle;

    Quaternion oldRotation;
    Quaternion newRotation;

    void Start()
    {
        markObject = new GameObject("mark_" + this.name);
    }

    void Update()
    {
        
    }

    public void FetchPosition()
    {
        Debug.LogError("Fetching position of " + markObject.name);

        RaycastHit hit;
        if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {

            oldRotation = markObject.transform.rotation;
            markObject.transform.position = hit.point;

            Transform currentFloor = hit.transform;

            markObject.transform.parent = currentFloor.transform;
          
        }
    }

    public void MoveToPosition()
    {
        if(markObject != null)
        {

            newRotation = markObject.transform.rotation;
            newAngle = newRotation * Quaternion.Inverse(oldRotation);

           
            //if(gameObject.name == "Player")
            //{
            //    movement.currentRotation = newRotation;
            //}
            //else
            //{
            //    gameObject.transform.rotation = newAngle * gameObject.transform.rotation;
            //}

            gameObject.transform.rotation = newAngle * gameObject.transform.rotation;



            //gameObject.transform.rotation = newAngle * gameObject.transform.rotation;
            //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, markObject.transform.rotation.y, 0f));



            gameObject.transform.position = new Vector3(markObject.transform.position.x, gameObject.transform.position.y, markObject.transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        // Set the color of the Gizmo
        Gizmos.color = Color.blue;

        // Get the position of the GameObject
        Vector3 startPos = this.gameObject.transform.position;

        // Get the direction the GameObject is facing
        Vector3 direction = Vector3.down;

        // Draw the ray in the scene view
        Gizmos.DrawRay(startPos, direction * 5f);
    }
}

/*
  //newAngle = Quaternion.Euler(newRotation.x - oldRotation.x, newRotation.y - oldRotation.y, newRotation.z - oldRotation.z);

            print("Old player angle: " + gameObject.transform.rotation.x + ", " + gameObject.transform.rotation.y + ", " + gameObject.transform.rotation.z);

            print("Old angle: " + oldRotation.x + ", " + oldRotation.y + ", " + oldRotation.z);
            print("New angle: " +  newRotation.x + ", " + newRotation.y + ", " + newRotation.z);
            print("Difference: " + newAngle.x + ", " + newAngle.y + ", " + newAngle.z);

print("New player angle: " + gameObject.transform.rotation.x + ", " + gameObject.transform.rotation.y + ", " + gameObject.transform.rotation.z);
            //gameObject.transform.rotation = newAngle * gameObject.transform.rotation;
*/
