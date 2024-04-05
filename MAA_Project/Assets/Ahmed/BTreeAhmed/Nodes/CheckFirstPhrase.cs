namespace Ahmed.BTreeAhmed.Nodes
{
    public class CheckFirstPhrase :BTNode
    {
        private bool firstCaptured;
        public override BTNodeState Evaluate()  // Need to update this later with the capture script in the tutturial level
        {
            if (!firstCaptured)
            {
            
                return BTNodeState.SUCCESS;
            }

            return BTNodeState.FAILURE;
        }
    }
}
