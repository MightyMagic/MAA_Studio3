using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BTNode
{
    public bool idleing;
    public override BTNodeState Evaluate()
    {
        if (!idleing)
        {
            return BTNodeState.SUCCESS;
        }
        return BTNodeState.FAILURE;
    }
}
