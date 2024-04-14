using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSUpdater : MonoBehaviour
{
    float fps;

    float updateTimer = 0.2f;

    [SerializeField]
    private TextMeshProUGUI TextMeshProUGUI;

    [SerializeField]
    public bool showFPS = true;

    void Update()
    {
        if (showFPS)
        {
            updateTimer -= Time.deltaTime;
            if (updateTimer <= 0)
            {
                fps = 1.0f / Time.deltaTime;
                TextMeshProUGUI.text = "FPS: " + Mathf.Round(fps);
                updateTimer = 0.2f;
            }
        }
    }

    public void ToggleFPS()
    {
        showFPS = !showFPS;
        TextMeshProUGUI.enabled = showFPS;
    }
}
