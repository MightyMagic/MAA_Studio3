using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITuttorialButton : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            gameObject.SetActive(false);
        }
    }
}
