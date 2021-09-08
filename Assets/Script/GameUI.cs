using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static Slider healthBar { get; private set; }
    public static TextMeshProUGUI timeText { get; private set; }

    void Start()
    {
        healthBar = GetComponentInChildren<Slider>();
        timeText = GetComponentInChildren<TextMeshProUGUI>();
        timeText.text = "0";
    }

    void FixedUpdate()
    {
        timeText.text = Mathf.FloorToInt(Time.timeSinceLevelLoad).ToString();
    }

    public static void SetHealthUI(float healthRatio)
    {
        if (healthBar)
            healthBar.value = Mathf.Clamp01(healthRatio);
        else
            throw new System.Exception("You're trying to set health UI while UI is not instanced");
    }
}
