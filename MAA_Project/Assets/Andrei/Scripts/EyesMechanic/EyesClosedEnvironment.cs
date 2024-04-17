using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class EyesClosedEnvironment : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] Material floorMaterial;
    [SerializeField] Material surroundingMaterial;

    [SerializeField] int floorLayerNumber;
    [SerializeField] int surroundingLayerNumber;

    public List<GameObject> arrayOfSurroundings;
    public List<GameObject> floorObjects;

    [Header("Desired Mask")]
    [SerializeField] int eyesLayerNumber;

    public int spawnedObjectsCount = 0;

    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

        for(int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i] != null)
            {
                if (allObjects[i].layer == floorLayerNumber)
                    floorObjects.Add(allObjects[i]);
                else if (allObjects[i].layer == surroundingLayerNumber)
                    arrayOfSurroundings.Add(allObjects[i]);
            }
        }

        GenerateEyesClosed();
    }

    private void Update()
    {
       // if (Input.GetKeyDown(KeyCode.H))
       // {
       //     GenerateEyesClosed();
       // }
    }

   //void OnTriggerEnter(Collider other)
   //{
   //    // Check if the other object is on the layer we're interested in
   //    if (floorLayer == (floorLayer | (1 << other.gameObject.layer)))
   //    {
   //        floorObjects.Add(other.gameObject);
   //        //Debug.Log("Started overlapping with object: " + other.gameObject.name);
   //    }
   //
   //    if (surroundingLayer == (surroundingLayer | (1 << other.gameObject.layer)))
   //    {
   //        arrayOfSurroundings.Add(other.gameObject);
   //        //Debug.Log("Started overlapping with object: " + other.gameObject.name);
   //    }
   //}

    void SpawnTrackSphere()
    {
        // Create a large sphere at the center of the scene
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = Vector3.zero; // Center of the scene
        sphere.transform.localScale = new Vector3(250, 250, 250); // Large size

        // Add a Rigidbody component to enable physics interactions
        Rigidbody rb = sphere.AddComponent<Rigidbody>();
        rb.isKinematic = true; // Make the Rigidbody not affected by physics

        // Add a SphereCollider component to detect collisions
        SphereCollider sc = sphere.AddComponent<SphereCollider>();
        sc.isTrigger = true; // Make the SphereCollider a trigger to detect all objects it overlaps with
    }

    public void CLearEyesClosed()
    {

    }

    public void GenerateEyesClosed()
    {

        Debug.LogError("Entered generating stage");

        //SpawnTrackSphere();

        //GameObject[] floorObjects = GameObject.FindGameObjectsWithTag("Ground");
        //GameObject[] arrayOfSurroundings = GameObject.FindGameObjectsWithTag("Obstacle");

        for(int i = 0; i < floorObjects.Count; i++)
        {
            Debug.LogError("Entered first cycle");

            
            GameObject go = Instantiate(floorObjects[i], floorObjects[i].transform.position, floorObjects[i].transform.rotation);
            go.transform.parent = floorObjects[i].transform;
            go.name = "EyesClosed"+floorObjects[i].name;

            go.layer = eyesLayerNumber;

            ProBuilderMesh pbMesh = go.GetComponent<ProBuilderMesh>();
            

            if (pbMesh != null)
            {
                MeshRenderer meshRenderer = pbMesh.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {
                    meshRenderer.sharedMaterial = floorMaterial;
                    //meshRenderer.material = floorMaterial;
                }
                
            }
            else
            {
                if (go.GetComponent<Renderer>())
                {
                    go.GetComponent<Renderer>().material = floorMaterial;
                }
            }

            // if (go.GetComponent<Renderer>())
            // {
            //     go.GetComponent<Renderer>().material = floorMaterial;
            // }

            if (go.GetComponent<Collider>())
            {
                Destroy(go.GetComponent<Collider>());
            }

            spawnedObjectsCount++;
        }

        for (int i = 0; i < arrayOfSurroundings.Count; i++)
        {
            Debug.LogError("Entered second cycle");


            GameObject go = Instantiate(arrayOfSurroundings[i], arrayOfSurroundings[i].transform.position, arrayOfSurroundings[i].transform.rotation);
            go.transform.parent = arrayOfSurroundings[i].transform;
            go.name = "EyesClosed" + arrayOfSurroundings[i].name;

            go.layer = eyesLayerNumber;

            //Component[] goComponents = go.GetComponents<Component>();
            //
            //
            //if (goComponents.Length > 0)
            //{
            //    foreach (Component component in goComponents)
            //    {
            //        // Remove the component
            //        DestroyImmediate(component);
            //    }
            //
            //    //probuilderComponents.Free();
            //}

            //go.AddComponent<MeshRenderer>();

            ProBuilderMesh pbMesh = go.GetComponent<ProBuilderMesh>();

            if (pbMesh != null)
            {
                MeshRenderer meshRenderer = pbMesh.GetComponent<MeshRenderer>();

                if (meshRenderer != null)
                {
                    meshRenderer.sharedMaterial = surroundingMaterial;
                }   
            }
            else
            {
                if (go.GetComponent<Renderer>())
                {
                    go.GetComponent<Renderer>().material = surroundingMaterial;
                }
            }

            //if (go.GetComponent<Renderer>())
            //{   
            //    go.GetComponent<Renderer>().material = surroundingMaterial;
            //}

            if (go.GetComponent<Collider>())
            {
                Destroy(go.GetComponent<Collider>());
            }

            spawnedObjectsCount++;
                
        }

        //Debug.LogError("Spawned " arra)
    }
}
