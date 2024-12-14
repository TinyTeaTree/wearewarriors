using System.Linq;
using Core;
using UnityEngine;

namespace Game
{
    public class GardenPlotVisual : BaseVisual<Garden>
    {
        [SerializeField] private int plotID;
        [SerializeField] private GameObject Rows;
        [SerializeField] private PlotFieldVisual Field;
        
        private PlantVisual _plantVisual = null;
        
        public int PlotID => plotID;
        public PlantVisual PlantVisual => _plantVisual;
        public int FieldId => Field.FieldId;

        public void LoadPlantVisuals(PlotData data)
        {
            if (data.State == TPlotState.Empty)
            {
                Rows.gameObject.SetActive(false);
                return;    
            }
            else if (data.State == TPlotState.Raked)
            {
                Rows.gameObject.SetActive(true);
                return;
            }
            else if (data.State == TPlotState.SeedsGrowing)
            {
                CreatePlantVisual(data.Plant);
                _plantVisual.SetUp(data);
            }
            else if (data.State == TPlotState.PlantRiping)
            {
                CreatePlantVisual(data.Plant);
                _plantVisual.SetUp(data);
            }
        }
        
        public void WaterPlant(PlotData data)
        {
            _plantVisual.WaterPlant(data);
        }

        private void CreatePlantVisual(TPlant plantType)
        {
            var plantVisual = Feature.Config.plantVisuals.FirstOrDefault(v => v.PlantID == plantType);

            if(plantVisual != null)
            {
                _plantVisual = Instantiate(plantVisual, transform);
                _plantVisual.SetFeature(Feature);
            }
        }

        public void PlantSeed(PlotData plotData)
        {
            var plantPrefab = Feature.Config.plantVisuals.FirstOrDefault(v => v.PlantID == plotData.Plant);

            _plantVisual = Instantiate(plantPrefab, transform);

            _plantVisual.SetFeature(Feature);
            _plantVisual.MarkID = Feature.Marks.AddMark(transform, TMark.PlantProgress);

            _plantVisual.StartGrowing();
        }

        public void RakePlot(PlotData plotData)
        {
            if (plotData.State == TPlotState.Empty)
            {
                Rows.gameObject.SetActive(false);
            }
            else
            {
                Rows.gameObject.SetActive(true);
            }
        }

        public void ShakePlant()
        {
            _plantVisual.Shake();
        }
    }
}