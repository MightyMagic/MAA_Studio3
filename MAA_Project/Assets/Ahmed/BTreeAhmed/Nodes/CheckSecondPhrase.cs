using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSecondPhrase : BTNode
{
    private bool secondCaptured;
    public override BTNodeState Evaluate()  // Need to update this later with the capture script in the tutturial level
    {
        if (!secondCaptured)
        {
            
            return BTNodeState.SUCCESS;
        }

        return BTNodeState.FAILURE;
    }
}
