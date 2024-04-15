namespace Ahmed.BTreeAhmed.Nodes
{
    public class CheckSecondDialog : BTNode
    {
        private TalkToPlayerCoroutineManager dialogIndex;
        private bool startedSecondEncounter;
        public CheckSecondDialog(TalkToPlayerCoroutineManager dialogIndex)
        {
            this.dialogIndex = dialogIndex;
        }
        public override BTNodeState Evaluate()
        {
            if (!startedSecondEncounter)
            {
                dialogIndex.isStartedTalking = false;
                dialogIndex.isFinishedTalking = false;
                startedSecondEncounter = true;
            }
            if (dialogIndex.dialogIndex == 1)
            {
            
                return BTNodeState.SUCCESS;
            }

            return BTNodeState.FAILURE;
        }
    }
}
