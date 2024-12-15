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
        public ToolsConfig Config { get; private set; }
        public ToolsVisual ToolVisual => _visual;
        public ToolsRecord ToolsRecord => Record;

        public Task AppLaunch()
        {
            Config = ConfigService.GetConfig<ToolsConfig>();
            return Task.CompletedTask;
        }

        public async Task LoadTools()
        {
             await CreateVisual();

             List<ToolVisual> tools = new();

             foreach (var tool in Record.GardenTools)
             {
                 var toolConfig = Config.Tools.FirstOrDefault(t => t.ToolID == tool.Id);
                 var toolVisual = Object.Instantiate(toolConfig.prefab, tool.Pos, Quaternion.Euler(tool.Rot), _visual.transform);
                 toolVisual.SetFeature(this);
                 toolVisual.ToolID = tool.Id;
                 tools.Add(toolVisual);
             }

             _visual.AddTools(tools);
        }

        public void AddGrainBag(TPlant getSeedType)
        {
            var grainBagPrefab = Config.Tools.FirstOrDefault(t => t.GrainBagSeedType == getSeedType).prefab;

            var avatarPos = Avatar.AvatarTransform.position;
            var toolVisual = ToolVisual.transform;

            Vector3 grainBagSpawnPos = avatarPos - Vector3.forward * 3 + Vector3.up * 5;
               
            var grainBag = Object.Instantiate(grainBagPrefab, grainBagSpawnPos , Quaternion.identity);
            grainBag.SetFeature(this);
            grainBag.ToolID = TTools.GrainBag;
            grainBag.transform.SetParent(toolVisual);
            
            _visual.AddTools(new List<ToolVisual>(){grainBag});
            Record.GardenTools.Add(new ToolRecordData
            {
                Id = grainBag.ToolID,
                Pos = grainBagSpawnPos,
                Rot = Vector3.zero
            });
        }

        public ToolVisual GetHoldingTool()
        {
            return Record.EquippedToolVisual;
        }

        public ToolVisual GetClosestPickableTool(Vector3 pos)
        {
            ToolVisual closestTool = null;
            float closestDistance = Mathf.Infinity;
            
            foreach (var tool in _visual.AllTools )
            {
                if (tool is not null && tool.Pickable)
                {
                    float distance = Vector3.Distance(tool.transform.position.XZ(), pos.XZ());
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTool = tool;
                    }
                }
            }
            return closestTool;
        }
        
        public void PickUpTool(ToolVisual closestTool, Transform handTransform)
        {
            if (Record.EquippedToolVisual != null)
            {
                DropTool(Record.EquippedToolVisual);
            }
            
            closestTool.SetHighlight(false);
            Record.EquippedToolVisual = closestTool;
            closestTool.GetPickedUp(handTransform);
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
                if (GetHoldingTool() != tool)
                {
                    tool.SetHighlight(tool == closestTool);
                }
            }
        }
         
        private async Task SaveToolData(ToolVisual tool)
        {
            var gardenTool = Record.GardenTools.FirstOrDefault(t => t.Id == tool.ToolID);
            gardenTool.Pos = tool.transform.position;
            gardenTool.Rot = tool.transform.rotation.eulerAngles;

            await PlayerAccount.SyncPlayerData();
        }
        private void DropTool(ToolVisual tool)
        {
            // Turning on rigidbody for adding drop force

            tool.ToggleRigidBody(true);
            tool.transform.SetParent(_visual.transform);
            tool.DropToolPhysics(Avatar.AvatarTransform, 7);

            Record.EquippedToolVisual = null;
            tool.StartPickupCooldown(2f);

            Avatar.DropTool(tool);
            
            SaveToolData(tool).Forget();
        }

        public async Task ThrowTool(ToolVisual tool, Vector3 dropPoint)
        {
            tool.ToggleRigidBody(true);
            tool.transform.SetParent(_visual.transform);
            tool.ThrowToolPhysics(dropPoint, 7);

            Record.EquippedToolVisual = null;
            tool.StartPickupCooldown(2f);
            
            Avatar.DropTool(tool);
            
            await SaveToolData(tool);
        }
    }
}