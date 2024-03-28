using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimplePause : MonoBehaviour
{

    [SerializeField] Button backToMenuButton;
    bool gamePaused = false;
    void Start()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        backToMenuButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //BackToMenu();

            if(!gamePaused)
            {
                gamePaused = true;
                //Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                backToMenuButton.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                //Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                gamePaused = false;
                backToMenuButton.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }

        }
    }

    public void BackToMenu()
    {

        SceneManager.LoadScene(0);
    }
}
