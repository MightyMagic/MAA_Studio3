using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleMonster : MonoBehaviour
{
    [SerializeField] PathFinding pathFinding;
    [SerializeField] Grid grid;

   
    [SerializeField] float moveSpeed;


    public List<GameObject> pointsToVisit;


    public List<Vector3> destinations = new List<Vector3>();
    GameObject wanderingTarget;

    public GameObject player;
    Rigidbody rb;

    MonsterDirector monsterDirector;

    public bool activelyChasing;

    void Start()
    {
        //grid.CreateGrid();

        rb = GetComponent<Rigidbody>();
        monsterDirector = GetComponent<MonsterDirector>();

        activelyChasing = false;

        //int i = Random.Range(0, wanderingPoints.Count);
        //wanderingTarget = wanderingPoints[i];

        //Alert();
        
       
    }

    void Update()
    {
        
        MoveAlongPath();
       

    }

    void MoveAlongPath()
    {
        if(destinations.Count > 0)
        {
            Vector3 currentTarget = (destinations[0] - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector3(currentTarget.x, 0f, currentTarget.z);

            if ((transform.position - destinations[0]).magnitude > 4f)
            {
            }
            else
            {
                if(destinations.Count > 0)
                {
                    destinations.RemoveAt(0);
                }
            }
        }
        else if (destinations.Count == 0)
        {
            if(pointsToVisit.Count > 0)
            {
                BuildPathToPoint(pointsToVisit[0]);
                pointsToVisit.RemoveAt(0);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void ClearStackOfPoints()
    {
        pointsToVisit.Clear();
        destinations.Clear();
    }

    public void WanderAround()
    {
        if((transform.position - wanderingTarget.transform.position).magnitude > 3f)
        {
            BuildPathToPoint(wanderingTarget);
        }
        else
        {
           ChooseWanderingPoint();
        }
    }
    
    public void ChooseWanderingPoint()
    {
        int i = Random.Range(0, pointsToVisit.Count);
        wanderingTarget = pointsToVisit[i];
    }

    //public void Alert()
    //{
    //    destinations.Clear();
    //    Waypoint target = monsterDirector.FindClosestWaypointToTarget(player.transform);
    //    BuildPathToPoint(target.waypoint.gameObject);
    //}

    public void BuildPathToPoint(GameObject point)
    {
        
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
            SceneManager.LoadScene("SimpleGameOver");
        }
    }
}
