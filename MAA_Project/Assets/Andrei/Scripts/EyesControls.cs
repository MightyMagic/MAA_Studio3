using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Ideally should be done via observer
/// </summary>
public class EyesControls : MonoBehaviour
{
    [SerializeField] GameObject openEyesEnvironment;

    [SerializeField] Image grayNoise;

    public bool eyesClosed;

    void Start()
    {
        eyesClosed = false;

        ChangeUI(false);
    }

    void Update()
    {
        SwapMeshes();
    }

    private void SwapMeshes()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !eyesClosed)
        {
            DisableMeshes(openEyesEnvironment.transform);
            eyesClosed = true;

            ChangeUI(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && eyesClosed)
        {
            EnableMeshes(openEyesEnvironment.transform);
            eyesClosed = false;

            ChangeUI(false);
        }
    }

    private void EnableMeshes(Transform meshT)
    {
        foreach (Transform child in meshT)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }

            EnableMeshes(child);
        }
    }
    
    private void DisableMeshes(Transform meshT)
    {
        foreach(Transform child in meshT)
        {
            MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            DisableMeshes(child);
        }
    }

    private void ChangeUI(bool b)
    {
        grayNoise.enabled = b;
    }
}
