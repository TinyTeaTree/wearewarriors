using Agents;
using Core;
using System.Threading.Tasks;
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
            Record.loadingScreenVisual = Object.Instantiate(Resources.Load("Loading Screen"), GameObject.Find("Canvas").transform) as GameObject;
            Debug.Log("Show Called");
        }
    }
}