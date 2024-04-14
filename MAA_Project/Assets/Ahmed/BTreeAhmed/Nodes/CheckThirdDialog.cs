using UnityEngine;

namespace Ahmed.BTreeAhmed.Nodes
{
    public class CheckThirdDialog : BTNode
    {
        private TalkToPlayerCoroutineManager dialogIndex;
        private bool startedThirdEncounter;
        private NpcAI moveNpc;
        public CheckThirdDialog(TalkToPlayerCoroutineManager dialogIndex, NpcAI moveNpc)
        {
            this.dialogIndex = dialogIndex;
            this.moveNpc = moveNpc;
        }
        public override BTNodeState Evaluate()
        {
            if (!startedThirdEncounter) // find a way to run this after the second encounter 
            {
                dialogIndex.isStartedTalking = false;
                dialogIndex.isFinishedTalking = false;
                startedThirdEncounter = true;
                //moveNpc.pointNum++;
            }
            if (dialogIndex.dialogIndex == 2)
            {
                
                return BTNodeState.SUCCESS;
            }

            return BTNodeState.FAILURE;
        }
    }
}
