using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BTNode
{
    public enum BTNodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    protected BTNodeState _nodestate;
    public BTNodeState NodeState { get { return _nodestate; } }
    public abstract BTNodeState Evaluate();
  
}
