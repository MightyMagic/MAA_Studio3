using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BasicFpsMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    Rigidbody playerRb;
    float mouseX;
    float mouseY;

    public Quaternion currentRotation;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        Move();
        RotateViaMouse();
    }

    private void Move()
    {
        float inputValueSide = Input.GetAxisRaw("Horizontal");
        float inputValueForward = Input.GetAxisRaw("Vertical");

        Vector3 currentVelocity = (transform.forward * inputValueForward + transform.right * inputValueSide).normalized * moveSpeed;

        playerRb.velocity = new Vector3(currentVelocity.x, playerRb.velocity.y, currentVelocity.z);
    }

    void RotateViaMouse()
    {

        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;

        mouseY = Mathf.Clamp(mouseY, -20, 20);

        //playerRb.MoveRotation(Quaternion.Euler(new Vector3(-mouseY, mouseX * rotationSpeed, 0)));

        transform.rotation = Quaternion.Euler(new Vector3(-mouseY, mouseX * rotationSpeed, 0) + currentRotation.eulerAngles);
       
        
    }


    public void ResetMouse()
    {
        mouseX = 0;
        mouseY = 0;
    }
}
