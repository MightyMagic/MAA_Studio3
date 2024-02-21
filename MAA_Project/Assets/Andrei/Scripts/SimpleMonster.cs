using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMonster : MonoBehaviour
{
    [SerializeField] PathfindingAndrei pathFinding;
    [SerializeField] GridAndrei grid;

    [SerializeField] List<GameObject> wanderingPoints;
    [SerializeField] float moveSpeed;

    List<Vector3> destinations = new List<Vector3>();
    GameObject wanderingTarget;

    GameObject player;
    Rigidbody rb;



    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

        int i = Random.Range(0, wanderingPoints.Count);
        wanderingTarget = wanderingPoints[i];
    }

    void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if(destinations.Count >= 1)
        {
            Vector3 currentTarget = (destinations[0] - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector3(currentTarget.x, 0f, currentTarget.z);

            if ((transform.position - destinations[0]).magnitude > 2.5f)
            {
            }
            else
            {
                if(destinations.Count >= 1)
                {
                    destinations.RemoveAt(0);
                }
            }
        }
    }

    public void WanderAround()
    {
        if((transform.position - wanderingTarget.transform.position).magnitude > 2.5f)
        {
            BuildPathToPoint(wanderingTarget);
        }
        else
        {
            int i = Random.Range(0, wanderingPoints.Count);
            wanderingTarget = wanderingPoints[i];
        }
    }

    public void ChasePlayer()
    {
        BuildPathToPoint(player);
    }

    public void BuildPathToPoint(GameObject point)
    {
        grid.CreateGrid();
        pathFinding.FindPath(this.gameObject.transform.position, point.transform.position);
        destinations.Clear();
        for (int i = 0; i < grid.path.Count; i++)
        {
            destinations.Add(grid.path[i].worldPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            print("Skill issue");
            Debug.LogError("Eaten by monster");
        }
    }
}
