using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallOffTiles : MonoBehaviour
{
    [SerializeField] float distanceToDie;
    [SerializeField] float fallVelocity;

    [SerializeField] int sceneIndex;

    float initialY;

    public bool startedFalling = false;

    void Start()
    {
        initialY = transform.position.y;
    }

    void Update()
    {
        if (startedFalling)
        {
           
            transform.position += Vector3.down * fallVelocity * Time.deltaTime;
            CheckForGameOver();
        }

    }

    void CheckForGameOver()
    {
        if(Mathf.Abs(initialY - transform.position.y) > distanceToDie)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }


}
