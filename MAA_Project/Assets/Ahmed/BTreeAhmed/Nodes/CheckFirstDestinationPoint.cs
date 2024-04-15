namespace Ahmed.BTreeAhmed.Nodes
{
   public class CheckFirstDestinationPoint : BTNode
   {
      private NpcAI npcAI;

      public CheckFirstDestinationPoint(NpcAI npcAI)
      {
         this.npcAI = npcAI;
      }
      public override BTNodeState Evaluate()
      {
         if (npcAI.pointNum == 0)
         {
            return BTNodeState.SUCCESS;
         }

         return BTNodeState.FAILURE;
      }
   }
}
