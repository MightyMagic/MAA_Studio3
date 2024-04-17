using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemActivation : MonoBehaviour
{
    [Header("Effect")]
    [SerializeField] GameObject itemObject;
    [SerializeField] Material disruptedMat;

    [SerializeField] float delayBetweenSwaps;

    [Header("UI")]
    [SerializeField] GameObject uiCanvas;
    

    private List<Material> materials = new List<Material>();
    private List<GameObject> children = new List<GameObject>();

    int counter = 0;

    void Start()
    {
        uiCanvas.SetActive(false);

        foreach(Transform child in itemObject.transform)
        {
            children.Add(child.gameObject);
        }

        for(int i = 0; i < children.Count; i++)
        {
            Renderer renderer = children[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                materials.Add(renderer.material);
            }
        }

       
    }

    void Update()
    {
        if(uiCanvas.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetInt("PlayerSpawn", 0);
            PlayerPrefs.SetInt("PhraseIndex", 1);
            SceneManager.LoadScene(2);
        }
    }

    public void EnablePortal()
    {
        InvokeRepeating("SwappingMaterials", 0f, delayBetweenSwaps);
        uiCanvas.SetActive(true);
    }

    public void SwappingMaterials()
    {
        if(counter == 0)
        {
            SwapMaterials(disruptedMat);
            counter = 1;
        }
        else
        {
            for (int i = 0; i < children.Count; i++)
            {
                Renderer renderer = children[i].GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = materials[i];
                }
            }

            counter = 0;
        }
    }

    void SwapMaterials(Material mat)
    {
        for (int i = 0; i < children.Count; i++)
        {
            Renderer renderer = children[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = mat;
            }
        }
    }
}
