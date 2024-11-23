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
        [Inject] public IMarks Marks { get; set; }

        private GardenConfig _config;
        public GardenConfig Config => _config;

        public void Dispose()
        {
            _visual.SelfDestroy();
        }

        public async Task Load()
        {
            _config = ConfigService.GetConfig<GardenConfig>();
            
            await Task.WhenAll(Task.Delay(TimeSpan.FromSeconds(1f)), CreateVisual());
            
            _visual.LoadPlotFieldVisuals();
        }
    }
}