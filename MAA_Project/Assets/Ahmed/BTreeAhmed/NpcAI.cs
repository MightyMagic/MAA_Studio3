using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcAI : MonoBehaviour
{
    [SerializeField] Eyes eyes;
    [SerializeField] float detectRange;
    [SerializeField] List<Transform> points = new List<Transform>();
    private int pointNum = 0;
    private BTNode topNode;

    [SerializeField] Transform player;
    private void Start()
    {
        ConstructTree();
    }
    private void Update()
    {
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
}

