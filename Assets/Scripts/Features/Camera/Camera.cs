using System.Threading.Tasks;
using Agents;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class Camera : BaseVisualFeature<CameraVisual>, ICamera, IAppLaunchAgent
    {
        [Inject] public CameraRecord Record { get; set; }
        [Inject] public IWorld World { get; set; }
        [Inject] public IAvatar Avatar { get; set; }
        public CameraConfig Config { get; private set; }

        public Task AppLaunch()
        {
            Config = _bootstrap.Services.Get<ILocalConfigService>().GetConfig<CameraConfig>();
            return Task.CompletedTask;
        }

        public UnityEngine.Camera WorldCamera => _visual.Camera;

        public async Task Load()
        {
            await CreateVisual();

            _visual.enabled = false;
        }

        public void Start()
        {
            _visual.enabled = true;
            
            _visual.SetSpot(World.CameraStartSpot);
            _visual.SetTarget(Avatar.AvatarTransform);
            
            Record.Target = Avatar.AvatarTransform.gameObject;
        }

        public void ActivateAnimation()
        {
            _visual.ActivateAnimator();
        }
    }
}