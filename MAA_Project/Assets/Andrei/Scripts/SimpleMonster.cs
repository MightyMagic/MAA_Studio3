using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class SimpleMonster : MonoBehaviour
{
    [SerializeField] PathFinding pathFinding;
    [SerializeField] GridX grid;

   
    public float moveSpeed;


    public List<GameObject> pointsToVisit;


    public List<Vector3> destinations = new List<Vector3>();
    GameObject wanderingTarget;

    public GameObject player;
    public Rigidbody rb;

    MonsterDirector monsterDirector;

    public bool activelyChasing;

    public bool gridIsBuilt = true;

    void Start()
    {
        //grid.CreateGrid();

        if (PlayerPrefs.HasKey("MonsterSpeed"))
        {
            moveSpeed = PlayerPrefs.GetFloat("MonsterSpeed");
        }

        rb = GetComponent<Rigidbody>();
        monsterDirector = GetComponent<MonsterDirector>();

        activelyChasing = false;  
    }

    void Update()
    {  
        MoveAlongPath();
        RotateTowardsTarget();
    }

    void RotateTowardsTarget()
    {
        if (destinations.Count > 0)
        {
            Vector3 targetDirection = destinations[0] - transform.position;
            targetDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }
    }

    void MoveAlongPath()
    {
        if (destinations.Count > 0)
        {
            Vector3 currentTarget = (destinations[0] - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector3(currentTarget.x, 0f, currentTarget.z);

            if ((transform.position - destinations[0]).magnitude > 4f)
            {
            }
            else
            {
                if (destinations.Count > 0)
                {
                    destinations.RemoveAt(0);
                }
            }
        }
        else if (destinations.Count == 0)
        {
            if (pointsToVisit.Count > 0)
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

   // public void WanderAround()
   // {
   //     if((transform.position - wanderingTarget.transform.position).magnitude > 3f)
   //     {
   //         BuildPathToPoint(wanderingTarget);
   //     }
   //     else
   //     {
   //        ChooseWanderingPoint();
   //     }
   // }
   // 
   // public void ChooseWanderingPoint()
   // {
   //     int i = Random.Range(0, pointsToVisit.Count);
   //     wanderingTarget = pointsToVisit[i];
   // }

    //public void Alert()
    //{
    //    destinations.Clear();
    //    Waypoint target = monsterDirector.FindClosestWaypointToTarget(player.transform);
    //    BuildPathToPoint(target.waypoint.gameObject);
    //}

    public void BuildPathToPoint(GameObject point)
    {
        if(grid != null)
        {
            pathFinding.FindPath(this.gameObject.transform.position, point.transform.position);
            destinations.Clear();
            for (int i = 0; i < grid.path.Count; i++)
            {
                destinations.Add(grid.path[i].worldPosition);
            }
        }      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("Skill issue");
            Debug.LogError("Eaten by monster");
            PlayerPrefs.SetInt("PlayerSpawn", 1);
            SceneManager.LoadScene("LevelOneArea");
        }
    }

    private void OnDrawGizmos()
    {
        if(destinations.Count > 0)
        {
            for (int i = 0; i < destinations.Count - 1; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawLine(destinations[i], destinations[i + 1]);

                Color gColor;
                gColor = Color.black;
                gColor.a = 0.5f;

                Gizmos.color = gColor;
                Gizmos.DrawSphere(destinations[i], 2f);
            }

            Gizmos.DrawSphere(destinations[destinations.Count - 1], 2f);
        }
    }
}
