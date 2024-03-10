using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WobblyText : MonoBehaviour
{
    public TMP_Text textComponent;
     float wobbleSpeed = 50;
     float wobbleDistance = 50;
    PuzzleWord puzzleWord;

    private void Start()
    {
        puzzleWord = GetComponentInParent<PuzzleWord>();
    }

    void Update()
    {
     
        wobbleSpeed = Mathf.Lerp(50f, 0f, puzzleWord.currentMeter);
        wobbleDistance = Mathf.Lerp(50f, 0f, puzzleWord.currentMeter);

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
