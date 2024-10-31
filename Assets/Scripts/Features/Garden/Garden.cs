using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agents;
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

        public Transform CameraStartSpot => _visual.CameraStartSpot;

        public void Dispose()
        {
            _visual.SelfDestroy();
        }

        public async Task Load()
        {
            _config = ConfigService.GetConfig<GardenConfig>();

            await Task.WhenAll(Task.Delay(TimeSpan.FromSeconds(3f)), CreateVisual());
        }

        public Vector3 AvatarStartSpot => _visual.AvatarStartSpot.position;
    }
}