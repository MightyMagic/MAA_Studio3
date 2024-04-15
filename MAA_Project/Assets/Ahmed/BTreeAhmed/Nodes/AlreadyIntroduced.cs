namespace Ahmed.BTreeAhmed.Nodes
{
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
}
