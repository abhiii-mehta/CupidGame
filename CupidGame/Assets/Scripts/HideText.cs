using TMPro;
using UnityEngine;

public class AutoHideText : MonoBehaviour
{
    public float delay = 3f;

    void Start()
    {
        Invoke(nameof(HideText), delay);
    }

    void HideText()
    {
        gameObject.SetActive(false);
    }
}
