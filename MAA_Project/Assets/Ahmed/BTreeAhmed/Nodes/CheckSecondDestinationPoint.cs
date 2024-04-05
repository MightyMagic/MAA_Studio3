namespace Ahmed.BTreeAhmed.Nodes
{
    public class CheckSecondDestinationPoint : BTNode
    {
        private NpcAI npcAI;

        public CheckSecondDestinationPoint(NpcAI npcAI)
        {
            this.npcAI = npcAI;
        }
        public override BTNodeState Evaluate()
        {
            if (npcAI.pointNum == 1)
            {
                return BTNodeState.SUCCESS;
            }

            return BTNodeState.FAILURE;
        }
    }
}
