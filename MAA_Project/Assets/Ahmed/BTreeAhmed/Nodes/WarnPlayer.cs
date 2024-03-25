using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarnPlayer : BTNode
{
    public override BTNodeState Evaluate()
    {
        Debug.Log("Dont close your eyes or you will be lost");
        return BTNodeState.FAILURE;
    }
}
