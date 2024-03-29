using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactble : MonoBehaviour
{
    //public GameObject gameObj;
    [SerializeField] Transform player;
    [SerializeField] private float distThreshold;
    [SerializeField] private Canvas canvas;



    private void Update()
    {
        Interaction();
    }

    void Interaction()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (dist <= distThreshold)
        {
            // make pointer appaer
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Open interactble window
                canvas.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }
}
