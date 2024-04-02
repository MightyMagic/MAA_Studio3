using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TalkToPlayerCoroutineManager _manager;
    [SerializeField] PathFinding path;
    [SerializeField] Grid grid;
    [SerializeField] float detectRange;
    [SerializeField] List<Transform> points = new List<Transform>();
    [SerializeField] List<Vector3> destinations = new List<Vector3>();
    [SerializeField] float moveSpeed;
    private int pointNum = 0;
    private BTNode topNode;
    public bool alreadyIntroduced;

    [SerializeField] Transform player;
    private void Start()
    {
        
        ConstructTree();
    }
    private void Update()
    {
        
        topNode.Evaluate();
    }
    private void ConstructTree()
    {
        //EyesClosed eyesClosed = new EyesClosed(eyes);
        //CheckIfDone checkIfDone = new CheckIfDone();
        Idle idle = new Idle();
        RangeNode range = new RangeNode(detectRange,transform,player.transform,alreadyIntroduced);
        TalkToPlayer talk = new TalkToPlayer(_manager, this,player);
        MoveNpcToPoint moveNpc = new MoveNpcToPoint(points,destinations, pointNum, this.transform, player ,this);
        AlreadyIntroduced checkInroduce = new AlreadyIntroduced(range);
        
        // Assemble tree
        BTSequence moveNpcToPoint = new BTSequence(new List<BTNode> { moveNpc });
        BTInvertor notInRange = new BTInvertor(range);
        BTSelector talkAndMove = new BTSelector(new List<BTNode>{talk,moveNpcToPoint});
        BTSequence introduce = new BTSequence(new List<BTNode> { checkInroduce, notInRange });
        //BTSequence introduceSequence = new BTSequence(new List<BTNode> {talkToPlayer, moveNpcToPoint });
        topNode = new BTSelector(new List<BTNode> {introduce, talkAndMove});
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
    public void MoveNpcAlongPath()
    {
        var dist = Vector3.Distance(this.transform.position, points[pointNum].position);
        BuildPathToPointNpc(points[pointNum]);
        if (destinations.Count > 0)
        {
            Vector3 currentTarget = (destinations[0] - transform.position).normalized;
            rb.velocity = currentTarget * moveSpeed;
            Quaternion lookAtDest = Quaternion.LookRotation(currentTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDest, 5f * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}

