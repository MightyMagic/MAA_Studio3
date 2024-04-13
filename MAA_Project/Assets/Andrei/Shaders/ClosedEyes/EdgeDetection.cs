using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EdgeDetection : MonoBehaviour
{
    public Shader edgeDetectShader;
    public Material edgeDetectMaterial;

    void Start()
    {
        //edgeDetectMaterial = new Material(edgeDetectShader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, edgeDetectMaterial);
    }
}
