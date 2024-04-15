using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 200f;
 
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RotateObject();
        }
    }
    void RotateObject()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(Vector3.up, -mouseX * rotateSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.right, mouseY * rotateSpeed * Time.deltaTime, Space.Self);

    }
}
