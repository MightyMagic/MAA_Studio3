using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkToPlayerCoroutineManager : MonoBehaviour
{
    [SerializeField] private Transform npc;
    [SerializeField] private Transform player;
    [SerializeField] private Canvas npcCanvas;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private List<NpcDialougesSO> dialog;
    public int dialogIndex = 0;
    
    public bool isFinishedTalking;
    public bool isStartedTalking;
    public void StartTalkingCoroutine()
    {
        StartCoroutine(StartTalking());
    }
    IEnumerator StartTalking()
    {
        isStartedTalking = true;
        npcCanvas.gameObject.SetActive(true);
        text.text = dialog[dialogIndex].lineOne;
        yield return new WaitForSecondsRealtime(dialog[dialogIndex].timeToWaiteOne);
        text.text = dialog[dialogIndex].lineTwo;
        yield return new WaitForSecondsRealtime(dialog[dialogIndex].timeToWaiteTwo);
        text.text = dialog[dialogIndex].lineThree;
       yield return new WaitForSeconds(dialog[dialogIndex].timeToWaiteThree);
       
       npcCanvas.gameObject.SetActive(false);
       dialogIndex++;
       isFinishedTalking = true;
    }
}
