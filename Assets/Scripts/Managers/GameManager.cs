using System;
using TMPro;
using UnityEngine;

namespace ldjam_58
{
    public class GameManager : MonoBehaviour
    {
        private ScoreController _scoreController;

        private void Awake()
        {
            _scoreController = GetComponent<ScoreController>();
            if (_scoreController is null)
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
}
