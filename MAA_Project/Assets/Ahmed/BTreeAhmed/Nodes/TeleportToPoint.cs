using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPoint : BTNode
{
    private List<Transform> Points = new List<Transform>();
    private Transform npc;
    int pointNum;
    Transform go;

    public TeleportToPoint(List<Transform> Points, int pointNum, Transform npc, Transform go)
    {
        this.Points = Points;
        this.npc = npc;
        this.pointNum = pointNum;
        this.go = go;
    }
    public override BTNodeState Evaluate()
    {
        npc.position = Points[pointNum].position + new Vector3(0, 1, 0);
        pointNum++;
        return BTNodeState.SUCCESS;
    }
}
