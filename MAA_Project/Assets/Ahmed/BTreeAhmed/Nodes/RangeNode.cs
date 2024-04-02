using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : BTNode
{
    private float range;
    private Transform npc;
    private Transform player;
    public bool alreadyIntroduced;

    public RangeNode(float range, Transform npc, Transform player,bool alreadyIntroduced)
    {
        this.range = range;
        this.npc = npc;
        this.player = player;
        this.alreadyIntroduced = alreadyIntroduced;

    }
    public override BTNodeState Evaluate()
    {
        float distance = Vector3.Distance(npc.position, player.position);
        if(distance <= range)
        {
            alreadyIntroduced = true;
            return BTNodeState.SUCCESS;
        }
        return BTNodeState.FAILURE;
    }

}
