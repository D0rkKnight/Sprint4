using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingUIKey : MonoBehaviour
{
    public KeyCode code = KeyCode.A;
    public TMP_Text textObject;


    Vector3 stableScale = new Vector3(1, 1, 1);
    Vector3 pressedScale = new Vector3(1.1f, 1.1f, 1.1f);

    private void Start()
    {
        // Set the TMPro text object on game init
        textObject.text = code.ToString();
    }

    private void Update()
    {
        // If the key is pressed, do something
        if (Input.GetKey(code))
        {
            // Lerp the button up
            LeanTween.scale(gameObject, pressedScale, 0.1f).setEase(LeanTweenType.easeOutQuad);
        }
        else
        {
            // Lerp the button down
            LeanTween.scale(gameObject, stableScale, 0.1f).setEase(LeanTweenType.easeOutQuad);
        }
    }

    // When code is updated in the editor, update the TMPro text object
    private void OnValidate()
    {
        textObject.text = code.ToString();
    }

    // When component is added, automatically find TMPro object in self/children
    private void Reset()
    {
        textObject = GetComponentInChildren<TMP_Text>();
    }
}