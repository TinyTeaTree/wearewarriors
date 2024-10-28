using System.Threading.Tasks;
using Agents;
using Core;
using Services;

namespace Game
{
    public class Tools : BaseVisualFeature<ToolsVisual>, ITools, IAppLaunchAgent
    {
        [Inject] public ToolsRecord Record { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }
        
        private ToolsConfig _toolsConfig;
        
        public Task AppLaunch()
        {
            _toolsConfig = ConfigService.GetConfig<ToolsConfig>();
            return Task.CompletedTask;
        }

        public Task LoadTools()
        {
           return Task.CompletedTask;
        }

        public ToolAction GetToolAbilities()
        {
            throw new System.NotImplementedException();
        }

        public ToolVisual GetHoldingTool()
        {
            throw new System.NotImplementedException();
        }

        public ToolVisual GetClosestTool()
        {
            throw new System.NotImplementedException();
        }

        public void DropTool(ToolVisual tool)
        {
            throw new System.NotImplementedException();
        }

        public void PickUpTool(ToolVisual tool)
        {
            throw new System.NotImplementedException();
        }
    }
}