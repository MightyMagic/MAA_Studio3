using UnityEngine;
using TMPro;

public class DuplicateTextEffect : MonoBehaviour
{
    TMP_Text text;
    [SerializeField] TMP_Text textComponent;
    WordInSpace space;
    [SerializeField] private Transform originalTransform;

    void Start()
    {  
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
        TextAlpha(targetDistance);
        // Adjust the move vector accordingly
        Vector3 move = direction.normalized * targetDistance;

        // Move the text
        transform.position = Vector3.Lerp(transform.position, targetPosition - move, 0.009f);
        if (space.wordSpaceCurrentMeter >= .9f )
        {
            gameObject.SetActive(false);
        }
    }

    private void TextAlpha(float meter)
    {
        text.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower,meter);
    }
    public void ResetDuplicateTextPos()
    {
        gameObject.SetActive(true);
        transform.position = originalTransform.position;
        transform.rotation = originalTransform.rotation;
    }
}
