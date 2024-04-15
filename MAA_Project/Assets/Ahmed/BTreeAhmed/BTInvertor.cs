using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BTNode;

public class BTInvertor : BTNode
{
    protected BTNode bTNode;
    public BTInvertor(BTNode bTNode)
    {
        this.bTNode = bTNode;
    }

    public override BTNodeState Evaluate()
    {
        switch (bTNode.Evaluate())
        {
            case BTNodeState.RUNNING:
                _nodestate = BTNodeState.RUNNING;
                break;
            case BTNodeState.SUCCESS:
                _nodestate = BTNodeState.FAILURE;
                break;
            case BTNodeState.FAILURE:
            _nodestate = BTNodeState.SUCCESS;
                break;
            default:
                break;
        }
        return _nodestate;
    }
}
