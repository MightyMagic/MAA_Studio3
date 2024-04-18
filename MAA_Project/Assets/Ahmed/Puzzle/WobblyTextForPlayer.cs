using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WobblyTextForPlayer : MonoBehaviour
{
    TMP_Text textComponent;
    [SerializeField] float wobbleSpeed;
    [SerializeField] float wobbleDistance;
    WordInSpace puzzleWord;

    private void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        puzzleWord = GetComponentInParent<WordInSpace>();
    }

    void Update()
    {
     
        wobbleSpeed = Mathf.Lerp(wobbleSpeed, 0f, puzzleWord.wordSpaceCurrentMeter);
        wobbleDistance = Mathf.Lerp(wobbleDistance, 0f, puzzleWord.wordSpaceCurrentMeter);

        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;
        

        for(int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if(!charInfo.isVisible)
            {
                continue;
            }
            
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            for(int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(Mathf.Sin(Time.time * wobbleSpeed + orig.x * 0.01f) * wobbleDistance,
                    Mathf.Sin(Time.time * wobbleSpeed + orig.x * 0.01f) * wobbleDistance, 0);
            }
        }
        for(int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}