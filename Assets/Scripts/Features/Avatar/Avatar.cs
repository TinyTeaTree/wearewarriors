using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agents;
using Core;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class Avatar : BaseVisualFeature<AvatarVisual>, IAvatar, IAppExitAgent
    {
        [Inject] public AvatarRecord Record { get; set; }
        [Inject] public IJoystick Joystick { get; set; }
        [Inject] public ITools Tools { get; set; }
        [Inject] public IPlayerAccount PlayerAccount { get; set; }
        [Inject] public IGarden Garden { get; set; }
        [Inject] public IWorld World { get; set; }
        [Inject] public ICoins Coins { get; set; }

        public Transform AvatarTransform => _visual?.transform;
        private AvatarConfig Config { get; set; }
        
        public async Task Load()
        {
            await CreateVisual();

            _visual.SetPos(Record.AvatarRecordData.Pos);
            _visual.SetRot(Record.AvatarRecordData.Rot);

            Config = _bootstrap.Services.Get<ILocalConfigService>().GetConfig<AvatarConfig>();
        }

        public void Activate()
        {
            _visual.SetDirectionProvider(Joystick);
            
            _visual.StartMovement(World);
        }

        public void Update()
        {
            ScanForTool();
        }

        private void ScanForTool()
        {
            var closestTool = Tools.GetClosestTool(_visual.transform.position);
            if (closestTool != null)
            {
                float distance = Vector3.Distance(closestTool.transform.position, _visual.transform.position);
                if (distance > Config.HighlightDistance)
                {
                    Tools.HighlightOff();
                }
                else
                {
                    Tools.HighlightOn(closestTool);
                }

                if (distance < Config.PickupDistance)
                {
                    foreach (var anchor in _visual.AvatarAnchors)
                    {
                        if (closestTool.ToolID == anchor.toolID)
                        {
                            Tools.PickUpTool(closestTool, anchor.anchorPoint);
                            closestTool.Pickable = false;
                        }
                    }
                }
            }
        }

        public void AppExit()
        {
            Record.AvatarRecordData.Pos = _visual.transform.position;
            Record.AvatarRecordData.Rot = _visual.transform.rotation.eulerAngles.y;
            PlayerAccount.SyncPlayerData();
        }

        public void ProcessMove()
        {
            if (Record.IsWorking)
            {
                _visual.AnimateTool(Record.ToolWorking, false);
                var holdingTool = Tools.GetHoldingTool();
                if (holdingTool != null)
                {
                    holdingTool.EndWorking();
                }
                Record.IsWorking = false;
            }
            
            if (!Record.IsMoving)
            {
                Record.IsMoving = true;
            }
        }
        
        public void UpdateIdle()
        {
            if (Record.IsMoving)
            {
                Record.IsMoving = false;
            }
            
            if (!Record.IsWorking)
            {
                CheckStartWorking();
            }
            else
            {
                CheckProgressWork();
            }
        }

       
        private void CheckStartWorking()
        {
            var holdingTool = Tools.GetHoldingTool();
            if (holdingTool == null)
            {
                return;
            }
            
            var gardenPlotVisual = _visual.TryGetPlot();
            var seedPool = _visual.TryGetSeed();
            
            CheckRakeWork(holdingTool, gardenPlotVisual);
            
            CheckGrainBagWork(holdingTool, gardenPlotVisual, seedPool);

            CheckWaterWork(holdingTool, gardenPlotVisual);
        }
        private void StartWorking(ToolVisual holdingTool)
        {
            _visual.AnimateTool(holdingTool.ToolID, true);
            holdingTool.StartWorking();
            Record.IsWorking = true;
            Record.ToolWorking = holdingTool.ToolID;
            Record.WorkTime = Time.time;
        }
        private void CheckProgressWork()
        {
            if (!(Record.WorkTime + 1f < Time.time)) 
                return;
            
            var holdingTool = Tools.GetHoldingTool();
            if (holdingTool == null)
                return;
            
            var gardenPlotVisual = _visual.TryGetPlot();
            var seedPool = _visual.TryGetSeed();
            
            Record.WorkTime = Time.time;

            CheckRakeProgress(holdingTool, gardenPlotVisual);

            CheckWaterProgress(holdingTool, gardenPlotVisual);
                    
            CheckGrainBagProgress(holdingTool, seedPool, gardenPlotVisual);
        }

        private void CheckWaterProgress(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.WateringCan) 
                return;
            if (gardenPlotVisual == null) 
                return;

            Garden.WaterPlant(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID, holdingTool.WorkPerSecond);
        }

        private void CheckGrainBagProgress(ToolVisual holdingTool, GardenSeedPoolVisual seedPool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.GrainBag) 
                return;
            if (seedPool != null)
            {
                holdingTool.SeedType = seedPool.SeedPoolType;
                _visual.AnimateTool(holdingTool.ToolID, false);
            }

            if (gardenPlotVisual == null) 
                return;
            
            var plotData = Garden.GetPlotData(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID);
            if (plotData.State == TPlotState.Raked)
            {
                Garden.SeedPlot(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID, holdingTool.WorkPerSecond, holdingTool.SeedType);
            }
            else
            {
                _visual.AnimateTool(holdingTool.ToolID, false);
            }
        }

        private void CheckRakeProgress(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.Rake) 
                return;
            if (gardenPlotVisual == null) 
                return;
  
            var plotData = Garden.GetPlotData(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID);
            
            if (plotData.State is TPlotState.Empty or TPlotState.Weeds)
            {
                Garden.RakePlot(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID, holdingTool.WorkPerSecond);
            }
            else
            {
                _visual.AnimateTool(holdingTool.ToolID, false);
            }
        }
        
        private void CheckGrainBagWork(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual, GardenSeedPoolVisual seedPool)
        {
            if (holdingTool.ToolID != TTools.GrainBag) 
                return;
            
            if (gardenPlotVisual != null)
            {
                if (holdingTool.SeedType != TPlant.None)
                {
                    var plotData = Garden.GetPlotData(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID);
                    if (plotData.State == TPlotState.Raked)
                    {
                        StartWorking(holdingTool);
                    }
                }
            }
            else
            {
                CheckSeedPoolWork(holdingTool, seedPool);
            }
        }
        
        private void CheckSeedPoolWork(ToolVisual holdingTool, GardenSeedPoolVisual seedPool)
        {
            if (seedPool == null) 
                return;
            
            if (seedPool.SeedPoolType != holdingTool.SeedType)
            {
                StartWorking(holdingTool);
            }
        }

        private void CheckRakeWork(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.Rake) 
                return;
            if (gardenPlotVisual == null)
                return;
            
            var plotData = Garden.GetPlotData(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID);
            if (plotData.State is TPlotState.Empty or TPlotState.Weeds)
            {
                StartWorking(holdingTool);
            }
        }
        
        private void CheckWaterWork(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.WateringCan) 
                return;
            
            if (gardenPlotVisual== null) 
                return;

            StartWorking(holdingTool);
        }
    }
}