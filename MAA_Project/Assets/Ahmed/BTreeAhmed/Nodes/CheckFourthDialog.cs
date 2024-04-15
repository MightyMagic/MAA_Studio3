using UnityEngine;

namespace Ahmed.BTreeAhmed.Nodes
{
    public class CheckFourthDialog : BTNode
    {
        private TalkToPlayerCoroutineManager dialogIndex;
        private bool startedFourthEncounter;
        private NpcAI moveNpc;
        public CheckFourthDialog(TalkToPlayerCoroutineManager dialogIndex, NpcAI moveNpc)
        {
            this.dialogIndex = dialogIndex;
            this.moveNpc = moveNpc;
        }
        public override BTNodeState Evaluate()
        {
            if (!startedFourthEncounter) // find a way to run this after the second encounter 
            {
                dialogIndex.isStartedTalking = false;
                dialogIndex.isFinishedTalking = false;
                startedFourthEncounter = true;
            }
            if (dialogIndex.dialogIndex == 3)
            {
                return BTNodeState.SUCCESS;
            }

            return BTNodeState.FAILURE;
        }
    }
}
