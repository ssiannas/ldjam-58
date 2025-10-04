using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private void Awake()
    {
        _scoreManager = GetComponent<ScoreManager>(); 
        if (_scoreManager is null)
        {
            Debug.LogError("ScoreManager component is missing from GameManager GameObject", this);
            return;
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
