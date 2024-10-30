using System.Collections.Generic;
using System.Linq;
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
        public ToolsConfig _toolsConfig { get; private set; }

        public Task AppLaunch()
        {
            _toolsConfig = ConfigService.GetConfig<ToolsConfig>();
            return Task.CompletedTask;
        }

        public async Task LoadTools()
        {
             await CreateVisual();

             foreach (var tool in Record.GardenTools)
             {
                 var toolConfig = _toolsConfig.Tools.FirstOrDefault(t => t.ToolID == tool.Id);
                 var toolVisual = Object.Instantiate(toolConfig.prefab, tool.Pos, Quaternion.Euler(tool.Rot), _visual.transform);
                 Record.AllToolsInGarden.Add(toolVisual);  
             }
             
             _visual.SetToolVisuals(Record.AllToolsInGarden);
        }

        public ToolAction[] GetToolAbilities(ToolsEnum toolType)
        {
            return _toolsConfig.Tools[(int)toolType].ToolAbilities;
        }

        public ToolVisual GetHoldingTool()
        {
            return Record.EquippedToolVisual;
        }

        public ToolVisual GetClosestTool(Vector3 pos)
        {
            ToolVisual closestTool = null;
            float closestDistance = Mathf.Infinity;
            
            foreach (var tool in _visual.AllTools )
            {
                if (tool is not null)
                {
                    float distance = Vector3.Distance(tool.transform.position, pos);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTool = tool;
                    }
                }
            }
            return closestTool;
        }

        public void DropTool(ToolVisual tool)
        {
            throw new System.NotImplementedException();
        }

        public void PickUpTool(ToolVisual closestTool, Transform avatarTransform)
        { 
            // Todo: Avatar pick up tool     
            Record.EquippedToolVisual = closestTool;
            closestTool.transform.SetParent(avatarTransform, true);
            closestTool.SetHighlight(false);
        }

        public void HighlightOff()
        {
            List<ToolVisual> visuals = _visual?.AllTools;
            foreach (var tool in visuals)
            {
                tool.SetHighlight(false);
            }
        }

        public void HighlightOn(ToolVisual closestTool)
        {
            List<ToolVisual> visuals = _visual?.AllTools;
            foreach (var tool in visuals)
            {
                tool.SetHighlight(tool == closestTool);
            }
        }
    }
}