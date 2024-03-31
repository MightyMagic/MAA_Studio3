using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPoint : BTNode
{
    private List<Transform> points = new List<Transform>();
    private Transform npc;
    int pointNum;
    Transform go;

    public TeleportToPoint(List<Transform> points, int pointNum, Transform npc, Transform go)
    {
        this.points = points;
        this.npc = npc;
        this.pointNum = pointNum;
        this.go = go;
    }
    public override BTNodeState Evaluate()
    {
       float distance = Vector3.Distance(npc.position, points[pointNum].position);
       if (distance <= 4f)
       {
           Debug.Log("Got Close to position");
           pointNum++;
           return BTNodeState.SUCCESS;
       } 
       return BTNodeState.FAILURE;
    }
}
