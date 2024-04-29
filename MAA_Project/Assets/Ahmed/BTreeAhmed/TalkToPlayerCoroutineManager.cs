using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TalkToPlayerCoroutineManager : MonoBehaviour
{
    [SerializeField] private NpcAI npc;
    [SerializeField] private Transform player;
    [SerializeField] private Canvas npcCanvas;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private List<NpcDialougesSO> dialog;
    [SerializeField] private AudioSource audio;
    [SerializeField] private Material dissolveMaterial;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem[] _particles;
    public int dialogIndex = 0;
    
    public bool isFinishedTalking;
    public bool isStartedTalking;

    private void Start()
    {
        dissolveMaterial.SetFloat("_DissolveAmount", 0 );
        dissolveMaterial.SetFloat("_GlowRange",0);
        dissolveMaterial.SetFloat("_GlowFalloff",0.001f);
    }

    public void StartTalkingCoroutine()
    {
        StartCoroutine(StartTalking());
    }
    IEnumerator StartTalking()
    {
        isStartedTalking = true;
        _animator.SetBool("Talking",true);
        npcCanvas.gameObject.SetActive(true);
        text.text = dialog[dialogIndex].lineOne;
        audio.Play();
        yield return new WaitForSecondsRealtime(dialog[dialogIndex].timeToWaiteOne);
        text.text = dialog[dialogIndex].lineTwo;
        if(!audio.isPlaying)
            audio.Play();
        yield return new WaitForSecondsRealtime(dialog[dialogIndex].timeToWaiteTwo);
        text.text = dialog[dialogIndex].lineThree;
        if (!audio.isPlaying)
            audio.Play();
        yield return new WaitForSeconds(dialog[dialogIndex].timeToWaiteThree);
       _animator.SetBool("Talking",false);
       
       npcCanvas.gameObject.SetActive(false);
       dialogIndex++;
       isFinishedTalking = true;
       if (dialogIndex == 3)
       {
           npc.pointNum = 1;
       }
       if (dialogIndex == 4)
       {
          npc.pointNum = 2;
          yield return new WaitForSeconds(1);
          for (float i = 0; i <= 1; i += .01f)
          {
            dissolveMaterial.SetFloat("_DissolveAmount", i );
            dissolveMaterial.SetFloat("_GlowRange",i/2);
            dissolveMaterial.SetFloat("_GlowFalloff",i/2);
            foreach (ParticleSystem particle in _particles)
            {
                particle.Stop();
            }
            yield return new WaitForSeconds(.01f);
          }
          npc.GetComponent<Rigidbody>().IsDestroyed();
          npc.GetComponent<Collider>().enabled = false;
          npc.GetComponent<MeshRenderer>().enabled = false;
          npc.GetComponentInChildren<MeshRenderer>().enabled = false;
          npc.GetComponentInChildren<Collider>().enabled = false;
          
       }
    }
}
