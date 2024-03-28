using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildValues : MonoBehaviour
{
    [SerializeField] List<DebugVariable> variables = new List<DebugVariable>();

    private void Start()
    {
        DisplayCurrentValues();
    }

    public void DisplayCurrentValues()
    {
        for (int i = 0; i < variables.Count; i++)
        {
            if (variables[i] != null)
            {
                if (PlayerPrefs.HasKey(variables[i].playerPrefsKey))
                {
                    variables[i].valueSlider.value = PlayerPrefs.GetFloat(variables[i].playerPrefsKey);
                    variables[i].valueText.text = "Is " + PlayerPrefs.GetFloat(variables[i].playerPrefsKey).ToString();
                }             
            }
        }
    }

    public void SaveNewValues()
    {
        for(int i = 0; i < variables.Count; i++)
        {
            if (variables[i] != null)
            {
                PlayerPrefs.SetFloat(variables[i].playerPrefsKey, variables[i].valueSlider.value);
                variables[i].valueText.text = "Is " + PlayerPrefs.GetFloat(variables[i].playerPrefsKey).ToString();
            }
        }
    }
}

[System.Serializable]
public class DebugVariable
{
    public Slider valueSlider;
    public string playerPrefsKey;
    public TextMeshProUGUI valueText;
}
