using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesClosed : BTNode
{
    // private Eyes eyes;
    private Eyes eyes;

    public EyesClosed(Eyes eyes)
    {
        this.eyes = eyes;
    }

    public override BTNodeState Evaluate()
    {
       if (eyes.eyesClosed)
       {
           return BTNodeState.SUCCESS;
       }
       else
       {
           return BTNodeState.FAILURE;
       
       }
    }
}
