using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLoader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.2f;

    private void Start()
    {
        fadeGroup = GetComponent<CanvasGroup>();
        fadeGroup.alpha = 1.0f;
    }

    private void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }
}
