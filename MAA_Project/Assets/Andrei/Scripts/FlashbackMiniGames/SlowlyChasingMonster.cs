using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowlyChasingMonster : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float distanceToStop;
    [SerializeField] float distanceToAwake;

    Rigidbody rb;
    GameObject player;

    Vector3 initialPosition;

    bool isAsleep = true;

    void Start()
    {
        player = GameObject.Find("PlayerAhmed");
        rb = GetComponent<Rigidbody>();

        initialPosition = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    void Update()
    {
        Vector3 playerVector = new Vector3((player.transform.position - transform.position).x, 0f, (player.transform.position - transform.position).z);


        if (!isAsleep)
        {
            MoveTowardsTarget();
            RotateTowardsTarget();
        }

        if (playerVector.magnitude < distanceToAwake && isAsleep)
        {
            initialPosition = new Vector3(transform.position.x, 0f, transform.position.z);
            isAsleep = false;
        }
                 
    }

    void RotateTowardsTarget()
    {
        
       Vector3 targetDirection = player.transform.position - transform.position;
       targetDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);

       Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

       transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
    }

    void MoveTowardsTarget()
    {
        Vector3 currentPos = new Vector3(transform.position.x, 0f, transform.position.z);

        if((initialPosition - currentPos).magnitude < distanceToStop)
        {
            Vector3 currentTarget = (player.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector3(currentTarget.x, 0f, currentTarget.z);
        }
        else
        {
            rb.velocity = Vector3.zero;
            isAsleep = true;
            //this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene("LoreScene1");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
