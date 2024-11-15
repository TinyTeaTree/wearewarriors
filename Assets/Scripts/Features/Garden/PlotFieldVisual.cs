using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlotFieldVisual : BaseVisual<Garden>
    {
        [SerializeField] private int fieldID;
        [SerializeField] GardenPlotVisual[] gardenPlotVisuals;
        
        public int FieldId => fieldID;
        public GardenPlotVisual[] GardenPlotVisuals => gardenPlotVisuals;

        public void Load()
        {
            
        }
        
        
    }
}