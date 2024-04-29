using System.Collections.Generic;
using UnityEngine;

namespace Ahmed.BTreeAhmed.Nodes
{
    public class MoveNpcToPoint : BTNode
    {
        private List<Transform> points = new List<Transform>();
        private List<Vector3> destinations;
        private Transform npc;
        private NpcAI npcAI;
        public int pointNum;
        private bool addNextPoint;
        private Animator _animator;
        Transform go;

        public MoveNpcToPoint(List<Transform> points,List<Vector3> destinations, int pointNum, Transform npc, Transform go, NpcAI npcAI, Animator animator)
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
            float distance = Vector3.Distance(npc.position, points[npcAI.pointNum].position);
            if (distance <= 4f)
            {
                npcAI._animator.SetBool("Walking",false);
                return BTNodeState.FAILURE;
            }
            return BTNodeState.SUCCESS;
        }
   
    }
}
