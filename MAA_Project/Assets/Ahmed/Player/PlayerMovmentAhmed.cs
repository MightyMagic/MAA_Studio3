using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovmentAhmed : MonoBehaviour
{
    public float playerSpeed;
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

    [Header("Camera restrictions")]
    [SerializeField] float bottomXCamera;
    [SerializeField] float topXCamera;
    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        Cursor.lockState = CursorLockMode.Locked;

        //if (PlayerPrefs.HasKey("PlayerSpeed"))
        //{
        //    playerSpeed = PlayerPrefs.GetFloat("PlayerSpeed");
        //}
        
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
            if(canvas != null & puzzleSpace != null)
            {
                puzzleSpace.SetActive(true);
                canvas.enabled = true;
            }
        }
        else
        {
            openPuzzleWindow = false;


           if (canvas != null & puzzleSpace != null)
           {
               puzzleSpace.SetActive(false);
               canvas.enabled = false;
           }
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
        verticalRotStore = Mathf.Clamp(verticalRotStore, -bottomXCamera, topXCamera);

        lookPoint.rotation = Quaternion.Euler(verticalRotStore,
            lookPoint.rotation.eulerAngles.y,
            lookPoint.rotation.eulerAngles.z);
    }
}
