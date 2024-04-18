using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustHeight : MonoBehaviour
{
    private float initialYPos;
    CharacterController controller;

    private void Awake()
    {
        if(GetComponent<CharacterController>())
            controller = GetComponent<CharacterController>();
        initialYPos = transform.position.y;
    }

    void Update()
    {
        if(Mathf.Abs(transform.position.y - initialYPos) > 0.2f)
        {
            controller.enabled = false;
            transform.position = new Vector3(transform.position.x, initialYPos, transform.position.z);
            controller.enabled = true;
        }
    }
}
