using System.Threading.Tasks;
using Agents;
using Core;
using Services;
using UnityEngine;

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

        public ToolVisual GetHoldingTool()
        {
            //TODO: Return the Tool that was is being held
            return null;
        }

        public ToolVisual GetClosestTool(Vector3 pos)
        {
            //TODO: Return the Tool that is the Closest to the @pos
            return null;
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