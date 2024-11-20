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
        [Inject] public IGarden Garden { get; set; }
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
            if (Tools.GetHoldingTool() != null)
                return;
            
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
        }
        
        public void ProcessIdle()
        {
            if (!Record.IsWorking)
            {
                if (Tools.GetHoldingTool() != null && Tools.GetHoldingTool().ToolID == TTools.Rake)
                {
                    _visual.AnimateTool(Tools.GetHoldingTool().ToolID, true);
                    Record.IsWorking = true;
                    Record.ToolWorking = Tools.GetHoldingTool().ToolID;
                    Record.WorkTime = Time.time;
                }
            }
            else
            {
                if (Record.WorkTime + 1f < Time.time)
                {
                    Record.WorkTime = Time.time;
                    
                    //TODO: Connect Garden under Raycast
                    var gardenPlotVisual = _visual.DetectPlot();

                    if (gardenPlotVisual != null)
                    {
                       gardenPlotVisual.PlantVisual.WaterPlant(0.5f);
                    }
                }
            }
        }
    }
}