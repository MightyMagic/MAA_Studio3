using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactble : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private float distThreshold;
    public GameObject go;
    [SerializeField] private InteractbleWindow canvas;
    [SerializeField] Canvas interactbleButton;
    float angle;
    //float interactingAngle = 20f;



    private void Update()
    {
        Interaction();
    }
  
    void Interaction()
    {

        float dist = Vector3.Distance(player.position, transform.position);
        


        Vector3 playerToObject = transform.position - player.position;
        playerToObject.Normalize();
        angle = Vector3.Angle(player.forward,playerToObject);

        if (dist <= distThreshold)
        {
            
            interactbleButton.gameObject.SetActive(true);
            Vector3 directionOfPlayer = player.position - interactbleButton.transform.position;
            Quaternion lookAwayRotation = Quaternion.LookRotation(-directionOfPlayer);
            interactbleButton.transform.rotation = lookAwayRotation;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if(go == null)
                {
                    go = transform.gameObject;
                    canvas.gameObject.SetActive(true);
                    canvas.OpenObjectInCanvas(gameObject);
                } 
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                go = null;
                canvas.DestroyCanvasGameObject();
                canvas.gameObject.SetActive(false);
            }
        }
        else
        {
            interactbleButton.gameObject.SetActive(false);

        }
    }
}
