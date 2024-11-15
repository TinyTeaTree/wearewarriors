using System.Linq;
using Codice.CM.Common;
using Core;
using UnityEngine;

namespace Game
{
    public class GardenPlotVisual : BaseVisual<Garden>
    {
        [SerializeField] private int PlotID;
        [SerializeField] private TPlant plantType;
        
        //Todo: Get plot visual

       public void LoadGardenPlot()
        {
            var plant = Feature.Config.GardenPlots.FirstOrDefault(v => v.plantID == plantType)?.PlantVisual;

            Instantiate(plant, transform);
        }
        
    }
}