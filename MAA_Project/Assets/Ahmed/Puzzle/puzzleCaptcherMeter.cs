using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleCaptcherMeter : MonoBehaviour
{
    public float meterThreshold = 6f;
    [SerializeField] Image meterForWallWord;
    [SerializeField] Image meterInPuzzleAssymbly;
    //[SerializeField] List<PuzzleWord> words;
    public float meterSpeed = 0.2f;
    public void PuzzleCatcherMeterUpdater(PuzzleWord word)
    {   
         float scaledMeter = Mathf.Clamp(word.currentMeter, 0f, meterThreshold);
         meterForWallWord.transform.localScale = new Vector3(scaledMeter, 1, 1);
         if (word.currentMeter < meterThreshold)
         {
             word.currentMeter += meterSpeed * Time.deltaTime;
         }
         else
         {
             word.currentMeter = 0;
         }  
    }public void PuzzleCatcherMeterUpdaterInClosedEyes(WordInSpace word)
    {   
         float scaledMeter = Mathf.Clamp(word.wordSpaceCurrentMeter, 0f, meterThreshold);
        meterInPuzzleAssymbly.transform.localScale = new Vector3(scaledMeter, 1, 1);
         if (word.wordSpaceCurrentMeter < meterThreshold)
         {
             word.wordSpaceCurrentMeter += meterSpeed * Time.deltaTime;
         }
         else
         {
             word.wordSpaceCurrentMeter = 0;
         }  
    }
}

