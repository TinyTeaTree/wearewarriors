using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Factories;
using Services;
using UnityEngine;

namespace Game
{
    public class Garden : BaseVisualFeature<GardenVisual>, IGarden
    {
        [Inject] public GardenRecord Record { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }
        [Inject] public IMarks Marks { get; set; }

        private GardenConfig _config;
        public GardenConfig Config => _config;

        public void Dispose()
        {
            _visual.SelfDestroy();
        }

        public void WaterPlant(int fieldId, int plotID, float amount)
        {
            var plotData = GetPlotData(fieldId, plotID);

            if (plotData.State == TPlotState.SeedsGrowing || plotData.State == TPlotState.PlantRiping)
            {
                plotData.Progress = Mathf.MoveTowards(plotData.Progress, 1f, amount);
                
                if (plotData.Progress == 1f)
                {
                    plotData.State = TPlotState.PlantRiping;
                    plotData.Progress = 1f; //Start with full Plants
                }
                
                _visual.FieldVisuals.FirstOrDefault(f => f.FieldId == fieldId).GardenPlotVisuals.FirstOrDefault(p => p.PlotID == plotID).WaterPlant(plotData);
            }
        }

        public void RakePlot(int fieldId, int plotID, float amount)
        {
            var plotData = GetPlotData(fieldId, plotID);
            
            if (plotData.State is TPlotState.Empty or TPlotState.Weeds)
            {
                plotData.Progress = Mathf.MoveTowards(plotData.Progress, 1f, amount);

                if (plotData.Progress == 1f)
                {
                    plotData.State = TPlotState.Raked;
                    plotData.Progress = 0f;
                }
                _visual.FieldVisuals.FirstOrDefault(f => f.FieldId == fieldId).GardenPlotVisuals.FirstOrDefault(p => p.PlotID == plotID).RakePlot(plotData);
            }
        }

        public void SeedPlot(int fieldId, int plotID, float amount, TPlant holdingToolSeedType)
        {
            var plotData = GetPlotData(fieldId, plotID);

            if (plotData.State is TPlotState.Raked)
            {
                plotData.Progress = Mathf.MoveTowards(plotData.Progress, 1f, amount);

                if (plotData.Progress >= 1f)
                {
                    plotData.State = TPlotState.SeedsGrowing;
                    plotData.Progress = 0f;
                    plotData.Plant = holdingToolSeedType;
                    _visual.FieldVisuals.FirstOrDefault(f => f.FieldId == fieldId).GardenPlotVisuals.FirstOrDefault(p => p.PlotID == plotID).PlantSeed(plotData);
                }
            }
        }

        public async Task Load()
        {
            _config = ConfigService.GetConfig<GardenConfig>();
            
            await Task.WhenAll(Task.Delay(TimeSpan.FromSeconds(1f)), CreateVisual());
            
            _visual.LoadPlotFieldVisuals();
        }

        public FieldData GetFieldData(int fieldId)
        {
            var fieldData = Record.Fields.FirstOrDefault(f => f.Id == fieldId);

            if (fieldData == null)
            {
                Notebook.NoteError($"There is no data for a field visual {fieldId}, may be problems");
            }

            return fieldData;
        }

        public PlotData GetPlotData(int fieldId, int plotId)
        {
            var fieldData = GetFieldData(fieldId);

            if (fieldData != null)
            {
                var plotData = fieldData.Plots.FirstOrDefault(p => p.Id == plotId);
                
                if (plotData == null)
                {
                    Notebook.NoteError($"There is no data for a plot visual {plotId}, may be problems");
                }

                return plotData;
            }

            return null;
        }
    }
}