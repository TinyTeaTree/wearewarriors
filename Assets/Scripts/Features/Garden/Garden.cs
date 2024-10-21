using System.Threading.Tasks;
using Agents;
using Core;
using Factories;
using Services;

namespace Game
{
    public class Garden : BaseVisualFeature<GardenVisual>, IGarden, IAppLaunchAgent
    {
        [Inject] public GardenRecord Record { get; set; }
        [Inject] public LocalConfigService ConfigService { get; set; }

        private GardenConfig _config;
        
        public async Task AppLaunch()
        {
            _config = ConfigService.GetConfig<GardenConfig>();
            
            await CreateVisual();

            _visual.AddTomatoGarden();
        }
    }
}