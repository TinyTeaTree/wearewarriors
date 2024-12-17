using Agents;
using Core;
using Services;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class LoadingScreen : BaseVisualFeature<LoadingScreenVisual>, ILoadingScreen, IAppLaunchAgent
    {
        [Inject] public LoadingScreenRecord Record { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }
        
        [Inject] public IHud Hud { get; set; }

        private LoadingScreenConfig _config;

        public Task AppLaunch()
        {
            _config = ConfigService.GetConfig<LoadingScreenConfig>();
            return Task.CompletedTask;
        }

        public void Close()
        {
            _visual.SelfDestroy();
            Record.IsShowing = false;
        }

        public void ProgressControl(float progress)
        {
            Record.LoadingPercentage = progress * 100;
            _visual.UpdateProgress(progress);
        }

        public async void Show(bool toggleTips)
        {
            string randomTip = _config.proTips.GetRandom();

            await CreateVisual();
            
            Hud.SetHudOnCanvas(_visual.Canvas);

            Record.IsShowing = true;

            _visual.InitLoadingScreen(toggleTips, randomTip);
        }
    }
}