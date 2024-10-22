using Agents;
using Core;
using System.Threading.Tasks;
using Services;
using UnityEngine;

namespace Game
{
    public class LoadingScreen : BaseVisualFeature<LoadingScreenVisual>, ILoadingScreen, IAppLaunchAgent
    {
        [Inject] public LoadingScreenRecord Record { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }

        private LoadingScreenConfig _config;

        public Task AppLaunch()
        {
            Debug.Log("Loading Screen Has Lanched");
            return Task.CompletedTask;
        }

        public void Close()
        {
            Record.loadingScreenVisual.SelfDestroy();
            Record.IsShowing = false;
        }

        public void ProgressControl(float progress)
        {
            Record.LoadingPercentage = progress * 100;
            Record.loadingScreenVisual.UpdateProgress(progress);
        }

        public void Show(bool toggleTips)
        {
            _config = ConfigService.GetConfig<LoadingScreenConfig>();
            string randomTip = _config.proTips[Random.Range(0, _config.proTips.Count)];

            Transform canvas = GameObject.Find("Canvas").transform;
            LoadingScreenVisual resource = Resources.Load<LoadingScreenVisual>(Addresses.LoadingScreen);

            Record.loadingScreenVisual = Object.Instantiate(resource,canvas);
            Record.loadingScreenVisual.SetFeature(this);
            Record.IsShowing = true;

            Record.loadingScreenVisual.InitLoadingScreen(toggleTips, randomTip);

            Debug.Log("Show Called");
        }
    }
}