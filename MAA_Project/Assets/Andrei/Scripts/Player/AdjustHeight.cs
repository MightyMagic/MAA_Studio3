using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustHeight : MonoBehaviour
{
    private float initialYPos;

    private void Awake()
    {
        initialYPos = transform.position.y;
    }

    void Update()
    {
        if(Mathf.Abs(transform.position.y - initialYPos) > 0.2f)
        {
            transform.position = new Vector3(transform.position.x, initialYPos, transform.position.z);
        }
    }
}
