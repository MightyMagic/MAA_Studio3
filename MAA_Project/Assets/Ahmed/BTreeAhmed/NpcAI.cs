using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    [SerializeField] PathFinding path;
    [SerializeField] Grid grid;
    [SerializeField] Eyes eyes;
    [SerializeField] float detectRange;
    [SerializeField] List<Transform> points = new List<Transform>();
    [SerializeField] List<Vector3> destinations = new List<Vector3>();
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody rb;
    private int pointNum = 0;
    private BTNode topNode;

    [SerializeField] Transform player;
    private void Start()
    {
        ConstructTree();
    }
    private void Update()
    {
        MoveNpcAlongPath();

        topNode.Evaluate();
    }
    private void ConstructTree()
    {
        //EyesClosed eyesClosed = new EyesClosed(eyes);
        RangeNode range = new RangeNode(detectRange,transform,player.transform);
        WarnPlayer warnPlayer = new WarnPlayer();
        TeleportToPoint teleport = new TeleportToPoint(points, pointNum, this.transform, player);

        BTSequence teloportToNextPhrase = new BTSequence(new List<BTNode>() { teleport } );
        BTSequence introduceSequence = new BTSequence(new List<BTNode> { range, warnPlayer });
        topNode = new BTSelector(new List<BTNode>() { introduceSequence, teloportToNextPhrase });
    }
    private void BuildPathToPointNpc(Transform point)
    {
        if (path != null && grid != null)
        {
            path.FindPath(this.gameObject.transform.position, point.position);
            destinations.Clear();
            for(int i = 0; i < grid.path.Count; i++)
            {
                destinations.Add(grid.path[i].worldPosition);
            }
        }
        else
        {
            Debug.LogError("PathFinding object is not assigned.");
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void MoveNpcAlongPath()
    {
        var dist = Vector3.Distance(this.transform.position, points[pointNum].position);
        BuildPathToPointNpc(points[pointNum]);
        if (destinations.Count > 0)
        {
                
            Vector3 currentTarget = (destinations[0] - transform.position).normalized * (moveSpeed * Time.deltaTime);
            transform.Translate(currentTarget);
           // Quaternion lookAtDest = Quaternion.LookRotation(currentTarget);
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDest, 1f * Time.deltaTime);
        }

        

    }
}

