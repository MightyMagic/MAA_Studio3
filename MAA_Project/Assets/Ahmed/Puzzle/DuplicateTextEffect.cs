using UnityEngine;
using TMPro;

public class DuplicateTextEffect : MonoBehaviour
{
    TMP_Text text;
    [SerializeField] TMP_Text textComponent;
    WordInSpace space;
    public Vector3 originalPosition;

    void Start()
    {  
        originalPosition = transform.position;
        space = GetComponentInParent<WordInSpace>();
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = textComponent.text;
        if (text != null && textComponent != null)
        {
            MoveToOriginalText();
        }
    }

    void MoveToOriginalText()
    {
        Vector3 targetPosition = textComponent.transform.position;
        Vector3 direction = targetPosition - transform.position;

        // Calculate the target distance based on SpaceCurrentMeter
        float targetDistance = Mathf.Lerp(direction.magnitude, 0f, space.wordSpaceCurrentMeter);

        // Adjust the move vector accordingly
        Vector3 move = direction.normalized * targetDistance;

        // Move the text
        transform.position = Vector3.Lerp(transform.position, targetPosition - move, 0.009f);
    }
    public void ResetDuplicateTextPos()
    {
        transform.position = originalPosition;
    }
}
