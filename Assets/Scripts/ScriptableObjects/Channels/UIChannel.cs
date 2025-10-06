using System;
using UnityEngine;
using UnityEngine.Events;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "UIChannel", menuName = "SO/Channels/UIChannel")]
    public class UIChannel : ScriptableObject
    {
        public UnityAction<string> OnUpdateScore;
        public UnityAction OnUpgradeAvailable;
        public UnityAction OnUpgradeReset;
        public UnityAction OnRequestUpgradeData;
        public UnityAction<Upgrade> OnShowTooltip;
        public UnityAction OnHideTooltip;
        public Func<float> OnGetCanvasScaleFactor;
        public UnityAction<float> OnForceUpdateCursor;
        public UnityAction<string> OnShowDialog;

        public void UpdateScore(string newScore)
        {
            OnUpdateScore?.Invoke(newScore);
        }

        public void NotifyUpgradeAvailable()
        {
            OnUpgradeAvailable?.Invoke();
        }

        public void NotifyUpgradeReset()
        {
            OnUpgradeReset?.Invoke();
        }

        public void GetUpgradeData()
        {
            OnRequestUpgradeData?.Invoke();
        }

        public void ShowTooltip(Upgrade upgrade)
        {
            OnShowTooltip?.Invoke(upgrade);
        }

        public void HideTooltip()
        {
            OnHideTooltip?.Invoke();
        }
        
        public float GetCanvasScaleFactor()
        {
            return OnGetCanvasScaleFactor?.Invoke() ?? 1f;  
        }

        public void ForceUpdateCursor(float newAspect)
        {
            OnForceUpdateCursor?.Invoke(newAspect);
        }
        
        public void ShowDialog(string id)
        {
            OnShowDialog?.Invoke(id);
        }
    }
}