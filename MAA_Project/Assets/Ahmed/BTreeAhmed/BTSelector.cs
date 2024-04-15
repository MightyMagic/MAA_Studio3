using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTNode
{
    protected List<BTNode> bTNodes = new List<BTNode>();
    public BTSelector(List<BTNode> bTNodes)
    {
        this.bTNodes = bTNodes;
    }

    public override BTNodeState Evaluate()
    {
        foreach (BTNode node in bTNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeState.RUNNING:
                    _nodestate = BTNodeState.RUNNING;
                    return _nodestate;
                case BTNodeState.SUCCESS:
                    _nodestate = BTNodeState.SUCCESS;
                    return _nodestate;
                case BTNodeState.FAILURE:
                    _nodestate = BTNodeState.FAILURE;
                    break;
                default:
                    break;
            }
        }
        _nodestate = BTNodeState.FAILURE;
        return _nodestate;
    }
}
