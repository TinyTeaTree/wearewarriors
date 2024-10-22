using Core;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoadingScreenVisual : BaseVisual<LoadingScreen>
    {
        [SerializeField] TextMeshProUGUI proTip;
        [SerializeField] TextMeshProUGUI loadingBarPercentage;
        [SerializeField] Image loadingBar;

        public void SetProTip(string massage)
        {
            proTip.text = $"Pro Tip - {massage}";
        }

        public void UpdateLoadingBarPercentage()
        {
            loadingBarPercentage.text = $"{Mathf.RoundToInt(Feature.Record.LoadingPercentage)}%";
        }

        public void UpdateProgress(float progressNormlized) 
        {
            if(progressNormlized > 1 || progressNormlized < 0)
            {
                Debug.LogError("Enter a float betweem 0 to 1");
            }

            loadingBar.fillAmount = progressNormlized;
            UpdateLoadingBarPercentage();
        }
      
    }
}