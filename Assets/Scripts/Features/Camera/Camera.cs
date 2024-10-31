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
        [Inject] public IGarden Garden { get; set; }
        [Inject] public IAvatar Avatar { get; set; }
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
            _visual.SetTarget(Avatar.AvatarTransform);
        }

        public void LookAt(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void Follow(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void StopFollowing()
        {
            throw new System.NotImplementedException();
        }

        public void ActivateCameraAnimation()
        {
            throw new System.NotImplementedException();
        }
    }
}