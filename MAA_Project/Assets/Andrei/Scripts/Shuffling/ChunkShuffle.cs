using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkShuffle : MonoBehaviour
{
    [Header("A star")]
    [SerializeField] Grid aStarGrid;

    [Header("Monster")]
    [SerializeField] SimpleMonster monsterScript;

    [Header("List of objects that stay within the room")]
    [SerializeField] List<KeepInPlace> objectsInPlace;

    [Header("Newest chunk")]
    public List<GameObject> draftChunks;

    [Header("Finished structure")]
    public List<ChunkConfiguration> configurations = new List<ChunkConfiguration>();

    private int currentIndex;

    void Start()
    {
        for(int i = 0; i < configurations.Count; i++)
        {
            if (configurations[i].isDefault)
            {
                PlaceChunks(configurations[i]);
                currentIndex = i;
            }
        }

        StartCoroutine(RebuildGrid());
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {                                                                                
           RearrangeChunks();
           StartCoroutine(RebuildGrid());
        } 
    }

    private void RearrangeChunks()
    {
        for (int i = 0; i < objectsInPlace.Count; i++)
        {
            objectsInPlace[i].FetchPosition();
        }

        PlaceChunks(FetchNextChunk(currentIndex));
        currentIndex = (currentIndex + 1) % configurations.Count;

        for (int i = 0; i < objectsInPlace.Count; i++)
        {
            objectsInPlace[i].MoveToPosition();
        }
    }

    private ChunkConfiguration FetchNextChunk(int index)
    {
        return configurations[(index + 1) % configurations.Count];
    }

    void PlaceChunks(ChunkConfiguration chunk)
    {
        for (int i = 0; i < chunk.anchorObjects.Count; i++)
        {
            chunk.anchorObjects[i].chunkObject.transform.position = chunk.anchorObjects[i].anchorPosition;
            chunk.anchorObjects[i].chunkObject.transform.rotation = Quaternion.Euler(chunk.anchorObjects[i].anchorAngle);
        }

        aStarGrid.CreateGrid();
        monsterScript.ClearStackOfPoints();

        // Hide certain objects
        for (int j = 0; j < chunk.objectsToHide.Count; j++)
        {
            chunk.objectsToHide[j].SetActive(false);
        }

        // Reveal certain objects

        for (int j = 0; j < chunk.objectsToReveal.Count; j++)
        {
            chunk.objectsToReveal[j].SetActive(true);
        }

       //aStarGrid.CreateGrid();
       //monsterScript.ClearStackOfPoints();
    }

    public IEnumerator RebuildGrid()
    {
        yield return new WaitForSeconds(0.5f);
        aStarGrid.CreateGrid();
        monsterScript.ClearStackOfPoints();
    }

   

    public void AppendNewConfiguration()
    {
        if (draftChunks.Count > 0)
        {
            ChunkConfiguration newConfiguration = new ChunkConfiguration();

            for(int i = 0; i < draftChunks.Count; i++)
            {
                Anchor newAnchor = new Anchor();
                newAnchor.chunkObject = draftChunks[i];
                newAnchor.anchorPosition = draftChunks[i].transform.position;
                newAnchor.anchorAngle = draftChunks[i].transform.rotation.eulerAngles;

                newConfiguration.anchorObjects.Add(newAnchor);
            }

            configurations.Add(newConfiguration);
        }
        else
        {
            Debug.LogError("No chunks to append!");
        }

        draftChunks.Clear();
    }
}

[System.Serializable]
public class ChunkConfiguration
{
    public List<Anchor> anchorObjects = new List<Anchor>();
    public bool isDefault;

    public List<GameObject> objectsToHide;
    public List<GameObject> objectsToReveal;
}


[System.Serializable]
public class Anchor
{
    public GameObject chunkObject;
    public Vector3 anchorPosition;
    public Vector3 anchorAngle;
}

