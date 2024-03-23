using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    [SerializeField] Eyes eyes;
    [SerializeField] float detectRange;
    [SerializeField] Grid grid;
    [SerializeField] PathFinding path;
    [SerializeField] List<Transform> points = new List<Transform>();
    private BTNode topNode;

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
        RangeNode range = new RangeNode(detectRange,transform,player.transform);
        WarnPlayer warnPlayer = new WarnPlayer();

        BTSequence IntroduceSequence = new BTSequence(new List<BTNode> { range, warnPlayer });
        topNode = new BTSelector(new List<BTNode>() { IntroduceSequence });
    }
    private void MoveToPoint(Vector3 point)
    {
        
    }
}

