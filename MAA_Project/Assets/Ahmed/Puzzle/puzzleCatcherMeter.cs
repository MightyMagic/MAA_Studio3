using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puzzleCatcherMeter : MonoBehaviour
{
    public float meterThreshold = 6f;
    [SerializeField] Image meter;
    public float currentMeter;
    public float meterSpeed;
    public void PuzzleCatcherMeterUpdater()
    {
        float scaledMeter = Mathf.Clamp(currentMeter,0f,meterThreshold);
        meter.transform.localScale = new Vector3(scaledMeter, .25f, 1);

        if(currentMeter < meterThreshold)
        {
            currentMeter += meterSpeed * Time.deltaTime;
        }
    }
}

