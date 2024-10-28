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
        
        public void LoadTools()
        {
            foreach (var tool in _toolsConfig.Tool)
            {
               
            }
        }

        public void GetToolAbilities()
        {
            for (int i = 0; i < _toolsConfig.Tool.Length; i++)
            {
                foreach (var toolAbility in _toolsConfig.Tool[i].ToolAbilities)
                {
                    Record.ToolAction[i] = toolAbility;
                }
            }
        }

        public void GetHoldingTool()
        {
            throw new System.NotImplementedException();
        }

        public void GetClosestTool()
        {
            throw new System.NotImplementedException();
        }

        public void DropTool()
        {
            throw new System.NotImplementedException();
        }

        public void PickUpTool()
        {
            throw new System.NotImplementedException();
        }
    }
}