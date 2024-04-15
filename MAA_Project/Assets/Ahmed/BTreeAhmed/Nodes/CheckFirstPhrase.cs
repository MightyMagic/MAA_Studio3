namespace Ahmed.BTreeAhmed.Nodes
{
    public class CheckFirstPhrase :BTNode
    {
        private NpcAI _npcAI;
        public CheckFirstPhrase(NpcAI npc)
        {
            this._npcAI = npc;
        }

        private bool captured;
        public override BTNodeState Evaluate()  // Need to update this later with the capture script in the tutturial level
        {
            if (_npcAI._puzzleCatcher.puzzleWords[0].captured && !captured)
            {
            
                captured = true;
                return BTNodeState.SUCCESS;
            }

            return BTNodeState.FAILURE;
        }
    }
}
