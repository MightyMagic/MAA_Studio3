using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovmentAhmed : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float mouseVerticalSpeed;
    [SerializeField] float mouseHorizontalSpeed;
    [SerializeField] Transform lookPoint;
    [SerializeField] bool openPuzzleWindow;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject puzzleSpace;
    float verticalRotStore;
    Vector3 moveInput;
    Camera cam;
    [SerializeField] Camera eyesCamera;
    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    private void LateUpdate()
    {
        if (cam == null) cam = Camera.main;

        cam.transform.position = lookPoint.position;
        cam.transform.rotation = lookPoint.rotation;

        eyesCamera.transform.position = lookPoint.position;
        eyesCamera.transform.rotation = lookPoint.rotation;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) 
        {
            openPuzzleWindow = true;
            puzzleSpace.SetActive(true);
            canvas.enabled = true;
        }
        else
        {
            openPuzzleWindow = false;
            puzzleSpace.SetActive(false);
            canvas.enabled = false;
        }
        MovePlayer();
        LookCamera();
    }
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(horizontalInput, 0, verticalInput);
        moveInput.Normalize();

        Vector3 moveDirection = transform.TransformDirection(moveInput);
        CharacterController charCon = GetComponent<CharacterController>();
        charCon.Move((moveDirection * playerSpeed) * Time.deltaTime);
    }
    void LookCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + mouseX * mouseHorizontalSpeed,
            transform.rotation.eulerAngles.z);

        verticalRotStore -= mouseY * mouseVerticalSpeed;
        verticalRotStore = Mathf.Clamp(verticalRotStore, -80, 80);

        lookPoint.rotation = Quaternion.Euler(verticalRotStore,
            lookPoint.rotation.eulerAngles.y,
            lookPoint.rotation.eulerAngles.z);
    }
}
