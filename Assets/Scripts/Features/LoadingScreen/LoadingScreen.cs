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
            var canvas = GameObject.Find("Canvas").transform;
            var resource = Resources.Load<LoadingScreenVisual>(Addresses.LoadingScreen);

            Record.loadingScreenVisual = Object.Instantiate(resource,canvas);
            Record.loadingScreenVisual.SetFeature(this);
            Record.IsShowing = true;
            Debug.Log("Show Called");
        }
    }
}