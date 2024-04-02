using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNpcToPoint : BTNode
{
    private List<Transform> points = new List<Transform>();
    private List<Vector3> destinations;
    private Transform npc;
    private NpcAI npcAI;
    int pointNum;
    Transform go;

    public MoveNpcToPoint(List<Transform> points,List<Vector3> destinations, int pointNum, Transform npc, Transform go, NpcAI npcAI)
    {
        this.points = points;
        this.npc = npc;
        this.pointNum = pointNum;
        this.go = go;
        this.destinations = destinations;
        this.npcAI = npcAI;
    }
    public override BTNodeState Evaluate()
    { 
        npcAI.MoveNpcAlongPath();
       float distance = Vector3.Distance(npc.position, points[pointNum].position);
       if (distance <= 4f)
       {
           Debug.Log("Got Close to position");
           pointNum++;
           return BTNodeState.FAILURE;
       } 
       return BTNodeState.SUCCESS;
    }
   
}
