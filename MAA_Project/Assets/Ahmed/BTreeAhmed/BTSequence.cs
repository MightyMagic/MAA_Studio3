using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTNode
{
    protected List<BTNode> bTNodes = new List<BTNode>();
    public BTSequence(List<BTNode> bTNodes)
    {
        this.bTNodes = bTNodes;
    }

    public override BTNodeState Evaluate()
    {
        bool isAnyNodeRuning = false;
        foreach (BTNode node in bTNodes)
        {
            switch (node.Evaluate())
            {
                case BTNodeState.RUNNING:
                    isAnyNodeRuning = true; 
                    break;
                case BTNodeState.SUCCESS:
                    break;
                case BTNodeState.FAILURE:
                    return _nodestate;
                default:
                    break;
            }
        }
        if(isAnyNodeRuning)
        {
           _nodestate = BTNodeState.RUNNING;
        }
        else
        {
            _nodestate = BTNodeState.SUCCESS;
        }
        return _nodestate;
    }

}
