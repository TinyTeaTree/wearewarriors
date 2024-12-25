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
        [Inject] public IShop Shop { get; set; }
        [Inject] public IWorld World { get; set; }
        [Inject] public ICoins Coins { get; set; }

        public List<TPlant> PlantsGathered => Record.PlantsGathered;

        public Transform AvatarTransform => _visual?.transform;
        private AvatarConfig Config { get; set; }
        
        public async Task Load()
        {
            await CreateVisual();

            _visual.SetPos(Record.AvatarRecordData.Pos);
            _visual.SetRot(Record.AvatarRecordData.Rot);

            Config = _bootstrap.Services.Get<ILocalConfigService>().GetConfig<AvatarConfig>();
            
            Record.GatherProgress = 0;
            Record.PlantsGathered.Clear();
        }

        public void DropTool(ToolVisual tool)
        {
            _visual.DropTool();
            Record.GatherProgress = 0;
            Record.PlantsGathered.Clear();
        }

        public void SellPlants(Transform sellPoint)
        {
            Record.PlantsGathered.Clear();
            Record.GatherProgress = 0f;

            var box = Tools.GetHoldingTool() as CropBoxVisual;
            var plants = box.Plants;
            foreach (var plant in plants)
            {
                plant.transform.SetParent(sellPoint);
                plant.transform.position = sellPoint.position;
            }
            
            box.Plants.Clear();
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
            Tools.HighlightOff();
            var closestTool = Tools.GetClosestPickableTool(_visual.transform.position);
            if (closestTool != null)
            {
                float distance = Vector3.Distance(closestTool.transform.position, _visual.transform.position);
                if (distance < Config.PickupDistance)
                {
                    Tools.HighlightOn(closestTool);
                    
                    if (Joystick.Direction.magnitude >= 0.8f && Tools.GetHoldingTool() != null)
                    {
                        return; //Don't pick up tools if you are in full speed
                    }

                    if (Tools.GetHoldingTool() == null || Tools.GetHoldingTool().Droppable)
                    {
                        foreach (var anchor in _visual.AvatarAnchors)
                        {
                            if (closestTool.ToolID == anchor.toolID)
                            {
                                Tools.PickUpTool(closestTool, anchor.anchorPoint);
                                _visual.SetTool(closestTool.ToolID);
                            }
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
            
            CheckRakeWork(holdingTool, gardenPlotVisual);
            
            CheckGrainBagWork(holdingTool, gardenPlotVisual);

            CheckWaterWork(holdingTool, gardenPlotVisual);

            CheckPickWork(holdingTool, gardenPlotVisual);
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
            
            Record.WorkTime = Time.time;

            CheckRakeProgress(holdingTool, gardenPlotVisual);

            CheckWaterProgress(holdingTool, gardenPlotVisual);
                    
            CheckGrainBagProgress(holdingTool, gardenPlotVisual);
            
            CheckBoxProgress(holdingTool, gardenPlotVisual);
        }

        private void CheckWaterProgress(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.WateringCan) 
                return;
            if (gardenPlotVisual == null) 
                return;

            Garden.WaterPlant(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID, holdingTool.WorkPerSecond);
        }
        
        private void CheckBoxProgress(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.CropBox) 
                return;
            if (gardenPlotVisual == null) 
                return;

            var plotData = Garden.GetPlotData(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID);
            
            if(plotData.State == TPlotState.PlantRiping)
            {
                gardenPlotVisual.ShakePlant();
            }

            if (plotData.Progress > 0.01f)
            {
                var gatherCapacity = 1f - Record.GatherProgress;
                var cropYield = Mathf.Min(plotData.Progress, holdingTool.WorkPerSecond, gatherCapacity + 0.001f);
                
                plotData.Progress -= cropYield;
                

                var cropsGathered = CalculateCropGathered(Record.GatherProgress, Record.GatherProgress + cropYield);
                
                Record.GatherProgress += cropYield;

                for (int i = 0; i < cropsGathered; ++i)
                {
                    Record.PlantsGathered.Add(gardenPlotVisual.PlantVisual.PlantID);
                    gardenPlotVisual.PlantVisual.AnimateGather(holdingTool);
                    gardenPlotVisual.PlantVisual.SetPlantProgress(plotData);
                }
            }
        }

        private int CalculateCropGathered(float progressBefore, float progressAfter)
        {
            int gathered = 0;
            if (progressBefore <= 0.1f && progressAfter > 0.1f)
            {
                gathered++;
            }
            if (progressBefore <= 0.2f && progressAfter > 0.2f)
            {
                gathered++;
            }
            if (progressBefore <= 0.3f && progressAfter > 0.3f)
            {
                gathered++;
            }
            if (progressBefore <= 0.4f && progressAfter > 0.4f)
            {
                gathered++;
            }
            if (progressBefore <= 0.5f && progressAfter > 0.5f)
            {
                gathered++;
            }
            if (progressBefore <= 0.6f && progressAfter > 0.6f)
            {
                gathered++;
            }
            if (progressBefore <= 0.7f && progressAfter > 0.7f)
            {
                gathered++;
            }
            if (progressBefore <= 0.8f && progressAfter > 0.8f)
            {
                gathered++;
            }
            if (progressBefore <= 0.9f && progressAfter > 0.9f)
            {
                gathered++;
            }
            if (progressBefore < 1f && Mathf.Approximately(progressAfter, 1f))
            {
                gathered++;
            }

            return gathered;
        }

        private void CheckGrainBagProgress(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.GrainBagCorn) 
                return;
            if (gardenPlotVisual == null) 
                return;

            if (holdingTool is GrainBagVisual grainBagVisual)
            {
                if (grainBagVisual.SeedsVolume > 0)
                {
                    var plotData = Garden.GetPlotData(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID);
                    if (plotData.State == TPlotState.Raked && plotData.Plant == TPlant.None)
                    {
                        Garden.SeedPlot(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID, holdingTool.WorkPerSecond, holdingTool.SeedType);
                        //**Todo: Fix a bug that take multiple seeds at once if the WorkPerSecond is less than 1.
                        grainBagVisual.SeedsVolume--;
                    }
                    else
                    {
                        _visual.AnimateTool(holdingTool.ToolID, false);
                    }
                }
                else
                {
                    Tools.DestroyGrainBagTool(holdingTool);
                    _visual.AnimateTool(holdingTool.ToolID, false);
                }
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
        
        private void CheckGrainBagWork(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.GrainBagCorn) 
                return;
            
            if (gardenPlotVisual != null)
            {
                if (holdingTool.SeedType != TPlant.None)
                {
                    var plotData = Garden.GetPlotData(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID);
                    if (plotData.State == TPlotState.Raked && plotData.Plant == TPlant.None)
                    {
                        StartWorking(holdingTool);
                    }
                }
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
        
        private void CheckPickWork(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID != TTools.CropBox) 
                return;
            
            if (gardenPlotVisual== null) 
                return;

            var plotData = Garden.GetPlotData(gardenPlotVisual.FieldId, gardenPlotVisual.PlotID);
            
            if(plotData.State == TPlotState.PlantRiping)
            {
                StartWorking(holdingTool);
            }
        }
    }
}