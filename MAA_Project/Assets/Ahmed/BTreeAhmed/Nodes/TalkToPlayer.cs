using UnityEngine;

namespace Ahmed.BTreeAhmed.Nodes
{
    public class TalkToPlayer : BTNode
    {
        private TalkToPlayerCoroutineManager _coroutineManager;
        private NpcAI npc;
        private Transform player;
    

        public TalkToPlayer(TalkToPlayerCoroutineManager manager, NpcAI npc, Transform player)
        {
            this._coroutineManager = manager;
            this.npc = npc;
            this.player = player;
        }
        public override BTNodeState Evaluate()
        {
            if (!_coroutineManager.isStartedTalking)
            {
                // Start the coroutine if not already started
                _coroutineManager.StartTalkingCoroutine();
                return BTNodeState.SUCCESS;
            }

            if (!_coroutineManager.isFinishedTalking)
            {
                Quaternion lookAt = Quaternion.LookRotation(player.position - npc.transform.position);
                npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, lookAt, 5f * Time.deltaTime);
                return BTNodeState.SUCCESS;
            }
            return BTNodeState.FAILURE;
        }
    }
}
