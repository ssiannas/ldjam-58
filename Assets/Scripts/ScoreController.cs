using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private UIChannel uiChannel;
    private uint _souls;
    
    private void Awake()
    {
        if (uiChannel is null)
        {
            Debug.LogError("ScoreChannel is not assigned in the inspector", this);
            return;
        }
    }

    public void AddSouls(uint numSouls)
    {
        _souls += numSouls;
        uiChannel.UpdateScore(_souls.ToString());
    }
}
