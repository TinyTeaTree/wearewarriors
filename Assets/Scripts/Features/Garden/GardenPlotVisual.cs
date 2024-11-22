using System.Linq;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class GardenPlotVisual : BaseVisual<Garden>
    {
        [SerializeField] private int plotID;
        [SerializeField] private TPlant plantType;
        
        public int PlotID => plotID;
        public TPlant PlantType => plantType;

        private PlantVisual _plantVisual = null;
        public PlantVisual PlantVisual => _plantVisual;
        
        //Todo: Get plot visual

        public void LoadPlantVisuals()
        {
            if (plantType == TPlant.None)
            {
                return;    
            }
            
            var plantVisual = Feature.Config.plantVisuals.FirstOrDefault(v => v.PlantID == plantType);
            
            if(plantVisual != null)
            {
               _plantVisual = Instantiate(plantVisual, transform);
            }
        }

        public void PlantSeed(TPlant plant)
        {
             var plantPrefab = Feature.Config.plantVisuals.FirstOrDefault(v => v.PlantID == plant);

             _plantVisual = Instantiate(plantPrefab, transform);
             plantType = plant;
        }
        
    }
}