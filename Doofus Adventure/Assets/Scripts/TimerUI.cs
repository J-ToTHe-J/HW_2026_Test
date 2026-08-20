using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public static TimerUI Instance;
    public TextMeshProUGUI timerText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateTimer(float value)
    {
        if (timerText == null)
        {
            Debug.LogError("TimerUI: timerText field is not assigned!");
            return;
        }
        timerText.text = Mathf.Max(value, 0f).ToString("F2");
    }
}