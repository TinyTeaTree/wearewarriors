using System.Linq;
using Codice.CM.Common;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class GardenPlotVisual : BaseVisual<Garden>
    {
        [SerializeField] private int plotID;
        [SerializeField] private TPlant plantType;
        
        public int PlotID => plotID;
        public TPlant PlantType => plantType;
        
        //Todo: Get plot visual

        public void LoadPlantVisuals()
        {
            var plantVisual = Feature.Config.plantVisuals.FirstOrDefault(v => v.PlantID == plantType);

            if(plantVisual != null)
            {
                Instantiate(plantVisual, transform);
            }
        }
        
    }
}