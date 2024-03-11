using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterDirector : MonoBehaviour
{
    [Header("Monster script")]
    SimpleMonster monsterScript;

    [Header("References")]
    [SerializeField] List<ChunkWaypoints> chunkWaypoints;

    [Header("Debug")]
    public List<Waypoint> patrolWaypoints;
    public List<Waypoint> allWaypoints;

    int currentIndex;
    int currentChunkIndex;

    bool globalPatrol;
    bool investigateRoom;
    bool chasing;

    void Awake()
    {
        FetchAllPatrolWaypoints();
    }

    void Start()
    {
        monsterScript = GetComponent<SimpleMonster>();
        //InvestigateChunk(1);

        PatrolAround(FindClosestWaypointToTarget(transform));
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
            Debug.LogError("Index number is " + closestIndex);
            return patrolWaypoints[closestIndex].chunkNumber;
        }
        else
        {
            return patrolWaypoints[currentIndex].chunkNumber;
        }       
    }

    public void InvestigateChunk(int chunkIndex)
    {
        List<GameObject> chunkList = new List<GameObject>();

       for(int i = 0; i < allWaypoints.Count; i++)
       {
            if (allWaypoints[i] != null && allWaypoints[i].chunkNumber == chunkIndex)
                chunkList.Add(allWaypoints[i].waypoint.gameObject);
       }

        monsterScript.pointsToVisit.Clear();
        monsterScript.pointsToVisit.AddRange(chunkList);
    }

    public Waypoint RetrieveNextWaypoint()
    {
        int dir = Random.Range(0, 2);
        if(dir == 1)
        {
            currentIndex = (currentIndex + 1) % patrolWaypoints.Count;
            return patrolWaypoints[currentIndex];
        }
        else
        {
            currentIndex = (currentIndex - 1 + patrolWaypoints.Count) % patrolWaypoints.Count;
            return patrolWaypoints[currentIndex];
        }
    }

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

        

        monsterScript.pointsToVisit.Clear();
        monsterScript.pointsToVisit.AddRange(chunkList);
        Debug.LogError("Patrol list is " + chunkList.Count);
        Debug.LogError("Points to visit is " + monsterScript.pointsToVisit.Count);


    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < patrolWaypoints.Count - 1; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(patrolWaypoints[i].waypoint.position, 1);
            Gizmos.DrawLine(patrolWaypoints[i].waypoint.position, patrolWaypoints[i + 1].waypoint.position);
        }

        //for (int i = 0; i < chunkWaypoints.Count - 1; i++)
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawSphere(waypoints[i].waypoint.position, 1);
        //    Gizmos.DrawLine(waypoints[i].waypoint.position, waypoints[i + 1].waypoint.position);
        //}
    }
}
