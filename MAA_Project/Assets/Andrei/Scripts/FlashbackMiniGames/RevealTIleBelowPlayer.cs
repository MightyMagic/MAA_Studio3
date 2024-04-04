using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealTIleBelowPlayer : MonoBehaviour
{

    [SerializeField] LayerMask tileMask;
    Renderer hitRenderer;

    FallOffTiles fallScript;

    void Start()
    {
        fallScript = GetComponent<FallOffTiles>();
    }

    void Update()
    {
        CheckForTileBelow();
    }

    void CheckForTileBelow()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, tileMask))
        {
            hitRenderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (hitRenderer != null)
            {
                 hitRenderer.enabled = true;
            }
        }
        else
        {
            if(fallScript != null)
            {
                CharacterController characterController = GetComponent<CharacterController>();
                if (characterController != null)
                {
                    if (characterController.enabled)
                    {
                        characterController.enabled = false;
                    }
                }

                fallScript.startedFalling = true;
            }

            if (hitRenderer != null)
            {
                hitRenderer.enabled = false;
                hitRenderer = null;
            }
        }
    }
}

