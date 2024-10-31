using System.Threading.Tasks;
using Agents;
using Core;
using Services;

namespace Game
{
    public class Camera : BaseVisualFeature<CameraVisual>, ICamera, IAppLaunchAgent
    {
        [Inject] public CameraRecord Record { get; set; }
        [Inject] public IGarden Garden { get; set; }
        
        public CameraConfig Config { get; set; }


        public Task AppLaunch()
        {
            Config = _bootstrap.Services.Get<ILocalConfigService>().GetConfig<CameraConfig>();
            return Task.CompletedTask;
        }

        public async Task Load()
        {
            await CreateVisual();

            _visual.SetSpot(Garden.CameraStartSpot);
        }
    }
}