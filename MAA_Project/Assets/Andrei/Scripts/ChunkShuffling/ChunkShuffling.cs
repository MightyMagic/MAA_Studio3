using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkShuffling : MonoBehaviour
{
    [SerializeField] List<ChunkConfiguration> configurations;

    private ChunkConfiguration newConfiguration;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shuffle<ChunkConfiguration>(configurations);
            newConfiguration = configurations[0];

            PlaceChunks(newConfiguration);
        }
    }

    void PlaceChunks(ChunkConfiguration configuration)
    {
        for(int i = 0; i < configuration.chunkConfiguration.Count; i++)
        {
            configuration.chunkConfiguration[i].chunkObject.transform.position = configuration.chunkConfiguration[i].anchorTransform.position;
            configuration.chunkConfiguration[i].chunkObject.transform.rotation = Quaternion.Euler(configuration.chunkConfiguration[i].rotation);
        }
    }
    

    public static void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();

        List<T> originalList = new List<T>(list); // Make a copy of the original list

        // Shuffle the list until it's different from the original order
        do
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        } while (list.SequenceEqual(originalList)); // Check if the shuffled list is the same as the original

    }
}

[System.Serializable]
public class ChunkConfiguration
{
    public List<AnchorPlacement> chunkConfiguration;
}

[System.Serializable]
public class AnchorPlacement
{
    public GameObject chunkObject;
    public Transform anchorTransform;
    public Vector3 rotation;

}
