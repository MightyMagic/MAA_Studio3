using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHoleMovement : MonoBehaviour
{

    [SerializeField] float scalingSpeed;
    [SerializeField] float moveSpeed;

    [SerializeField] float maxSize;

    private Vector3 initialScale;

    GameObject player;
    

    void Start()
    {
        player = GameObject.Find("PlayerAhmed");

        initialScale = transform.localScale;
        initialScale.y = 0f;
        initialScale = initialScale.normalized; 
    }

    void Update()
    {
        Grow();
        MoveTowardsPlayer();
    }

    void Grow()
    {
        if(transform.localScale.x < maxSize)
        {
            transform.localScale += initialScale * (scalingSpeed * Time.deltaTime);
        }
    }

    void MoveTowardsPlayer()
    {
        transform.position += new Vector3((player.transform.position - transform.position).x, 0f, (player.transform.position - transform.position).z) * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("LoreScene3BlackHole");
        }
    }


}
