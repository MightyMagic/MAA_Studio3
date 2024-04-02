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
    private bool isFinishedTalking;
    private bool isStartedTalking;
    public void StartTalkingCoroutine()
    {
        StartCoroutine(StartTalking());
    }
    IEnumerator StartTalking()
    {
        isStartedTalking = true;
        npcCanvas.gameObject.SetActive(true);
        text.text = "hey Pretty face what ya doing here";
        
        yield return new WaitForSecondsRealtime(5);
        text.text = "follow me";
       yield return new WaitForSeconds(2);
       npcCanvas.gameObject.SetActive(false);
       isFinishedTalking = true;
    }
    public bool IsFinishedTalking()
    {
        return isFinishedTalking;
    }

    public bool IsStartedTalking()
    {
        return isStartedTalking;
    }
}
