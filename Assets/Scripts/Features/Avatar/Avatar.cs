using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agents;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class Avatar : BaseVisualFeature<AvatarVisual>, IAvatar, IAppExitAgent
    {
        [Inject] public AvatarRecord Record { get; set; }
        [Inject] public IJoystick Joystick { get; set; }
        [Inject] public ITools Tools { get; set; }
        [Inject] public IPlayerAccount PlayerAccount { get; set; }

        public Transform AvatarTransform => _visual.transform;
        private AvatarConfig Config { get; set; }
        
        public async Task Load()
        {
            await CreateVisual();

            _visual.SetPos(Record.AvatarRecordData.Pos);

            Config = _bootstrap.Services.Get<ILocalConfigService>().GetConfig<AvatarConfig>();
        }

        public void Activate()
        {
            _visual.SetDirectionProvider(Joystick);
            
            _visual.StartMovement();
        }
        
        public void Update()
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
                        }
                    }
                }
            }
            
        }

        public void AppExit()
        {
            Record.AvatarRecordData.Pos = _visual.transform.position;
            PlayerAccount.SyncPlayerData();
        }

        public void ProcessMove()
        {
            if (Record.IsWorking)
            {
                _visual.AnimateTool(Record.ToolWorking, false);
                Record.IsWorking = false;
            }
            
            if (!Record.IsMoving)
            {
                Record.IsMoving = true;
                _visual.PlayWalkSounds();
            }
        }
        
        public void UpdateIdle()
        {
            if (Record.IsMoving)
            {
                Record.IsMoving = false;
                _visual.StopWalkSounds();
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
            var gardenPlotVisual = _visual.TryGetPlot();
            var holdingTool = Tools.GetHoldingTool();
            var seedPool = _visual.TryGetSeed();
            
            CheckRakeWork(holdingTool, gardenPlotVisual);
            
            CheckGrainBagWork(holdingTool, gardenPlotVisual, seedPool);
        }
        private void StartWorking(ToolVisual holdingTool)
        {
            _visual.AnimateTool(holdingTool.ToolID, true);
            Record.IsWorking = true;
            Record.ToolWorking = holdingTool.ToolID;
            Record.WorkTime = Time.time;
        }
        private void CheckProgressWork()
        {
            if (!(Record.WorkTime + 1f < Time.time)) 
                return;
            
            var gardenPlotVisual = _visual.TryGetPlot();
            var seedPool = _visual.TryGetSeed();
            var holdingTool = Tools.GetHoldingTool();
            
            Record.WorkTime = Time.time;

            CheckRakeProgress(holdingTool, gardenPlotVisual);
                    
            CheckGrainBagProgress(holdingTool, seedPool, gardenPlotVisual);
        }
        

        private void CheckGrainBagProgress(ToolVisual holdingTool, GardenSeedPoolVisual seedPool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID == TTools.GrainBag)
            {
                if (seedPool != null)
                {
                    holdingTool.SeedType = seedPool.SeedPoolType;
                    _visual.AnimateTool(holdingTool.ToolID, false);
                }
                        
                if (gardenPlotVisual != null)
                {
                            
                    if (gardenPlotVisual.PlantVisual == null)
                    {
                        gardenPlotVisual.PlantSeed(holdingTool.SeedType);
                        _visual.AnimateTool(holdingTool.ToolID, false);
                    }
                }
            }
        }

        private void CheckRakeProgress(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual)
        {
            if (holdingTool.ToolID == TTools.Rake)
            {
                if (gardenPlotVisual != null)
                {
                    if (gardenPlotVisual.PlantVisual.IsComplete)
                    {
                        _visual.AnimateTool(holdingTool.ToolID, false);
                        return;
                    }
                            
                    if (gardenPlotVisual.PlantType == TPlant.None)
                    {
                        return;
                    }
                            
                    gardenPlotVisual.PlantVisual.WaterPlant(.5f);
                }
            }
        }
        
        private void CheckGrainBagWork(ToolVisual holdingTool, GardenPlotVisual gardenPlotVisual, GardenSeedPoolVisual seedPool)
        {
            if (holdingTool == null || holdingTool.ToolID != TTools.GrainBag) 
                return;
            
            if (gardenPlotVisual != null)
            {
                if (gardenPlotVisual.PlantType == TPlant.None && holdingTool.SeedType != TPlant.None)
                {
                    StartWorking(holdingTool);
                }
            }

            CheckSeedPoolWork(holdingTool, seedPool);
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
            if (holdingTool?.ToolID != TTools.Rake) 
                return;
            if (gardenPlotVisual?.PlantVisual == null) 
                return;
            
            if (gardenPlotVisual.PlantVisual.IsComplete == false)
            {
                StartWorking(holdingTool);
            }
        }

       
    }
}