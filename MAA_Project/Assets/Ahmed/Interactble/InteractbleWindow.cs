using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractbleWindow : MonoBehaviour
{
    Interactble interactble;
    [SerializeField] TextMeshProUGUI text;
    GameObject go;

    
    public void OpenObjectInCanvas(GameObject interactbleObject)
    {
        interactble = interactbleObject.GetComponent<Interactble>();
        if(interactble == null)
        {
            Debug.Log("interactble is null");
        }
        else
        {
            go = Instantiate(interactble.go, transform.GetChild(0));
            go.layer = LayerMask.NameToLayer("UI");
            go.transform.localScale = new Vector3(3000, 3000, 3000);
            go.AddComponent<Rotate>();
        }
    } 
    public void DestroyCanvasGameObject()
    {
        Destroy(go);
    }
}
