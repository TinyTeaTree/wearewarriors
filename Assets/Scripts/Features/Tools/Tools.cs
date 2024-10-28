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

        public Task LoadTools()
        {
           return Task.CompletedTask;
        }

        public ToolAction GetToolAbilities()
        {
            return ToolAction.filling; //TODO: get tool abilitties
        }

        public ToolVisual GetHoldingTool()
        {
            //TODO: Return the Tool that was is being held
            return null;
        }

        public ToolVisual GetClosestTool(Vector3 pos)
        {
            //TODO: Return the Tool that is the Closest to the @pos
            return Object.FindObjectOfType<ToolVisual>(); //TMP: Remove once _visual is set up
        }

        public void DropTool(ToolVisual tool)
        {
            throw new System.NotImplementedException();
        }

        public void PickUpTool(ToolVisual tool)
        {
            throw new System.NotImplementedException();
        }

        public void HighlightOff()
        {
            ToolVisual[] visuals = _visual?.AllTools ?? Object.FindObjectsByType<ToolVisual>(FindObjectsSortMode.None); //TMP: Remove Find Objects once Visual has bee set up
            foreach (var tool in visuals)
            {
                tool.SetHighlight(false);
            }
        }

        public void HighlightOn(ToolVisual closestTool)
        {
            ToolVisual[] visuals = _visual?.AllTools ?? Object.FindObjectsByType<ToolVisual>(FindObjectsSortMode.None); //TMP: Remove Find Objects once Visual has bee set up
            foreach (var tool in visuals)
            {
                tool.SetHighlight(tool == closestTool);
            }
        }
    }
}