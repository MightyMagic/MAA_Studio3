using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleDialogue : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] List<DialogueObject> dialogueObjects = new List<DialogueObject>();

    [SerializeField] AudioSource audioSource;

    [SerializeField] List<TextMeshProUGUI> emilyOptions = new List<TextMeshProUGUI>(); 
    [SerializeField] TextMeshProUGUI subtitlePlaceHolder;

    [Header("Item")]
    [SerializeField] ItemActivation item;

    private int phrasesIndex = 0;
    private int responseIndex = 0;

    private int emilyIndex;

    void Start()
    {
        //item.enabled = false;
        ClearCurrentTexts();
        
        AppendNewPhrase();
    }

    void Update()
    {
        CheckForInput();
    }

    void CheckForInput()
    {
        if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
        {
            print("Pressed button!");
            responseIndex = (responseIndex + 1) % emilyOptions.Count;
            print("Responde index now is: " + responseIndex);

            

            for(int i = 0; i < emilyOptions.Count; i++)
            {
                if (i == responseIndex)
                {
                    if (emilyOptions[responseIndex].enabled == true)
                    {
                        if (emilyOptions[responseIndex].text != string.Empty)
                        {
                            emilyOptions[responseIndex].color = Color.red;
                        }
                        else
                        {
                            responseIndex = 0;
                        }
                    }
                }
                else
                {
                    emilyOptions[i].color = Color.white;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (emilyOptions[responseIndex].text != string.Empty)
            {
                StartCoroutine(PlayPhrase(dialogueObjects[responseIndex]));
            }
           

            foreach (TextMeshProUGUI t in emilyOptions)
            {
                t.enabled = false;
                
            }
        }
    }

    public IEnumerator PlayPhrase(DialogueObject dialogueObject)
    {
        float timer = dialogueObject.clip.length;

        yield return new WaitForSeconds(1f);

        audioSource.PlayOneShot(dialogueObject.clip);

        subtitlePlaceHolder.enabled = true;
        subtitlePlaceHolder.text = dialogueObject.text;

        yield return new WaitForSeconds(timer + 1f);

        subtitlePlaceHolder.enabled = false;

        // dialogue is over
       //if(0 == dialogueObjects.Count - 1)
       //{
       //
       //}

        int index = dialogueObjects.IndexOf(dialogueObject);
        //for(int i = 0; i <= index; i++)
        //{
        //    dialogueObjects.RemoveAt(i);
        //}

        if (emilyOptions[emilyOptions.Count - 1].text != string.Empty)
        {
            dialogueObjects.RemoveAt(0);
        }

        dialogueObjects.RemoveAt(0);

        subtitlePlaceHolder.enabled = false;
        ClearCurrentTexts();

        AppendNewPhrase();
    }

    public void AppendNewPhrase()
    {
        if (dialogueObjects.Count > 0)
        {
            if (dialogueObjects[0].characterName == CharacterName.Doctor)
            {
                StartCoroutine(PlayPhrase(dialogueObjects[0]));
            }
            else if(dialogueObjects[0].characterName == CharacterName.Emily)
            {
                emilyOptions[0].color = Color.red;

                for (int i = 0; i < emilyOptions.Count; i++)
                {
                    emilyOptions[i].enabled = true;
                }

                emilyOptions[0].text = dialogueObjects[0].text;

                if (dialogueObjects[1] != null)
                {
                    if (dialogueObjects[1].characterName == CharacterName.Emily)
                    {
                        emilyOptions[1].text = dialogueObjects[1].text;
                    }
                }
            }
        }
        else
        {
            //item.enabled = true;
            item.EnablePortal();
        }
    }

    void ClearCurrentTexts()
    {
        for (int i = 0; i < emilyOptions.Count; i++)
        {
            emilyOptions[i].text = string.Empty;
        }

        subtitlePlaceHolder.text = string.Empty;
    }
}

[System.Serializable]
public class DialogueObject
{
    public string text;
    public AudioClip clip;
    public CharacterName characterName;

}

public enum CharacterName
{
    Emily,
    Doctor,
    Monster
}
