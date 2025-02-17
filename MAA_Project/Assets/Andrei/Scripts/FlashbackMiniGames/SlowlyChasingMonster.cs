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

    MonsterGroup monsterGroup;   

    Rigidbody rb;
    GameObject player;

    Vector3 initialPosition;

    bool isAsleep = true;

    [Header("Disappear")]
    [SerializeField] float distanceToAppear;
    bool appeared = true;

    void Start()
    {
        player = GameObject.Find("PlayerAhmed");
        rb = GetComponent<Rigidbody>();

        initialPosition = new Vector3(transform.position.x, 0f, transform.position.z);

        monsterGroup = GameObject.Find("Monsters").GetComponent<MonsterGroup>();

        //gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 playerVector = new Vector3((player.transform.position - transform.position).x, 0f, (player.transform.position - transform.position).z);


        if (!isAsleep)
        {
            MoveTowardsTarget();
            RotateTowardsTarget();
        }

        if (playerVector.magnitude < distanceToAwake && isAsleep & monsterGroup.chasingMonsters.Count < 1)
        {
            monsterGroup.chasingMonsters.Add(this);
            monsterGroup.PlaySoundOfTheAttackingMonster(this.transform);
            initialPosition = new Vector3(transform.position.x, 0f, transform.position.z);
            isAsleep = false;
        }

        Appear(playerVector.magnitude);
                 
    }

    public void AssignMaterial(Material mat)
    {
        foreach(Transform t in transform.GetChild(0))
        {
            if (t.GetComponent<Renderer>())
            {
                t.GetComponent<Renderer>().material = mat;
            }
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

            monsterGroup.StopMonsterSound();
            monsterGroup.chasingMonsters.Clear();
            //this.enabled = false;
        }
    }

    void Appear(float distanceFromPlayer)
    {
        if(distanceFromPlayer < distanceToAppear & !appeared)
        {
            appeared = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if(appeared & distanceFromPlayer > (distanceToAppear * 2f))
        {
            appeared = false; transform.GetChild(0).gameObject.SetActive(false);
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
