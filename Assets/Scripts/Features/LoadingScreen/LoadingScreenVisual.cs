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
        [SerializeField] private Canvas _canvas;

        public Canvas Canvas => _canvas;

        public void InitLoadingScreen(bool toggleTip, string message)
        {
            
            SetProTip(message);
            proTip.gameObject.SetActive(toggleTip);

            if (toggleTip)
            {
                StartCoroutine(TipTimer());
            }

            loadingBarPercentage.text = "0%";
            Feature.Record.LoadingPercentage = 0;
        }

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
                Notebook.NoteError("Unsupported float, insert a number between 0 to 1");
            }

            loadingBar.fillAmount = progressNormlized;
            UpdateLoadingBarPercentage();
        }

        IEnumerator TipTimer()
        {
            while (Feature.Record.IsShowing)
            {
                Feature.Record.TipDisplayDuration += Time.deltaTime;
                yield return null;
            }
        }
    }
}