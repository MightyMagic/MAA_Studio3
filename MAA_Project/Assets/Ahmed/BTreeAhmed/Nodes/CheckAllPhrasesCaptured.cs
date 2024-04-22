using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAllPhrasesCaptured : BTNode
{
    private NpcAI npc;

    public CheckAllPhrasesCaptured(NpcAI npc)
    {
        this.npc = npc;
    }
    public override BTNodeState Evaluate()
    {
        foreach (WordInSpace wordInSpace in npc._PuzzleManager.canvasTexts)
        {
            while(string.IsNullOrEmpty(wordInSpace.wordInSpaceText))
            {
                return BTNodeState.FAILURE;
            }
        }
        Debug.Log("all captured");
        return BTNodeState.SUCCESS;
    }
}
