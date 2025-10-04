using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{

    private float _timer;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private UIChannel uiChannel;
    
    void Awake()
    {
        if (timerText is null)
        {
            Debug.LogError("Timer Text is not assigned in the inspector", this);
            return;
        }
        if (scoreText is null)
        {
            Debug.LogError("Score Text is not assigned in the inspector", this);
            return;
        }
        if (uiChannel is null)
        {
            Debug.LogError("UI Channel is not assigned in the inspector", this);
            return;
        }
        uiChannel.OnUpdateScore += SetScoreText;
    }
    
    private void SetScoreText(string text)
    {
        if (scoreText is null) return;
        scoreText.text = text;
    }

    private void SetTimerText(string text)
    {
        if (timerText is null) return;
        timerText.text = text;
    }
    
    // Update is called once per frame
    void Update()
    {
       _timer += Time.deltaTime;
       var timeSpan = TimeSpan.FromSeconds(_timer);
       // Trim down to MM:SS
       timeSpan = new TimeSpan(0, timeSpan.Minutes, timeSpan.Seconds);
       SetTimerText(timeSpan.ToString(@"mm\:ss"));
    }
}
