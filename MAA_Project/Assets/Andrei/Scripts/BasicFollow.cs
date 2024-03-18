using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector3 offSet;
    void Start()
    {
        offSet = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offSet + player.position;
    }
}
