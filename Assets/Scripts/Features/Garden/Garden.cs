using System;
using System.Threading.Tasks;
using Core;
using Factories;
using Services;
using UnityEngine;

namespace Game
{
    public class Garden : BaseVisualFeature<GardenVisual>, IGarden
    {
        [Inject] public GardenRecord Record { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }

        private GardenConfig _config;

        public Vector3 AvatarStartSpot => _visual.AvatarStartSpot.position;
        public Transform CameraStartSpot => _visual.CameraStartSpot;
        public GardenConfig Config => _config;

        public void Dispose()
        {
            _visual.SelfDestroy();
        }

        public async Task Load()
        {
            _config = ConfigService.GetConfig<GardenConfig>();
            
            await Task.WhenAll(Task.Delay(TimeSpan.FromSeconds(3f)), CreateVisual());
            
            //Todo: load plot field visual, plot visual and plant visual.
        }

    }
}