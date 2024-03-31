using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : BTNode
{
    private float range;
    private Transform target;
    private Transform player;

    public RangeNode(float range, Transform target, Transform player)
    {
        this.range = range;
        this.target = target;
        this.player = player;
    }
    public override BTNodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, player.position);
        if(distance <= range)
        {
            return BTNodeState.SUCCESS;
        }
        else
        {
           // Debug.Log("not in range");
            return BTNodeState.FAILURE;
        }
    }

}
