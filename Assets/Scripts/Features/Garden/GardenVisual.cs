using Core;
using UnityEngine;

namespace Game
{
    public class GardenVisual : BaseVisual<Garden>
    {
        [SerializeField] private PlotFieldVisual[] _plotFieldVisual;
        
        public PlotFieldVisual[] FieldVisuals => _plotFieldVisual;

        public void LoadPlotFieldVisuals()
        {
            foreach (var fieldVisual in _plotFieldVisual)
            {
                fieldVisual.SetFeature(Feature);
                fieldVisual.LoadPlotField();
            }
        }

    }
}