
using System.Linq;
using Core;
using UnityEngine;


namespace Game
{
    public class PlotFieldVisual : BaseVisual<Garden>
    {
        [SerializeField] private int fieldID;
        [SerializeField] GardenPlotVisual[] gardenPlotVisuals;
        
        public int FieldId => fieldID;
        public GardenPlotVisual[] GardenPlotVisuals => gardenPlotVisuals;

        public void LoadPlotField()
        {
            foreach (var plotVisual in gardenPlotVisuals)
            {
                plotVisual.SetFeature(Feature);
                plotVisual.LoadPlantVisuals();
            }
        }
        
        
    }
}