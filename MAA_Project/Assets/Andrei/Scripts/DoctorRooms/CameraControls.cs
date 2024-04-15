using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] float mouseVerticalSpeed;
    [SerializeField] float mouseHorizontalSpeed;

    float verticalRotStore;
    float horizontalRotStore;

    Quaternion initialRotation;

    [Header("Camera restrictions")]
    [SerializeField] float bottomXCamera;
    [SerializeField] float topXCamera;

    [SerializeField] float leftCamera;
    [SerializeField] float rightCamera;

    void Start()
    {
        initialRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookCamera();
    }

    void LookCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
        //    transform.rotation.eulerAngles.y + mouseX * mouseHorizontalSpeed,
        //    transform.rotation.eulerAngles.z);

        horizontalRotStore += mouseX * mouseHorizontalSpeed;
        horizontalRotStore = Mathf.Clamp(horizontalRotStore, -leftCamera, rightCamera);

        verticalRotStore -= mouseY * mouseVerticalSpeed;
        verticalRotStore = Mathf.Clamp(verticalRotStore, -bottomXCamera, topXCamera);

            transform.rotation = initialRotation * Quaternion.Euler(verticalRotStore,
            horizontalRotStore,
            transform.rotation.eulerAngles.z);
    }
}
