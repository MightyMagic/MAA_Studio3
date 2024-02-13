using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BasicFpsMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    Rigidbody playerRb;
    float mouseX;
    float mouseY;

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

        mouseX += Input.GetAxis("Mouse X");
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;

        mouseY = Mathf.Clamp(mouseY, -20, 20);

        transform.rotation = Quaternion.Euler(new Vector3(-mouseY, mouseX * rotationSpeed, 0));// * transform.parent.parent.rotation;
    }
}
