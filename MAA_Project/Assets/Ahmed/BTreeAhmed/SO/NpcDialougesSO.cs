using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Create Dialog/Create New Dialog")]
public class NpcDialougesSO : ScriptableObject
{
    public string lineOne;
    public string lineTwo;
    public string lineThree;
    public float timeToWaiteOne;
    public float timeToWaiteTwo;
    public float timeToWaiteThree;
    public AudioClip AudioClipOne;
    public AudioClip AudioClipTwo;
    public AudioClip AudioClipThree;

}
