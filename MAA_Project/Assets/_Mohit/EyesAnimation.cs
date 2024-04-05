using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EyesAnimation : MonoBehaviour
{
    public Animator animator; 
    public Animator animator1;
    public GameObject ahh;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            animator.Play("eyelid up");
            animator1.Play("eyelid down");
            ahh.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.Play("eyelid up R");
            animator1.Play("eyelid down R");


            if (animator.GetCurrentAnimatorStateInfo(0).IsName("eyelid down R"))
            {
                ahh.SetActive(true);
                Debug.Log("bruh");
            }
            
        }
    }
}
