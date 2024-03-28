using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprint : MonoBehaviour
{
    [SerializeField] float sprintMultiplier;
    [SerializeField] float sprintTimer;
    [SerializeField] float sprintToRestore;

    [SerializeField] Slider sprintSlider;

    float sprintTimerToRestore;
    public float sprintValue;
    public bool sprintEnabled = false;

    Eyes eyeScript;
    PlayerMovmentAhmed playerMovment;
    float playerInitialSpeed;

    private void Awake()
    {
        
    }

    void Start()
    {

        eyeScript = GetComponent<Eyes>();
        playerMovment = GetComponent<PlayerMovmentAhmed>();

        playerInitialSpeed = playerMovment.playerSpeed;
        if (PlayerPrefs.HasKey("PlayerSpeed"))
        {
            playerInitialSpeed = PlayerPrefs.GetFloat("PlayerSpeed");
        }

        if (PlayerPrefs.HasKey("SprintMultiplier"))
        {
            sprintMultiplier = PlayerPrefs.GetFloat("SprintMultiplier");
        }

        if (PlayerPrefs.HasKey("SprintTimer"))
        {
            sprintTimer = PlayerPrefs.GetFloat("SprintTimer");
        }

        if (PlayerPrefs.HasKey("SprintToRestore"))
        {
            sprintToRestore = PlayerPrefs.GetFloat("SprintToRestore");
        }

        sprintSlider.maxValue = sprintTimer;
        sprintSlider.minValue = 0f;
        sprintSlider.value = sprintTimer;

        sprintValue = sprintTimer;



        //sprintSlider.gameObject.SetActive(false);
    }

   
    void Update()
    {
        

        if (sprintEnabled)
        {
            

            if(Input.GetKey(KeyCode.LeftShift) && !eyeScript.eyesClosed && sprintValue > 0f)
            {
                sprintValue -= Time.deltaTime;
                sprintSlider.gameObject.SetActive(true);
                playerMovment.playerSpeed = playerInitialSpeed * sprintMultiplier;
            }
            else if(!eyeScript.eyesClosed && Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerMovment.playerSpeed = playerInitialSpeed;
                sprintEnabled = false;
            }

            if (sprintValue <= 0f)
            {
                sprintEnabled = false;
                //sprintValue = sprintTimer;
                playerMovment.playerSpeed = playerInitialSpeed;
                //sprintSlider.gameObject.SetActive(false);
            }
        }
        else
        {
            //if(sprintValue < sprintTimer)
            //{
            //    sprintValue = sprintValue + (Time.deltaTime * sprintToRestore);
            //}

            sprintValue = sprintValue + (Time.deltaTime * sprintToRestore);

            if (!eyeScript.eyesClosed && Input.GetKeyDown(KeyCode.LeftShift) && playerMovment != null  && sprintValue > 0f)
            {
                sprintEnabled = true;
                //sprintValue = sprintTimer;
            }
        }

        sprintValue = Mathf.Clamp(sprintValue, -1f, sprintTimer);
        sprintSlider.value = sprintValue;


    }
}
