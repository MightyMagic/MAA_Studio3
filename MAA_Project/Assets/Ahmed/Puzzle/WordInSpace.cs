using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordInSpace : MonoBehaviour 
{
    public TextMeshProUGUI textMeshPro;
    public string wordInSpaceText;
    public float wordSpaceCurrentMeter = 0;
    public bool wordIsInPanel;
    private void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

}
