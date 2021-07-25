using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsDisplay;

    private Color critical = new Color(1f, 0.3632f, 0.3349f, 0f);
    private float fps;



    // Update is called once per frame
    void Update()
    {
        fps = 1 / Time.unscaledDeltaTime;

        fpsDisplay.text = "FPS  + fps.ToString("00");

        if (fps <= 30)
        {
            fpsDisplay.color = critical;
        }
        else fpsDisplay.color = Color.white;
    }
}
