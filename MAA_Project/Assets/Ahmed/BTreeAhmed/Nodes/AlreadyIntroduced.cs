using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlreadyIntroduced : BTNode
{
    private RangeNode rangeNode;

    public AlreadyIntroduced(RangeNode rangeNode)
    {
        this.rangeNode = rangeNode;
    }
    public override BTNodeState Evaluate()
    {
        if (rangeNode.alreadyIntroduced)
        {
            return BTNodeState.FAILURE;
        }

        return BTNodeState.SUCCESS;
    }
}
