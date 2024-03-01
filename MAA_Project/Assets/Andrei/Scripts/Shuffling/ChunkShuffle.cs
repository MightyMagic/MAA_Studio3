using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkShuffle : MonoBehaviour
{
    [Header("Newest chunk")]
    public List<GameObject> draftChunks;

    [Header("Finished structure")]
    public List<ChunkConfiguration> configurations = new List<ChunkConfiguration>();
    void Start()
    {
        
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            ShuffleChunks();
            PlaceChunks(configurations[0]);
        } 
    }

    void PlaceChunks(ChunkConfiguration chunks)
    {
        for (int i = 0; i < chunks.AnchorObjects.Count; i++)
        {
            chunks.AnchorObjects[i].chunkObject.transform.position = chunks.AnchorObjects[i].anchorPosition;
            chunks.AnchorObjects[i].chunkObject.transform.rotation = Quaternion.Euler(chunks.AnchorObjects[i].anchorAngle);
        }
    }

    public void ShuffleChunks()
    {
        Shuffle<ChunkConfiguration>(configurations);
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

                newConfiguration.AnchorObjects.Add(newAnchor);
            }

            configurations.Add(newConfiguration);
        }
        else
        {
            Debug.LogError("No chunks to append!");
        }

        draftChunks.Clear();
    }

    public static void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();

        List<T> originalList = new List<T>(list); 

        
        do
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        } while (list.SequenceEqual(originalList)); 

    }
}

[System.Serializable]
public class ChunkConfiguration
{
    public List<Anchor> AnchorObjects = new List<Anchor>();
}


[System.Serializable]
public class Anchor
{
    public GameObject chunkObject;
    public Vector3 anchorPosition;
    public Vector3 anchorAngle;
}

