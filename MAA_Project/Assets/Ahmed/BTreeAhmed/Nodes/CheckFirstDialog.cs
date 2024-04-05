namespace Ahmed.BTreeAhmed.Nodes
{
    public class CheckFirstDialog :BTNode
    {
        private TalkToPlayerCoroutineManager dialogIndex;
        public CheckFirstDialog(TalkToPlayerCoroutineManager dialogIndex)
        {
            this.dialogIndex = dialogIndex;
        }
        public override BTNodeState Evaluate()
        {
            if (dialogIndex.dialogIndex == 0)
            {
                return BTNodeState.SUCCESS;
            }

            return BTNodeState.FAILURE;
        }
    }
}
