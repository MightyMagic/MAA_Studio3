using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleCaptcherMeter : MonoBehaviour
{
    public float meterThreshold = 6f;
    [SerializeField] Image meter;
    //[SerializeField] List<PuzzleWord> words;
    public float meterSpeed;
    public void PuzzleCatcherMeterUpdater(PuzzleWord word)
    {
        
         float scaledMeter = Mathf.Clamp(word.currentMeter, 0f, meterThreshold);
         meter.transform.localScale = new Vector3(scaledMeter, .25f, 1);

         if (word.currentMeter < meterThreshold)
         {
             word.currentMeter += meterSpeed * Time.deltaTime;
         }
         else
         {
             word.currentMeter = 0;
         }
        
       
    }
}

