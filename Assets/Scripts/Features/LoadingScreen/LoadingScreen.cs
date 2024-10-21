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
            Debug.Log("App Has Lanched");
            return Task.CompletedTask;
        }

        public void Close()
        {
            Object.Destroy(Record.loadingScreenVisual);
        }

        public void Show()
        {
            var canvas = GameObject.Find("Canvas").transform;
            var resource = Resources.Load<LoadingScreenVisual>(Addresses.LoadingScreen);

            Record.loadingScreenVisual = Object.Instantiate(resource,canvas);
            Debug.Log("Show Called");

            Record.loadingScreenVisual.RotatingLoadingImage();
        }
    }
}