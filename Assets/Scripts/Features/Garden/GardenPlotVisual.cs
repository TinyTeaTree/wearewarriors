using System.Linq;
using Core;
using UnityEngine;

namespace Game
{
    public class GardenPlotVisual : BaseVisual<Garden>
    {
        [SerializeField] private int plotID;
        [SerializeField] private TPlant plantType;
        
        public int PlotID => plotID;
        public TPlant PlantType => plantType;

        private PlantVisual _plantVisual;
        public PlantVisual PlantVisual => _plantVisual;
        
        //Todo: Get plot visual

        public void LoadPlantVisuals()
        {
            var plantVisual = Feature.Config.plantVisuals.FirstOrDefault(v => v.PlantID == plantType);

            if(plantVisual != null)
            {
               _plantVisual = Instantiate(plantVisual, transform);
            }
        }
        
    }
}