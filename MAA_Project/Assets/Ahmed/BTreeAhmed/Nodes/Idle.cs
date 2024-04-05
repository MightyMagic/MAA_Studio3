namespace Ahmed.BTreeAhmed.Nodes
{
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
}
