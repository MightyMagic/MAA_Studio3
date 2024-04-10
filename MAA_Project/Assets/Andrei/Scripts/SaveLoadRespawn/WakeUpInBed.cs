using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUpInBed : MonoBehaviour
{
    [SerializeField] Transform lookPoint;
    [SerializeField] GameObject playerObject;

    [SerializeField] List<RespawnBed> respawnBeds;
    [SerializeField] float duration;

    [SerializeField] Camera mainCam;

    float elapsedTime = 0f;
    public AnimationCurve movementCurve;


    void Start()
    {
        //playerObject.SetActive(false);
    }

    void Update()
    {
        
    }

    

    public IEnumerator WakeUp()
    {
        Debug.LogError("Wake up coroutine!");
        playerObject.SetActive(false);

        RespawnBed respawnBed = AssignBed();

        mainCam.transform.position = respawnBed.cameraInitialPos.position;
        mainCam.transform.rotation = respawnBed.cameraInitialPos.rotation;

        Transform initialTransform = mainCam.transform;
        Transform targetTransform = lookPoint;//respawnBed.playerPosition;

        playerObject.transform.position = respawnBed.playerPosition.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            mainCam.transform.position = Vector3.Lerp(initialTransform.position, targetTransform.position, movementCurve.Evaluate(t));
            mainCam.transform.rotation = Quaternion.Slerp(initialTransform.rotation, targetTransform.rotation, movementCurve.Evaluate(t));

            elapsedTime += Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }

        yield return null;
      
        mainCam.transform.position = targetTransform.position;
        mainCam.transform.rotation = targetTransform.rotation;

        //Start the game (activate the player and the enemy)
        playerObject.SetActive(true);
    }

    RespawnBed AssignBed()
    {
        RespawnBed respawnBed = respawnBeds[Random.Range(0, respawnBeds.Count)];
        return respawnBed;
        //mainCam.transform.position = respawnBed.cameraInitialPos.position;
        //mainCam.transform.rotation = respawnBed.cameraInitialPos.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for(int i = 0; i < respawnBeds.Count; i++)
        {
            Vector3 endPos = respawnBeds[i].cameraInitialPos.position + respawnBeds[i].cameraInitialPos.forward * 5f;
            Gizmos.DrawLine(respawnBeds[i].cameraInitialPos.position, endPos);
        }
    }
}

[System.Serializable]
public class RespawnBed
{
    public Transform cameraInitialPos;
    public Transform playerPosition;
   
}
