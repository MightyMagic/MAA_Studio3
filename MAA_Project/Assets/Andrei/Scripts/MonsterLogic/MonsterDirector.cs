using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterDirector : MonoBehaviour
{
    public float distanceToPlayer;
    [Header("Monster script")]
    SimpleMonster monsterScript;

    [Header("References")]
    [SerializeField] List<ChunkWaypoints> chunkWaypoints;
    [SerializeField] Eyes eyeScript;

    [Header("Attack radius")]
    [SerializeField] float bigRadius;
    [SerializeField] float mediumRadius;
    [SerializeField] float smallRadius;

    [Header("Debug")]
    public List<Waypoint> patrolWaypoints;
    public List<Waypoint> allWaypoints;

    int currentIndex;
    int currentChunkIndex;

    public bool chasing = false;
    bool playerIsDead = false;

    GameObject deflectionPoint;
    

    void Awake()
    {

        monsterScript = GetComponent<SimpleMonster>();

    }

    void Start()
    {
        chasing = false;
        //InvestigateChunk(1);
        FetchAllPatrolWaypoints();  
        PatrolAround(FindClosestWaypointToTarget(transform));


    }

    private void Update()
    {

        Vector3 vectorToPlayer = monsterScript.player.transform.position - transform.position;
        vectorToPlayer = new Vector3(vectorToPlayer.x, 0f, vectorToPlayer.z);

        distanceToPlayer = vectorToPlayer.magnitude;

        if (monsterScript.pointsToVisit.Count == 0 && monsterScript.destinations.Count == 0 && !chasing)
        { 
            PatrolAround(FindClosestWaypointToTarget(transform));
        }
        
       // if(investigateRoom && !chasing)
       // {
       //     Debug.LogError("Start investigating");
       //     InvestigateChunk(FindClosestWaypointToTarget(monsterScript.player.transform));
       //     Investigate(false);
       // }


        if(distanceToPlayer < mediumRadius && distanceToPlayer > smallRadius)
        {

            if(!chasing)
            {
                Debug.LogError("Started chasing");
                monsterScript.ClearStackOfPoints();
                chasing = true;

                deflectionPoint = FetchDeflectionPoint(monsterScript.player.transform);
            }
            else
            {
                if (!eyeScript.eyesClosed)
                {
                    // Chase directly
                }
                else if (eyeScript.eyesClosed && monsterScript.destinations.Count == 0)
                {
                    // Investigate the area
                }
            }
        }

        // Insta kill with no escape
        if(distanceToPlayer < smallRadius && !playerIsDead)
        {
            playerIsDead = true;

            monsterScript.ClearStackOfPoints();
            monsterScript.pointsToVisit.Add(monsterScript.player);
            //Chase(true);
            monsterScript.moveSpeed *= 2f;
            
            monsterScript.rb.velocity = vectorToPlayer.normalized * monsterScript.moveSpeed;
        }

        // Monster lost you, so goes back to patrolling
        if(distanceToPlayer > bigRadius && chasing)
        {
            chasing = false;
            Debug.LogError("Stopped chasing");
            PatrolAround(FindClosestWaypointToTarget(transform));
        }
    }

    public void Investigate()
    {
        InvestigateChunk(FindClosestWaypointToTarget(monsterScript.player.transform));
    }

    void FetchAllPatrolWaypoints()
    {
        patrolWaypoints.Clear();

        for(int i = 0; i < chunkWaypoints.Count; i++)
        {
            List<Waypoint> patrol = new List<Waypoint>();

            for(int j = 0; j < chunkWaypoints[i].waypoints.Count; j++)
            {
                if (chunkWaypoints[i].waypoints[j].usedInPatrol)
                    patrolWaypoints.Add(chunkWaypoints[i].waypoints[j]);
            }

            patrolWaypoints.AddRange(patrol);
            patrol.Clear();

            allWaypoints.AddRange(chunkWaypoints[i].waypoints);
        }
    }

    public int FindClosestWaypointToTarget(Transform target)
    {
        if(patrolWaypoints != null)
        {
            float closestDistance = Mathf.Infinity;
            int closestIndex = 0;

            for (int i = 0; i < patrolWaypoints.Count; i++)
            {
                if (patrolWaypoints[i] != null)
                {
                    float distance = Vector3.Distance(target.position, patrolWaypoints[i].waypoint.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestIndex = i;
                    }
                }
            }

            currentIndex = closestIndex;
            //Debug.LogError("Index number is " + closestIndex);
            return patrolWaypoints[closestIndex].chunkNumber;
        }
        else
        {
            return 0;
        }       
    }

    public GameObject FetchDeflectionPoint(Transform target)
    {
        List<GameObject> chunkList = new List<GameObject>();
        int chunkIndex = FindClosestWaypointToTarget(target);

        for (int i = 0; i < allWaypoints.Count; i++)
        {
            if (allWaypoints[i] != null && allWaypoints[i].chunkNumber == chunkIndex)
                return(allWaypoints[i].waypoint.gameObject);
        }

        return(null);
    }

    public void InvestigateChunk(int chunkIndex)
    {
        Debug.LogError("Start investigating");

        List<GameObject> chunkList = new List<GameObject>();

       for(int i = 0; i < allWaypoints.Count; i++)
       {
            if (allWaypoints[i] != null && allWaypoints[i].chunkNumber == chunkIndex)
                chunkList.Add(allWaypoints[i].waypoint.gameObject);
       }

        monsterScript.ClearStackOfPoints();
        monsterScript.pointsToVisit.AddRange(chunkList);
    }

   

    // Returns all of the patrol paoints of the corresponding chunk and clears the current buffer
    public void PatrolAround(int targetIndex)
    {
        // Find the closest patrol point 
        //int targetIndex = FindClosestWaypointToTarget(this.transform);

        List<GameObject> chunkList = new List<GameObject>();

        for(int j = 0; j < patrolWaypoints.Count; j++)
        {
            for(int i = 0; i < patrolWaypoints.Count; i++)
            {
                if (patrolWaypoints[i] != null && patrolWaypoints[i].chunkNumber == targetIndex)
                    chunkList.Add(patrolWaypoints[i].waypoint.gameObject);
            }

            targetIndex = (targetIndex + 1) % patrolWaypoints.Count;
        }

        monsterScript.ClearStackOfPoints();
        monsterScript.pointsToVisit.AddRange(chunkList);
    }

   private void OnDrawGizmos()
   {
        Color gColor;

        if(patrolWaypoints.Count > 0)
        {
            for (int i = 0; i < patrolWaypoints.Count - 1; i++)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(patrolWaypoints[i].waypoint.position, 1);
                Gizmos.DrawLine(patrolWaypoints[i].waypoint.position, patrolWaypoints[i + 1].waypoint.position);
            }
        }

        gColor = Color.red;
        gColor.a = 0.5f;
       
        Gizmos.color = gColor;
        Gizmos.DrawSphere(transform.position, smallRadius);

        gColor = Color.green;
        gColor.a = 0.5f;

        Gizmos.color = gColor;
        Gizmos.DrawSphere(transform.position, bigRadius);

        gColor = Color.blue;
        gColor.a = 0.5f;

        Gizmos.color = gColor;
        Gizmos.DrawSphere(transform.position, mediumRadius);
    }
}
