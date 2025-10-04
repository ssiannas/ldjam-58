using System;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
   
    private float _timer = 0;
    private TextMeshProUGUI _timerText = null;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
    }

    void Awake()
    {
        
    }

    private void SetText(string text)
    {
        if (_timerText is null) return;
        _timerText.text = text;
    }
    
    // Update is called once per frame
    void Update()
    {
       _timer += Time.deltaTime;
       var timeSpan = TimeSpan.FromSeconds(_timer);
       // Trim down to MM:SS
       timeSpan = new TimeSpan(0, timeSpan.Minutes, timeSpan.Seconds);
       SetText(timeSpan.ToString(@"mm\:ss"));
    }
}
