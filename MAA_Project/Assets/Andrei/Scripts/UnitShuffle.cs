using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitShuffle : MonoBehaviour
{

    [SerializeField] List<GameObject> units;

    public List<Vector3> unitAnchors;
    void Start()
    {
        foreach (var unit in units)
        {
            unitAnchors.Add(unit.transform.position);
        }
    }

    void Update()
    {
        
    }

    public void RearrangeUnits()
    {
        Shuffle<Vector3>(unitAnchors);

        for (int i = 0; i < units.Count; i++)
        {
            units[i].transform.position = unitAnchors[i];
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

