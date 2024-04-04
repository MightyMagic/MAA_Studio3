using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthManager : MonoBehaviour
{

    [SerializeField] List<GameObject> tiles = new List<GameObject>();
    [SerializeField] float lightUpDuration;
    [SerializeField] float coolDown;

    bool lightUp = false;
    float timer = 0f;

    void Start()
    {
        DisableAllTiles();
        timer = coolDown / 2;
    }


    void Update()
    {
        if(!lightUp)
        {
            timer += Time.deltaTime;

            if(timer > coolDown)
            {
                timer = 0f;
                lightUp = true;
                StartCoroutine(EnableRenderersCoroutine(tiles, lightUpDuration));
            }
        }

    }

    void DisableAllTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i] != null)
            {

                Renderer renderer = tiles[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.enabled = false;

               
                }
            }
            else
            {
                Debug.LogError("Null object in objectsToEnable list");
            }
        }
    }

    IEnumerator EnableRenderersCoroutine(List<GameObject> objectsToEnable, float duration)
    {

        for (int i = 0; i < objectsToEnable.Count; i++)
        {
            if (objectsToEnable[i] != null)
            {

                Renderer renderer = objectsToEnable[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.enabled = true;

                    yield return new WaitForSeconds(duration);


                    //renderer.enabled = false;
                }
                else
                {
                   
                }
            }
            else
            {
                Debug.LogError("Null object in objectsToEnable list");
            }
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = objectsToEnable.Count - 1; i >= 0; i--)
        {
            if (objectsToEnable[i] != null)
            {

                Renderer renderer = objectsToEnable[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.enabled = false;

                    yield return new WaitForSeconds(duration);


                    //renderer.enabled = false;
                }
                else
                {

                }
            }
            else
            {
                Debug.LogError("Null object in objectsToEnable list");
            }
        }

        //DisableAllTiles();
        lightUp = false;
    }

}
