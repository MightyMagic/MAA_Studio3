using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        MoveAlongPath();
        Vector3 direction = player.transform.position - this.transform.position;

        Quaternion lookAtobject = Quaternion.LookRotation(direction);
        this.transform.rotation =  Quaternion.Slerp(this.transform.rotation, lookAtobject, 5f * Time.deltaTime);
        topNode.Evaluate();
    }
    private void ConstructTree()
    {
        //EyesClosed eyesClosed = new EyesClosed(eyes);
        RangeNode range = new RangeNode(detectRange,transform,player.transform);
        WarnPlayer warnPlayer = new WarnPlayer();
        TeleportToPoint teleport = new TeleportToPoint(points, pointNum, this.transform, player);

        BTSequence teloportToNextPhrase = new BTSequence(new List<BTNode>() { teleport } );
        BTSequence IntroduceSequence = new BTSequence(new List<BTNode> { range, warnPlayer });
        topNode = new BTSelector(new List<BTNode>() { IntroduceSequence,teloportToNextPhrase });
    }
    private void BuildPathToPointNpc(Transform point)
    {
        if (path != null && grid != null)
        {
            path.FindPath(this.gameObject.transform.position, point.position);
            destinations.Clear();
            for (int i = 0; i < grid.path.Count; i++)
            {
                destinations.Add(grid.path[i].worldPosition);
            }
        }
        else
        {
            Debug.LogError("PathFinding object is not assigned.");
        }
    }
    private void MoveAlongPath()
    {
        if (destinations.Count > 0)
        {
            Vector3 currentTarget = (destinations[0] - transform.position).normalized * moveSpeed;
            transform.Translate(currentTarget);

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
            if (points.Count > 0)
            {
                BuildPathToPointNpc(points[0]);
                points.RemoveAt(0);
            }
            else
            {
                transform.Translate(Vector3.zero);
            }
        }
    }
}

