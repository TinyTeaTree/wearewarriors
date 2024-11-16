using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agents;
using Core;
using Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class Tools : BaseVisualFeature<ToolsVisual>, ITools, IAppLaunchAgent
    {
        [Inject] public ToolsRecord Record { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }
        [Inject] public IAvatar Avatar { get; set; }
        [Inject] public IPlayerAccount PlayerAccount { get; set; }
        public ToolsConfig _toolsConfig { get; private set; }

        
        

        /// ///////////////////////////// DELETE ME
        /// </summary>
        [Inject] public IMarks Marks { get; set; }

        public async Task Do()
        {
            await Task.Delay(2000);

            Marks.AddMark(_visual.AllTools.FirstOrDefault().transform, TMark.Example, "123");
        }
        
        /// <summary>
        /// //////////////////////////////////////DELETE ME
        
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
                 toolVisual.ToolID = tool.Id;
                 Record.AllToolsInGarden.Add(toolVisual);
             }

             _visual.SetToolVisuals(Record.AllToolsInGarden);
             _visual.LoadDropButton();

             Do().Forget(); //TODO: Kill Me
        }

        public ToolAction[] GetToolAbilities(TTools tool)
        {
            return _toolsConfig.Tools[(int)tool].ToolAbilities;
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

        public async Task DropTool(ToolVisual tool)
        {
            // Turning on rigidbody for adding drop force

            tool.ToggleRigidBody(true);
            tool.transform.SetParent(_visual.transform);
            tool.DropToolPhysics(Avatar.AvatarTransform, 7);

            _visual.ToggleDropButton(false);

            await Task.Delay(TimeSpan.FromSeconds(1f));

            Record.EquippedToolVisual = null;
            var gardenTool = Record.GardenTools.FirstOrDefault(t => t.Id == tool.ToolID);
            gardenTool.Pos = tool.transform.position;
            gardenTool.Rot = tool.transform.rotation.eulerAngles;

            await PlayerAccount.SyncPlayerData();

        }

        public void PickUpTool(ToolVisual closestTool, Transform handTransform)
        {
            Record.EquippedToolVisual = closestTool;

            closestTool.GetPickedUp(handTransform);
            
            _visual.ToggleDropButton(true);
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