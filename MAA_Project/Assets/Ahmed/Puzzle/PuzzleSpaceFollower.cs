using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpaceFollower : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
