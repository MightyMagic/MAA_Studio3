using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class KeepInPlace : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject markObjectPrefab;
    Quaternion newAngle;

    Quaternion oldRotation;
    Quaternion newRotation;

    PlayerMovmentAhmed playerMovement;

    GameObject markObject;

    Transform currentFloor;
    Vector3 offset;

    void Awake()
    {
        markObject = Instantiate(markObjectPrefab);
        markObject.name = "mark_" + gameObject.name;

        if (gameObject.tag == "Player")
        {
            playerMovement = gameObject.GetComponent<PlayerMovmentAhmed>();
        }
    }

    private void Start()
    {
        FetchPosition();
    }

    void Update()
    {
        //FetchPosition();
    }

    public void FetchPosition()
    {
        //Debug.LogError("Fetching position of " + markObject.name);

        RaycastHit hit;
        if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            Debug.Log("Hit " + hit.collider.gameObject.name);

            //oldRotation = markObject.transform.rotation;

            markObject.transform.rotation = gameObject.transform.rotation;
            var point = hit.point;
            point.y = gameObject.transform.position.y;
            markObject.transform.position = hit.point;

            //// memorizing the floor tile and offset
            currentFloor = hit.transform;

            Debug.LogError("Current floor is " + currentFloor.name);
            //offset = gameObject.transform.position - currentFloor.position;

            markObject.transform.parent = currentFloor.transform;
            //markObject.transform.localScale = Vector3.one;
          
        }
    }

    public void MoveToPosition()
    {
        if(markObject != null && (markObject.transform.position - gameObject.transform.position).magnitude > 6f)
        {

            //await Task.Delay(100);

           // newRotation = markObject.transform.rotation;
           // newAngle = newRotation * Quaternion.Inverse(oldRotation);
           //
           //
           // gameObject.transform.rotation = newAngle * gameObject.transform.rotation;

            Vector3 newPos = new Vector3(markObject.transform.position.x, gameObject.transform.position.y, markObject.transform.position.z);
            gameObject.transform.position = newPos;
            //gameObject.transform.position = markObject.transform.position;

            gameObject.transform.rotation = markObject.transform.rotation;

            //gameObject.transform.position = currentFloor.transform.position + offset;        
        }
    }

    void SaveVector3(Vector3 vector, string key)
    {
        PlayerPrefs.SetFloat(key + "_X", vector.x);
        PlayerPrefs.SetFloat(key + "_Y", vector.y);
        PlayerPrefs.SetFloat(key + "_Z", vector.z);
        PlayerPrefs.Save();
    }

    Vector3 LoadVector3(string key)
    {
        float x = PlayerPrefs.GetFloat(key + "_X", 0f);
        float y = PlayerPrefs.GetFloat(key + "_Y", 0f);
        float z = PlayerPrefs.GetFloat(key + "_Z", 0f);
        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        // Set the color of the Gizmo
        Gizmos.color = Color.blue;

        // Get the position of the GameObject
        Vector3 startPos = this.gameObject.transform.position;

        // Get the direction the GameObject is facing
        Vector3 direction = Vector3.down;

        // Draw the ray in the scene view
        Gizmos.DrawRay(startPos, direction * 5f);

        Gizmos.color = Color.black;

        if (markObject != null)
        {
            Vector3 direction1 = transform.position - markObject.transform.position;
            Gizmos.DrawRay(markObject.transform.position, direction1);
        }
       
    }
}

